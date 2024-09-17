using AutoMapper;
using KalendarzPracowniczyApplication.Dto;
using KalendarzPracowniczyDomain.Interfaces;
using MediatR;

namespace KalendarzPracowniczyApplication.CQRS.Queries.Works.GetUserTaskById
{
    public class GetUserTaskByIdQueryHandler : IRequestHandler<GetUserTaskByIdQuery, WorkDto>
    {
        private readonly IMapper _mapper;
        private readonly IWorkRepository _workRepository;

        public GetUserTaskByIdQueryHandler(IMapper mapper, IWorkRepository workRepository)
        {
            _mapper = mapper;
            _workRepository = workRepository;
        }

        public async Task<WorkDto> Handle(GetUserTaskByIdQuery request, CancellationToken cancellationToken)
        {
            try
            {
                var findId = await _workRepository.GetTaskByIdAsync(request.Id);
                var mapp = _mapper.Map<WorkDto>(findId);
                return mapp;
            }
            catch (AutoMapperMappingException ex)
            {
                throw new AutoMapperMappingException($"Błąd podczas mapowania {ex.Message}");
            }
        }
    }
}