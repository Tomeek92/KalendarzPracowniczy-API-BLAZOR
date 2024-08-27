using AutoMapper;
using KalendarzPracowniczyApplication.Dto;
using KalendarzPracowniczyApplication.Interfaces;
using KalendarzPracowniczyDomain.Entities.Workers;
using KalendarzPracowniczyDomain.Interfaces;

namespace KalendarzPracowniczyApplication.Service
{
    public class WorkerService : IWorkerService
    {
        private readonly IWorkerRepository _worker;
        private readonly IMapper _mapper;

        public WorkerService(IWorkerRepository worker, IMapper mapper)
        {
            _worker = worker;
            _mapper = mapper;
        }

        public async Task<WorkerDto> GetElementById(Guid id)
        {
            try
            {
                var findId = await _worker.GetElementById(id);
                var mapp = _mapper.Map<WorkerDto>(id);
                return mapp;
            }
            catch (AutoMapperMappingException ex)
            {
                throw new AutoMapperMappingException($"Błąd przy mapowaniu", ex);
            }
        }

        public async Task Create(WorkerDto worker)
        {
            try
            {
                var mapp = _mapper.Map<Worker>(worker);
                await _worker.Create(mapp);
            }
            catch (AutoMapperMappingException ex)
            {
                throw new AutoMapperMappingException($"Błąd przy mapowaniu", ex);
            }
        }

        public async Task Delete(Guid id)
        {
            await _worker.Delete(id);
        }

        public async Task<IEnumerable<WorkerDto>> GetAllWorkersDto()
        {
            try
            {
                var workers = await _worker.GetAllWorkers();
                var mapp = _mapper.Map<IEnumerable<WorkerDto>>(workers);
                return mapp;
            }
            catch (AutoMapperMappingException ex)
            {
                throw new AutoMapperMappingException($"Nieprawidłowe mapowanie", ex);
            }
        }

        public async Task Update(WorkerDto worker)
        {
            try
            {
                var mapp = _mapper.Map<Worker>(worker);
                await _worker.Update(mapp);
            }
            catch (AutoMapperMappingException ex)
            {
                throw new AutoMapperMappingException($"Nieprawidłowe mapowanie", ex);
            }
        }
    }
}