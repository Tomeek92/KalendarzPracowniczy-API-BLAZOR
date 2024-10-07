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
                var findIdWorker = await _context.Cars.FindAsync(id);
                if (findIdWorker == null)
                {
                    throw new KeyNotFoundException($"Nie znaleziono pracownika o podanym numerze {id}");
                }
                return findIdWorker;
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
                var findWorker = await GetElementById(id);
                if (findWorker == null)
                {
                    throw new KeyNotFoundException($"Nie znaleziono pracownika o podanym {id}");
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
                var findCarToUpdate = await _context.Cars.FindAsync(carUpdate.Id);
                if (findCarToUpdate == null)
                {
                    throw new KeyNotFoundException($"Nie znaleziono pracownika o podanym numerze Id: {carUpdate.Id}");
                }
                _context.Cars.Update(carUpdate);
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
                bool existingWorker = await _context.Cars.AnyAsync(worker => worker.Name == newCar.Name);
                if (existingWorker)
                {
                    throw new Exception($"Pracownik o podanej nazwie już istnieje! {newCar.Name}");
                }

                _context.Cars.Add(newCar);
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                throw new Exception($"Nieoczekiwany błąd zgłoś się do administratora", ex);
            }
        }

        public async Task<IEnumerable<Car>> GetAllWorkers()
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
    }
}