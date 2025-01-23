using KalendarzPracowniczyDomain.Entities.Events;

namespace KalendarzPracowniczyDomain.Interfaces
{
    public interface IEventRepository
    {
        Task<Event> GetElementById(Guid id);

        Task Create(Event createEvent);

        Task SaveAsync();

        Task Update(Event updateEvent);

        Task<IEnumerable<Event>> GettAllEvents();
    }
}