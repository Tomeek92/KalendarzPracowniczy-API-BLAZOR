using KalendarzPracowniczyApplication.CQRS.Commands.Works.Create;
using KalendarzPracowniczyApplication.CQRS.Commands.Works.Delete;
using KalendarzPracowniczyApplication.CQRS.Commands.Works.Update;
using KalendarzPracowniczyApplication.CQRS.Queries.Works.GetUserTaskById;
using KalendarzPracowniczyApplication.CQRS.Queries.Works.GetUserTasks;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace KalendarzPracowniczyAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class WorkController : ControllerBase
    {
        private readonly IMediator _mediator;

        public WorkController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost("Create")]
        public async Task<IActionResult> Create([FromBody] CreateWorkCommand createWorkCommand)
        {
            try
            {
                await _mediator.Send(createWorkCommand);
                return Ok();
            }
            catch
            {
                return StatusCode(500, "Błąd podczas tworzenia zadania");
            }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            try
            {
                var command = new DeleteWorkCommand(id);
                await _mediator.Send(id);
                return Ok();
            }
            catch (KeyNotFoundException ex)
            {
                return NotFound($"Nie znaleziono zadania do usunięcia w bazie danych {ex.Message}");
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Błąd podczas usuwania zadania {ex.Message}");
            }
        }

        [HttpPut]
        public async Task<IActionResult> Update(UpdateWorkCommand updateWorkCommand)
        {
            try
            {
                await _mediator.Send(updateWorkCommand);
                return Ok();
            }
            catch (KeyNotFoundException ex)
            {
                throw new KeyNotFoundException($"Nie odnaleziono zadania {ex.Message}");
            }
            catch
            {
                return StatusCode(500, "Błąd podczas aktualizacji zadania");
            }
        }

        [HttpGet("{userid}")]
        public async Task<IActionResult> GetUserTaskById(string userid)
        {
            try
            {
                var query = new GetUserTaskByIdQuery(userid);
                var result = await _mediator.Send(query);
                return Ok(result);
            }
            catch (KeyNotFoundException ex)
            {
                throw new KeyNotFoundException($"Nie odnaleziono id zadania {ex.Message}");
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Błąd podczas pobierania id zadania {ex.Message}");
            }
        }

        [HttpGet("getAlltask")]
        public async Task<IActionResult> GetAllTask()
        {
            try
            {
                var query = new GetUserTasksQuery();
                await _mediator.Send(query);
                return Ok();
            }
            catch (KeyNotFoundException ex)
            {
                throw new KeyNotFoundException($"Nie odnaleziono zadań {ex.Message}");
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Błąd podczas odnajdywania zadań {ex.Message}");
            }
        }
    }
}