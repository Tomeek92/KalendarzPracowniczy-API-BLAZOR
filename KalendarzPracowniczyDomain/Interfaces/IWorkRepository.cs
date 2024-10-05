using KalendarzPracowniczyDomain.Entities.Works;

namespace KalendarzPracowniczyDomain.Interfaces
{
    public interface IWorkRepository
    {
        Task<List<Work>> GetUserTasksAsync();

        Task<List<Work>> GetTaskByIdAsync(string id);

        Task AddTaskAsync(Work work);

        Task UpdateTaskAsync(Work work);

        Task DeleteTaskAsync(Guid id);
    }
}