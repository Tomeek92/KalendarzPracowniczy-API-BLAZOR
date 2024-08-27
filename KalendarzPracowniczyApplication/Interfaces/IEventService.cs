using KalendarzPracowniczyApplication.Dto;

namespace KalendarzPracowniczyApplication.Interfaces
{
    public interface IEventService
    {
        Task Create(EventDto eventDto);

        Task Update(EventDto eventDto);

        Task<IEnumerable<EventDto>> GetAllEventsDto();

        Task Delete(Guid id);

        Task<EventDto> GetElementById(Guid id);
    }
}