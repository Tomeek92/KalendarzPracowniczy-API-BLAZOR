using KalendarzPracowniczyApplication.CQRS.Commands.Events.Create;
using KalendarzPracowniczyApplication.CQRS.Commands.Events.Delete;
using KalendarzPracowniczyApplication.CQRS.Commands.Events.Update;
using KalendarzPracowniczyApplication.CQRS.Queries.Events.GetAll;
using KalendarzPracowniczyApplication.CQRS.Queries.Events.GetElementById;
using KalendarzPracowniczyDomain.Entities.Users;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace KalendarzPracowniczyAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class EventController : ControllerBase
    {
        private readonly UserManager<User> _userManager;
        private readonly IMediator _mediator;

        public EventController(IMediator mediator, UserManager<User> user)
        {
            _mediator = mediator;
            _userManager = user;
        }

        [HttpPost("Create")]
        public async Task<IActionResult> Create([FromBody] CreateEventCommand command)
        {
            try
            {
                await _mediator.Send(command);
                return Ok();
            }
            catch
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

        [HttpGet("GetAll")]
        public async Task<IActionResult> GetAllEventsDto()
        {
            try
            {
                var getAll = new GetAllEventQuery();
                var result = await _mediator.Send(getAll);

                foreach (var evt in result)
                {
                    Console.WriteLine($"Event Title: {evt.Title}, Id: {evt.Id}");
                }
                return Ok(result);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error {ex.Message} ");
            }
        }

        [HttpPut("Update")]
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