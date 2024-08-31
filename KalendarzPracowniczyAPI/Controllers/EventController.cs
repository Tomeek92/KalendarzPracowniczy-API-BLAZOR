using KalendarzPracowniczyApplication.CQRS.Commands.Events.Create;
using KalendarzPracowniczyApplication.CQRS.Queries.Events;
using KalendarzPracowniczyApplication.Dto;
using KalendarzPracowniczyApplication.Interfaces;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace KalendarzPracowniczyAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class EventController : ControllerBase
    {
        private readonly IMediator _mediator;

        public EventController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost]
        public async Task Create([FromBody] CreateEventCommand command)
        {
            await _mediator.Send(command);
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
            var getAll = await _mediator.Send(new GetAllEventQuery());
            return getAll;
        }

        [HttpPut]
        public async Task Update([FromBody] EventDto eventDto)
        {
            await _eventService.Update(eventDto);
        }
    }
}