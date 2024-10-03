using KalendarzPracowniczyDomain.Entities.Events;
using KalendarzPracowniczyDomain.Entities.Users;
using KalendarzPracowniczyDomain.Interfaces;
using KalendarzPracowniczyInfrastructureDbContext;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace KalendarzPracowniczyInfrastructure.Repositories
{
    public class EventRepository : IEventRepository
    {
        private readonly KalendarzPracowniczyDbContext _context;
        private readonly UserManager<User> _userManager;

        public EventRepository(KalendarzPracowniczyDbContext context, UserManager<User> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        public async Task<Event> GetElementById(Guid id)
        {
            try
            {
                var findId = await _context.FindAsync<Event>(id);
                if (findId == null)
                {
                    throw new Exception($"Element nie został znaleziony w bazie danych");
                }
                return findId;
            }
            catch (Exception ex)
            {
                throw new Exception($"Błąd przy pobieraniu elementu z bazy danych {ex.Message}");
            }
        }

        public async Task Create(Event createEvent, CancellationToken cancellationToken)
        {
            try
            {
                _context.Add(createEvent);
                var result = await _context.SaveChangesAsync(cancellationToken);
            }
            catch (Exception ex)
            {
                throw new Exception($"Błąd podczas dodawania zadania {ex.Message}");
            }
        }

        public async Task Delete(Guid id)
        {
            try
            {
                var findEventId = await GetElementById(id);
                if (findEventId == null)
                {
                    throw new KeyNotFoundException($"Rekord o danym {id} nie został znaleziony w bazie danych ");
                }
                _context.Remove(findEventId);
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                throw new Exception($"Błąd podczas usuwania rekord", ex);
            }
        }

        public async Task Update(Event updateEvent)
        {
            try
            {
                var existingEvent = await _context.Events.FindAsync(updateEvent.Id);
                if (existingEvent == null)
                {
                    throw new KeyNotFoundException($"Nie znaleziono takiego zadania w bazie danych! {updateEvent.Id}");
                }
                _context.Events.Update(updateEvent);
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                throw new Exception($"Błąd podczas aktualizowania zadania!", ex);
            }
        }

        public async Task<IEnumerable<Event>> GettAllEvents()
        {
            try
            {
                var allEvents = await _context.Events.ToListAsync();

                if (allEvents == null)
                {
                    throw new KeyNotFoundException($"Nie znaleziono żadnych zadań!");
                }
                return allEvents;
            }
            catch
            {
                throw new Exception($"Wystąpił błąd podczas wyświetlania listy zadań!");
            }
        }
    }
}