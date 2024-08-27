using AutoMapper;
using KalendarzPracowniczyApplication.Dto;
using KalendarzPracowniczyApplication.Interfaces;
using KalendarzPracowniczyDomain.Entities.Events;
using KalendarzPracowniczyDomain.Interfaces;

namespace KalendarzPracowniczyApplication.Service
{
    public class EventService : IEventService
    {
        private readonly IEventRepository _event;
        private readonly IMapper _mapper;

        public EventService(IEventRepository eventRepository, IMapper mapper)
        {
            _event = eventRepository;
            _mapper = mapper;
        }

        public async Task Create(EventDto eventDto)
        {
            try
            {
                var mapp = _mapper.Map<Event>(eventDto);
                await _event.Create(mapp);
            }
            catch (AutoMapperMappingException ex)
            {
                throw new AutoMapperMappingException($"Błąd mapowania", ex);
            }
        }

        public async Task Update(EventDto eventDto)
        {
            try
            {
                var mapp = _mapper.Map<Event>(eventDto);
                await _event.Update(mapp);
            }
            catch (AutoMapperMappingException ex)
            {
                throw new AutoMapperMappingException($"Błąd mapowania", ex);
            }
        }

        public async Task<IEnumerable<EventDto>> GetAllEventsDto()
        {
            try
            {
                var getAllEvents = await _event.GettAllEvents();
                var mapp = _mapper.Map<IEnumerable<EventDto>>(getAllEvents);
                return mapp;
            }
            catch (AutoMapperMappingException ex)
            {
                throw new AutoMapperMappingException($"Błąd mapowania", ex);
            }
        }

        public async Task<EventDto> GetElementById(Guid id)
        {
            var existingEvent = await _event.GetElementById(id);
            var mapp = _mapper.Map<EventDto>(id);
            return mapp;
        }

        public async Task Delete(Guid id)
        {
            await _event.Delete(id);
        }
    }
}