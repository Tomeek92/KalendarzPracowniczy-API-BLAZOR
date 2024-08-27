using KalendarzPracowniczyApplication.Dto;

namespace KalendarzPracowniczyApplication.Interfaces
{
    public interface IWorkerService
    {
        Task<WorkerDto> GetElementById(Guid id);

        Task Create(WorkerDto worker);

        Task Delete(Guid id);

        Task<IEnumerable<WorkerDto>> GetAllWorkersDto();

        Task Update(WorkerDto worker);
    }
}