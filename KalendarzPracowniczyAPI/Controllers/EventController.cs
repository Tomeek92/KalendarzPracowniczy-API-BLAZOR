using KalendarzPracowniczyApplication.Dto;
using KalendarzPracowniczyApplication.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace KalendarzPracowniczyAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class EventController : ControllerBase
    {
        private readonly IEventService _eventService;

        public EventController(IEventService eventService)
        {
            _eventService = eventService;
        }

        [HttpPost]
        public async Task Create([FromBody] EventDto eventDto)
        {
            await _eventService.Create(eventDto);
        }

        [HttpDelete("{id}")]
        public async Task Delete(Guid id)
        {
            await _eventService.Delete(id);
        }

        [HttpGet("{id}")]
        public async Task<EventDto> GetById(Guid id)
        {
            var findId = await _eventService.GetElementById(id);
            return findId;
        }

        [HttpGet]
        public async Task<IEnumerable<EventDto>> GetAllEventsDto()
        {
            var getAll = await _eventService.GetAllEventsDto();
            return getAll;
        }

        [HttpPut]
        public async Task Update([FromBody] EventDto eventDto)
        {
            await _eventService.Update(eventDto);
        }
    }
}