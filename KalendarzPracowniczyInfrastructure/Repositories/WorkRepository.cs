using KalendarzPracowniczyDomain.Entities.Works;
using KalendarzPracowniczyDomain.Interfaces;
using KalendarzPracowniczyInfrastructureDbContext;
using Microsoft.EntityFrameworkCore;

namespace KalendarzPracowniczyInfrastructure.Repositories
{
    public class WorkRepository : IWorkRepository
    {
        private readonly KalendarzPracowniczyDbContext _context;

        public WorkRepository(KalendarzPracowniczyDbContext context)
        {
            _context = context;
        }

        public async Task<List<Work>> GetUserTasksAsync()
        {
            return await _context.Works.ToListAsync();
        }

        public async Task<Work> GetTaskByIdAsync(Guid id)
        {
            return await _context.Works.FindAsync(id);
        }

        public async Task AddTaskAsync(Work work)
        {
            _context.Works.Add(work);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateTaskAsync(Work work)
        {
            _context.Works.Update(work);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteTaskAsync(Guid id)
        {
            var work = await _context.Works.FindAsync(id);
            if (work != null)
            {
                _context.Works.Remove(work);
                await _context.SaveChangesAsync();
            }
        }
    }
}