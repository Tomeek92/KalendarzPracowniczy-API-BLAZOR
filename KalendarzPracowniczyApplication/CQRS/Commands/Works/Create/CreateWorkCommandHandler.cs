using AutoMapper;
using KalendarzPracowniczyDomain.Entities.Works;
using KalendarzPracowniczyDomain.Interfaces;
using MediatR;

namespace KalendarzPracowniczyApplication.CQRS.Commands.Works.Create
{
    public class CreateWorkCommandHandler : IRequestHandler<CreateWorkCommand>
    {
        private readonly IMapper _mapper;
        private readonly IWorkRepository _workRepository;

        public CreateWorkCommandHandler(IMapper mapper, IWorkRepository workRepository)
        {
            _mapper = mapper;
            _workRepository = workRepository;
        }

        public async Task Handle(CreateWorkCommand request, CancellationToken cancellationToken)
        {
            try
            {
                var mapp = _mapper.Map<Work>(request);
                await _workRepository.AddTaskAsync(mapp);
            }
            catch (AutoMapperMappingException ex)
            {
                throw new AutoMapperMappingException($"Błąd podczas mapowania{ex.Message}");
            }
        }
    }
}