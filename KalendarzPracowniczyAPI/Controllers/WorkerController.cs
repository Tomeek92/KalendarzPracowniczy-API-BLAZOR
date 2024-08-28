using KalendarzPracowniczyApplication.Dto;
using KalendarzPracowniczyApplication.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace KalendarzPracowniczyAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class WorkerController : Controller
    {
        private readonly IWorkerService _workerService;

        public WorkerController(IWorkerService workerService)
        {
            _workerService = workerService;
        }

        public async Task Create([FromBody] WorkerDto workerDto)
        {
            await _workerService.Create(workerDto);
        }

        [HttpDelete("{id}")]
        public async Task Delete(Guid id)
        {
            await _workerService.Delete(id);
        }

        [HttpGet("{id}")]
        public async Task<WorkerDto> GetById(Guid id)
        {
            var findId = await _workerService.GetElementById(id);
            return findId;
        }

        [HttpGet]
        public async Task<IEnumerable<WorkerDto>> GetAllWorkerDto()
        {
            var getAll = await _workerService.GetAllWorkersDto();
            return getAll;
        }

        [HttpPut]
        public async Task Update([FromBody] WorkerDto workerDto)
        {
            await _workerService.Update(workerDto);
        }
    }
}