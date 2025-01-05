using KalendarzPracowniczyApplication.CQRS.Commands.DayOff.Create;
using KalendarzPracowniczyApplication.CQRS.Commands.DayOff.Delete;
using KalendarzPracowniczyApplication.CQRS.Commands.DayOff.Update;
using KalendarzPracowniczyApplication.CQRS.Queries.DayOff.GetAllDaysOff;
using KalendarzPracowniczyApplication.CQRS.Queries.DayOff.GetDayOffById;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace KalendarzPracowniczyAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class DayOffController : Controller
    {
        private readonly IMediator _mediator;

        public DayOffController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreateDayOffCommand createDayOffCommand)
        {
            try
            {
                await _mediator.Send(createDayOffCommand);
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
                var command = new DeleteDayOffCommand(id);
                await _mediator.Send(command);
                return Ok();
            }
            catch (KeyNotFoundException ex)
            {
                throw new KeyNotFoundException($"Nie odnaleziono urlopu do usunięcia {ex.Message}");
            }
            catch
            {
                return StatusCode(500, "Internal server error");
            }
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetElementById(Guid id)
        {
            try
            {
                var query = new GetDayOffByIdQuery(id);
                await _mediator.Send(query);
                return Ok(query);
            }
            catch (KeyNotFoundException ex)
            {
                throw new KeyNotFoundException($"Nie odnaleziono urlopu do pobrania {ex.Message}");
            }
            catch
            {
                return StatusCode(500, "Internal server error");
            }
        }
        [HttpGet("GetAllDaysOff")]
        public async Task<IActionResult> GetAllDaysOffDto()
        {
            try
            {
                var query = new GetAllDaysOffQuery();
                var result = await _mediator.Send(query);
                if (result == null)
                {
                    return NotFound($"Nie odnaleziono dnia wolnego");
                }
                return Ok(result);
            }
            catch (Exception ex)
            {
                throw new Exception($"Nieoczekiwany błąd podczas pobierania dni wolnych {ex.Message}");
            }
        }
        [HttpPut]
        public async Task<IActionResult> Update(UpdateDayOffCommand updateDayOffCommand)
        {
            try
            {
                await _mediator.Send(updateDayOffCommand);
                return Ok();
            }
            catch (KeyNotFoundException ex)
            {
                throw new KeyNotFoundException($"Nie odnaleziono urlopu do aktualizacji {ex.Message}");
            }
            catch
            {
                return StatusCode(500, "Internal server error");
            }
        }
    }
}