using KalendarzPracowniczyApplication.CQRS.Commands.Workers.Create;
using KalendarzPracowniczyApplication.CQRS.Commands.Workers.Delete;
using KalendarzPracowniczyApplication.CQRS.Commands.Workers.Update;
using KalendarzPracowniczyApplication.CQRS.Queries.Workers.GetAllWorkers;
using KalendarzPracowniczyApplication.CQRS.Queries.Workers.GetWorkerById;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace KalendarzPracowniczyAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CarController : Controller
    {
        private readonly IMediator _mediator;

        public CarController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreateCarCommand carCommand)
        {
            try
            {
                await _mediator.Send(carCommand);
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
                var command = new DeleteCarCommand(id);
                await _mediator.Send(command);
                return Ok();
            }
            catch (KeyNotFoundException ex)
            {
                return NotFound($"Nie odnaleziono pracownika do usunięcia");
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
                var query = new GetCarByIdQuery(id);
                var result = await _mediator.Send(query);
                if (result == null)
                {
                    return NotFound($"Nie odnaleziono pracownika");
                }
                return Ok(result);
            }
            catch (Exception)
            {
                return StatusCode(500, "Internal server error");
            }
        }

        [HttpGet("GetAll")]
        public async Task<IActionResult> GetAllCarsDto()
        {
            try
            {
                var query = new GetAllCarsQuery();
                var result = await _mediator.Send(query);
                if (result == null)
                {
                    return NotFound($"Nie odnaleziono samochodów");
                }
                return Ok(result);
            }
            catch (Exception)
            {
                return StatusCode(500, "Internal server error");
            }
        }

        [HttpPut]
        public async Task<IActionResult> Update([FromBody] UpdateCarCommand carCommand)
        {
            try
            {
                await _mediator.Send(carCommand);
                return Ok();
            }
            catch (KeyNotFoundException)
            {
                return NotFound($"Nie odnaleziono pracownika do aktualizacji");
            }
            catch (Exception)
            {
                return StatusCode(500, "Internal server error");
            }
        }
    }
}