using KalendarzPracowniczyDomain.Entities.UserDayOff;
using KalendarzPracowniczyDomain.Interfaces;
using KalendarzPracowniczyInfrastructureDbContext;
using Microsoft.EntityFrameworkCore;

namespace KalendarzPracowniczyInfrastructure.Repositories
{
    public class DayOffRepository : IDayOffRepository
    {
        private readonly KalendarzPracowniczyDbContext _context;

        public DayOffRepository(KalendarzPracowniczyDbContext context)
        {
            _context = context;
        }

        public async Task<List<DayOff>> GetElementById(string userId)
        {
            try
            {
                var findIdayOff = await _context.DaysOff
                  .Where(w => w.UserId == userId)
                  .ToListAsync();

                if (findIdayOff == null)
                {
                    throw new KeyNotFoundException($"Nie znaleziono pracownika o podanym numerze {userId}");
                }
                return findIdayOff;
            }
            catch (Exception ex)
            {
                throw new Exception($"Nieoczekiwany błąd zgłoś się do administratora", ex);
            }
        }
        public async Task<List<DayOff>> GetAllDaysOff()
        {
            try
            {
                var allDaysOff = await _context.DaysOff
                .Include(u => u.Users).ToListAsync();

                if (allDaysOff == null)
                {
                    throw new Exception($"Błąd podczas pobierania dni wolnych");
                }
                return allDaysOff;
            }
            catch (Exception ex)
            {
                throw new Exception($"Błąd podczas pobierania dni wolnych {ex.Message}");
            }
        }
        public async Task Delete(Guid id)
        {
            try
            {
                var findDayOff = await _context.DaysOff.FindAsync(id);
                if (findDayOff == null)
                {
                    throw new KeyNotFoundException($"Nie znaleziono pracownika o podanym {id}");
                }
                else
                {
                    _context.DaysOff.Remove(findDayOff);
                    await _context.SaveChangesAsync();
                }
            }
            catch (Exception ex)
            {
                throw new Exception($"Nieoczekiwany błąd zgłoś się do administratora", ex);
            }
        }

        public async Task Create(DayOff createDayOff, CancellationToken cancellationToken)
        {
            try
            {
                _context.Add(createDayOff);
                var result = await _context.SaveChangesAsync(cancellationToken);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.InnerException?.Message);
                throw new Exception($"Błąd podczas dodawania zadania {ex.Message}");
            }
        }

        public async Task Update(DayOff updateDayOff)
        {
            try
            {
                _context.Update(updateDayOff);
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                throw new Exception($"Błąd podczas update {ex.Message}");
            }
        }
    }
}