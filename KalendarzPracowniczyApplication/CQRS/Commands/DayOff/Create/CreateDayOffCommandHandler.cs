using AutoMapper;
using KalendarzPracowniczyDomain.Interfaces;
using MediatR;

namespace KalendarzPracowniczyApplication.CQRS.Commands.DayOff.Create
{
    public class CreateDayOffCommandHandler : IRequestHandler<CreateDayOffCommand>
    {
        private readonly IMediator _mediator;
        private readonly IMapper _mapper;
        private readonly IDayOffRepository _dayOffRepository;

        public CreateDayOffCommandHandler(IMapper mapper, IDayOffRepository dayOffRepository, IMediator mediator)
        {
            _mediator = mediator;
            _mapper = mapper;
            _dayOffRepository = dayOffRepository;
        }

        public async Task Handle(CreateDayOffCommand request, CancellationToken cancellationToken)
        {
            try
            {
                var mapp = _mapper.Map<KalendarzPracowniczyDomain.Entities.UserDayOff.DayOff>(request);
                await _dayOffRepository.Create(mapp);
            }
            catch (AutoMapperMappingException mapp)
            {
                throw new AutoMapperMappingException($"Błąd podczas mapowania {mapp.Message}");
            }
            catch (Exception ex)
            {
                throw new Exception($"Błąd podczas tworzenia urlopu {ex.Message}");
            }
        }
    }
}