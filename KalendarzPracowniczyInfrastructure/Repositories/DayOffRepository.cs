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
                    throw new ArgumentNullException(nameof(allDaysOff), $"Błąd podczas pobierania dni wolnych, nie mogą być nullem");
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
                    await SaveAsync();
                }
            }
            catch (Exception ex)
            {
                throw new Exception($"Nieoczekiwany błąd zgłoś się do administratora", ex);
            }
        }

        public async Task Create(DayOff createDayOff)
        {
            try
            {
                _context.Add(createDayOff);
                await SaveAsync();
            }
            catch (Exception ex)
            {
                throw new Exception($"Błąd podczas dodawania zadania {ex.Message}");
            }
        }

        private async Task SaveAsync()
        {
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException ex)
            {
                throw new DbUpdateException($"Błąd podczas zapisu do bazy danyc {ex.Message}");
            }
            catch (Exception ex)
            {
                throw new Exception($"Nieoczekiwany błąd {ex.Message}");
            }
        }

        public async Task Update(DayOff updateDayOff)
        {
            try
            {
                _context.Update(updateDayOff);
                await SaveAsync();
            }
            catch (Exception ex)
            {
                throw new Exception($"Błąd podczas update {ex.Message}");
            }
        }
    }
}