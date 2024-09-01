using AutoMapper;
using KalendarzPracowniczyApplication.Dto;
using KalendarzPracowniczyDomain.Interfaces;
using MediatR;

namespace KalendarzPracowniczyApplication.CQRS.Queries.Workers.GetAllWorkers
{
    public class GetAllWorkersQueryHandler : IRequestHandler<GetAllWorkersQuery, IEnumerable<WorkerDto>>
    {
        private readonly IMapper _mapper;
        private readonly IWorkerRepository _workerRepository;
        public GetAllWorkersQueryHandler(IMapper mapper, IWorkerRepository workerRepository)
        {
            _mapper = mapper;
            _workerRepository = workerRepository;
        }
        public async Task<IEnumerable<WorkerDto>> Handle(GetAllWorkersQuery request, CancellationToken cancellationToken)
        {
            try
            {
                var workers = await _workerRepository.GetAllWorkers();
                var mapp = _mapper.Map<IEnumerable<WorkerDto>>(workers);
                return mapp;
            }
            catch (AutoMapperMappingException ex)
            {
                throw new AutoMapperMappingException($"Nieprawidłowe mapowanie", ex);
            }
        }
    }
}
