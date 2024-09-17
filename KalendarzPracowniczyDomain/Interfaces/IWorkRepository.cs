using KalendarzPracowniczyDomain.Entities.Works;

namespace KalendarzPracowniczyDomain.Interfaces
{
    public interface IWorkRepository
    {
        Task<List<Work>> GetUserTasksAsync();

        Task<Work> GetTaskByIdAsync(Guid id);

        Task AddTaskAsync(Work work);

        Task UpdateTaskAsync(Work work);

        Task DeleteTaskAsync(Guid id);
    }
}