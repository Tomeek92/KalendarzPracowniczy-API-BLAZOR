using KalendarzPracowniczyDomain.Entities.Workers;

namespace KalendarzPracowniczyDomain.Interfaces
{
    public interface IWorkerRepository
    {
        Task<Worker> GetElementById(Guid id);

        Task Delete(Guid id);

        Task Update(Worker workerUpdate);

        Task Create(Worker newWorker);

        Task<IEnumerable<Worker>> GetAllWorkers();
    }
}