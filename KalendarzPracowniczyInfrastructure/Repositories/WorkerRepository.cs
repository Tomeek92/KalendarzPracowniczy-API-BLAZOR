using KalendarzPracowniczyDomain.Entities.Workers;
using KalendarzPracowniczyDomain.Interfaces;
using KalendarzPracowniczyInfrastructureDbContext;
using Microsoft.EntityFrameworkCore;

namespace KalendarzPracowniczyInfrastructure.Repositories
{
    public class WorkerRepository : IWorkerRepository
    {
        private readonly KalendarzPracowniczyDbContext _context;

        public WorkerRepository(KalendarzPracowniczyDbContext context)
        {
            _context = context;
        }

        public async Task<Worker> GetElementById(Guid id)
        {
            try
            {
                var findIdWorker = await _context.Workers.FindAsync(id);
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

        public async Task Update(Worker workerUpdate)
        {
            try
            {
                var findWorkerToUpdate = await _context.Workers.FindAsync(workerUpdate.Id);
                if (findWorkerToUpdate == null)
                {
                    throw new KeyNotFoundException($"Nie znaleziono pracownika o podanym numerze Id: {workerUpdate.Id}");
                }
                _context.Workers.Update(workerUpdate);
            }
            catch (Exception ex)
            {
                throw new Exception($"Nieoczekiwany błąd podczas aktulizowania pracownika", ex);
            }
        }

        public async Task Create(Worker newWorker)
        {
            try
            {
                bool existingWorker = await _context.Workers.AnyAsync(worker => worker.Name == newWorker.Name);
                if (existingWorker)
                {
                    throw new Exception($"Pracownik o podanej nazwie już istnieje! {newWorker.Name}");
                }

                _context.Workers.Add(newWorker);
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                throw new Exception($"Nieoczekiwany błąd zgłoś się do administratora", ex);
            }
        }

        public async Task<IEnumerable<Worker>> GetAllWorkers()
        {
            try
            {
                var allWorkers = await _context.Workers.ToListAsync();
                if (allWorkers == null)
                {
                    throw new KeyNotFoundException($"Nie znaleziono pracowników");
                }
                return allWorkers;
            }
            catch (Exception ex)
            {
                throw new Exception($"Nieoczekiwany błąd zgłoś się do administratora", ex);
            }
        }
    }
}