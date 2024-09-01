using AutoMapper;
using KalendarzPracowniczyApplication.Dto;
using KalendarzPracowniczyDomain.Entities.Workers;
using KalendarzPracowniczyDomain.Interfaces;
using MediatR;

namespace KalendarzPracowniczyApplication.CQRS.Queries.Workers.GetWorkerById
{
    public class GetWorkerByIdQueryHandler : IRequestHandler<GetWorkerByIdQuery, WorkerDto>
    {
        private readonly IMapper _mapper;
        private readonly IWorkerRepository _workerRepository;
        public GetWorkerByIdQueryHandler(IMapper mapper, IWorkerRepository workerRepository)
        {
            _mapper = mapper;
            _workerRepository = workerRepository;
        }
        public async Task<WorkerDto> Handle(GetWorkerByIdQuery request, CancellationToken cancellationToken)
        {
            try
            {
                var findId = await _workerRepository.GetElementById(request.Id);
                var mapp = _mapper.Map<WorkerDto>(findId);
                return mapp;
            }
            catch (AutoMapperMappingException ex)
            {
                throw new AutoMapperMappingException($"Błąd przy mapowaniu", ex);
            }
        }
    }
}
