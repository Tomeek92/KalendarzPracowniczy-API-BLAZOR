using AutoMapper;
using KalendarzPracowniczyDomain.Interfaces;
using MediatR;

namespace KalendarzPracowniczyApplication.CQRS.Commands.DayOff.Update
{
    public class UpdateDayOffCommandHandler : IRequestHandler<UpdateDayOffCommand>
    {
        private readonly IMediator _mediator;
        private readonly IMapper _mapper;
        private readonly IDayOffRepository _dayOffRepository;

        public UpdateDayOffCommandHandler(IMediator mediator, IMapper mapper, IDayOffRepository dayOffRepository)
        {
            _mediator = mediator;
            _mapper = mapper;
            _dayOffRepository = dayOffRepository;
        }

        public async Task Handle(UpdateDayOffCommand request, CancellationToken cancellationToken)
        {
            try
            {
                var mapp = _mapper.Map<KalendarzPracowniczyDomain.Entities.UserDayOff.DayOff>(request);
                await _dayOffRepository.Update(mapp);
            }
            catch (AutoMapperMappingException mapp)
            {
                throw new AutoMapperMappingException($"Błąd podczas mapowania {mapp.Message}");
            }
            catch (Exception ex)
            {
                throw new Exception($"Błąd podczas mapowania {ex.Message}");
            }
        }
    }
}