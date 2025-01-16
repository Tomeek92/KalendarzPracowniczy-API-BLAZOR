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
                var findCar = await _context.Cars.FindAsync(id);
                if (findCar == null)
                {
                    throw new KeyNotFoundException($"Nie znaleziono pracownika o podanym {id}");
                }
                else
                {
                    _context.Cars.Remove(findCar);
                    await _context.SaveChangesAsync();
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
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                throw new Exception($"Nieoczekiwany błąd podczas aktulizowania pracownika", ex);
            }
        }

        public async Task Create(Car newCar)
        {
            try
            {
                bool existingWorker = await _context.Cars.AnyAsync(car => car.CarPlatesNumber == newCar.CarPlatesNumber);
                if (existingWorker)
                {
                    throw new Exception($"Samochód o podanych numerach rejestracyjnych już istnieje! {newCar.CarPlatesNumber}");
                }

                _context.Cars.Add(newCar);
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                throw new Exception($"Nieoczekiwany błąd zgłoś się do administratora", ex);
            }
        }

        public async Task<IEnumerable<Car>> GetAllCars()
        {
            try
            {
                var allCars = await _context.Cars.ToListAsync();
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