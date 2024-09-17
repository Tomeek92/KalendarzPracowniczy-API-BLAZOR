using AutoMapper;
using KalendarzPracowniczyApplication.Dto;
using KalendarzPracowniczyDomain.Entities.Works;
using KalendarzPracowniczyDomain.Interfaces;
using MediatR;

namespace KalendarzPracowniczyApplication.CQRS.Queries.Works.GetUserTasks
{
    public class GetUserTasksQueryHandler : IRequestHandler<GetUserTasksQuery, List<WorkDto>>
    {
        private readonly IMapper _mapper;
        private readonly IWorkRepository _workerRepository;

        public GetUserTasksQueryHandler(IMapper mapper, IWorkRepository workRepository)
        {
            _mapper = mapper;
            _workerRepository = workRepository;
        }

        public async Task<List<WorkDto>> Handle(GetUserTasksQuery request, CancellationToken cancellationToken)
        {
            try
            {
                var work = await _workerRepository.GetUserTasksAsync();
                var mapp = _mapper.Map<List<WorkDto>>(work);
                return mapp;
            }
            catch (AutoMapperMappingException ex)
            {
                throw new AutoMapperMappingException($"Błąd podczas mapowania {ex.Message}");
            }
        }
    }
}