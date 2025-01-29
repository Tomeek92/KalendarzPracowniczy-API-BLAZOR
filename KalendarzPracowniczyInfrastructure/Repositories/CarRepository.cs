using KalendarzPracowniczyDomain.Entities.Cars;
using KalendarzPracowniczyDomain.Interfaces;
using KalendarzPracowniczyInfrastructureDbContext;
using Microsoft.EntityFrameworkCore;

namespace KalendarzPracowniczyInfrastructure.Repositories
{
    public class CarRepository : ICarRepository
    {
        private readonly KalendarzPracowniczyDbContext _context;

        public CarRepository(KalendarzPracowniczyDbContext context)
        {
            _context = context;
        }

        public async Task<Car> GetElementById(Guid id)
        {
            try
            {
                var findIdcar = await _context.Cars.FindAsync(id);
                if (findIdcar == null)
                {
                    throw new KeyNotFoundException($"Nie znaleziono pracownika o podanym numerze {id}");
                }
                return findIdcar;
            }
            catch (Exception ex)
            {
                throw new Exception($"Nieoczekiwany błąd zgłoś się do administratora", ex);
            }
        }

        public async Task Delete(Guid id)
        {
            try
            {
                var findCar = await GetElementById(id);
                if (findCar == null)
                {
                    throw new KeyNotFoundException($"Nie znaleziono pracownika o podanym {id}");
                }
                else
                {
                    _context.Cars.Remove(findCar);
                    await SaveAsync();
                }
            }
            catch (Exception ex)
            {
                throw new Exception($"Nieoczekiwany błąd zgłoś się do administratora", ex);
            }
        }

        public async Task Update(Car carUpdate)
        {
            try
            {
                _context.Cars.Update(carUpdate);
                await SaveAsync();
            }
            catch (Exception ex)
            {
                throw new Exception($"Nieoczekiwany błąd podczas aktulizowania pracownika", ex);
            }
        }

        private async Task SaveAsync()
        {
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException ex)
            {
                throw new DbUpdateException($"Błąd podczas zapisu do bazy danyc {ex.Message}");
            }
            catch (Exception ex)
            {
                throw new Exception($"Nieoczekiwany błąd {ex.Message}");
            }
        }

        public async Task Create(Car newCar)
        {
            try
            {
                await EnsureCarDoesNotExistAsync(newCar);
                _context.Cars.Add(newCar);
                await SaveAsync();
            }
            catch (Exception ex)
            {
                throw new Exception($"Nieoczekiwany błąd zgłoś się do administratora {ex.Message}");
            }
        }

        private async Task EnsureCarDoesNotExistAsync(Car existingCar)
        {
            if (existingCar is null)
            {
                throw new ArgumentNullException(nameof(existingCar), "Car nie może być nullem");
            }
            bool carExist = await _context.Cars.AnyAsync(car => car.CarPlatesNumber == existingCar.CarPlatesNumber && car.IsActive == true);
            if (carExist)
            {
                throw new Exception($"Samochód o podanych numerach rejestracyjnych już istnieje! {existingCar.CarPlatesNumber}");
            }
        }

        public async Task<IEnumerable<Car>> GetAllCars()
        {
            try
            {
                var allCars = await _context.Cars
                    .Where(car => car.IsActive == true)
                    .ToListAsync();
                if (allCars == null)
                {
                    throw new KeyNotFoundException($"Nie znaleziono pracowników");
                }
                return allCars;
            }
            catch (Exception ex)
            {
                throw new Exception($"Nieoczekiwany błąd zgłoś się do administratora", ex);
            }
        }

        public async Task<List<Car>> GetAvailableCarsAsync(DateTime? selectedDate)
        {
            try
            {
                if (selectedDate == DateTime.MinValue)
                {
                    throw new ArgumentException($"Błąd podczas pobierania daty");
                }
                var cars = await _context.Cars
                    .Where(car => car.IsActive == true)
                    .Where(car =>
                    !_context.Events.Any(e => e.CarId == car.Id && e.StartDate.HasValue && e.StartDate.Value.Date == selectedDate.Value.Date && !e.IsDeleted))
                .ToListAsync();
                return cars;
            }
            catch (Exception ex)
            {
                throw new Exception($"Nieoczekiwany błąd {ex.Message}");
            }
        }
    }
}