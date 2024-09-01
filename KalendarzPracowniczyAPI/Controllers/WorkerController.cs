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
    public class WorkerController : Controller
    {
        private readonly IMediator _mediator;

        public WorkerController(IMediator mediator)
        {
            _mediator = mediator;
        }
        [HttpPost]

        public async Task<IActionResult> Create([FromBody] CreateWorkerCommand workerCommand)
        {
            try
            {
                await _mediator.Send(workerCommand);
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
                var command = new DeleteWorkerCommand(id);
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
                var query = new GetWorkerByIdQuery(id);
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

        [HttpGet]
        public async Task<IActionResult> GetAllWorkerDto()
        {
            try
            {
                var query = new GetAllWorkersQuery();
                var result = await _mediator.Send(query);
                if (result == null)
                {
                    return NotFound($"Nie odnaleziono pracowników");
                }
                return Ok(result);
            }
            catch (Exception)
            {
                return StatusCode(500, "Internal server error");
            }
        }

        [HttpPut]
        public async Task<IActionResult> Update([FromBody] UpdateWorkerCommand workerCommand)
        {
            try
            {
                await _mediator.Send(workerCommand);
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