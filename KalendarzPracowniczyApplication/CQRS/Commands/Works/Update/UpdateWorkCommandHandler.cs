using AutoMapper;
using KalendarzPracowniczyDomain.Entities.Works;
using KalendarzPracowniczyDomain.Interfaces;
using MediatR;

namespace KalendarzPracowniczyApplication.CQRS.Commands.Works.Update
{
    public class UpdateWorkCommandHandler : IRequestHandler<UpdateWorkCommand>
    {
        private readonly IMapper _mapper;
        private readonly IWorkRepository _workRepository;

        public UpdateWorkCommandHandler(IMapper mapper, IWorkRepository workRepository)
        {
            _mapper = mapper;
            _workRepository = workRepository;
        }

        public async Task Handle(UpdateWorkCommand request, CancellationToken cancellationToken)
        {
            try
            {
                var mapp = _mapper.Map<Work>(request);
                await _workRepository.UpdateTaskAsync(mapp);
            }
            catch (AutoMapperMappingException ex)
            {
                throw new AutoMapperMappingException($"Błąd podczas mapowania {ex.Message}");
            }
        }
    }
}