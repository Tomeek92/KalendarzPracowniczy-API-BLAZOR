using KalendarzPracowniczyApplication.CQRS.Commands.Events.Create;
using KalendarzPracowniczyApplication.CQRS.Commands.Events.Delete;
using KalendarzPracowniczyApplication.CQRS.Commands.Events.Update;
using KalendarzPracowniczyApplication.CQRS.Queries.Events.GetAll;
using KalendarzPracowniczyApplication.CQRS.Queries.Events.GetElementById;
using KalendarzPracowniczyApplication.Dto;
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
        public async Task<IActionResult> Create([FromBody] CreateEventCommand command)
        {
            try
            {
                await _mediator.Send(command);
                return Ok();
            }
            catch (Exception)
            {
                return StatusCode(500, "Internal server error");
            }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            try
            {
                var command = new DeleteEventCommand(id);
                await _mediator.Send(command);
                return Ok();
            }
            catch (KeyNotFoundException ex)
            {
                return NotFound(ex);
            }
            catch (Exception ex)
            {
                return StatusCode(500, "Internal server error");
            }
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(Guid id)
        {
            try
            {
                var query = new GetElementByIdEventQuery(id);
                var result = await _mediator.Send(query);
                if (result == null)
                {
                    return NotFound($"Nie odnaleziono zadania z takim numerem {id}");
                }
                return Ok(result);
            }
            catch (Exception ex)
            {
                return StatusCode(500, "Internal server error");
            }
        }

        [HttpGet]
        public async Task<IActionResult> GetAllEventsDto()
        {
            try
            {
                var getAll = new GetAllEventQuery();
                var result = await _mediator.Send(getAll);
                if (result == null)
                {
                    return NotFound("Nie odnaleziono zadań!");
                }
                return Ok(result);
            }
            catch (Exception ex)
            {
                return StatusCode(500, "Internal server error");
            }
        }

        [HttpPut]
        public async Task<IActionResult> Update([FromBody] UpdateEventCommand eventCommand)
        {
            try
            {
                await _mediator.Send(eventCommand);
                return Ok();
            }
            catch (KeyNotFoundException ex)
            {
                return NotFound("Nie odnaleziono zadania do zaktualizowania");
            }
            catch (Exception)
            {
                return StatusCode(500, "Internal server error");
            }
        }
    }
}