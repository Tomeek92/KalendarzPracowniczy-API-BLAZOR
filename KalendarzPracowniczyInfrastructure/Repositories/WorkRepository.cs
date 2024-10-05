using KalendarzPracowniczyDomain.Entities.Works;
using KalendarzPracowniczyDomain.Interfaces;
using KalendarzPracowniczyInfrastructureDbContext;
using Microsoft.EntityFrameworkCore;
using System.Linq;

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

        public async Task<List<Work>> GetTaskByIdAsync(string userid)
        {
            try
            {
                if (string.IsNullOrEmpty(userid))
                {
                    throw new ArgumentException("Parametr 'userid' nie może być pusty.", nameof(userid));
                }
                if (_context.Works == null)
                {
                    throw new InvalidOperationException("Sprawdź czy są rekordy w bazie danych!");
                }
                var userWorks = await _context.Works
                    .Where(w => w.UserId == userid)
                    .ToListAsync();

                return userWorks;
            }
            catch (Exception ex)
            {
                throw new Exception($"Nieoczekiwany błąd {ex.Message}");
            }
        }

        public async Task AddTaskAsync(Work work)
        {
            if (work.Id == Guid.Empty)
            {
                work.Id = Guid.NewGuid();
            }
            _context.Add(work);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateTaskAsync(Work work)
        {
            _context.Update(work);
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