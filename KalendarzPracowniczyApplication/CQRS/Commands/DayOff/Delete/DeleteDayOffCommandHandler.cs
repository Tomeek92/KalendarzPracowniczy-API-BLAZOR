using AutoMapper;
using KalendarzPracowniczyDomain.Interfaces;
using MediatR;

namespace KalendarzPracowniczyApplication.CQRS.Commands.DayOff.Delete
{
    public class DeleteDayOffCommandHandler : IRequestHandler<DeleteDayOffCommand>
    {
        private readonly IMediator _mediator;
        private readonly IMapper _mapper;
        private readonly IDayOffRepository _dayOffRepository;

        public DeleteDayOffCommandHandler(IMapper mapper, IDayOffRepository dayOffRepository, IMediator mediator)
        {
            _mediator = mediator;
            _mapper = mapper;
            _dayOffRepository = dayOffRepository;
        }

        public async Task Handle(DeleteDayOffCommand request, CancellationToken cancellationToken)
        {
            try
            {
                await _dayOffRepository.Delete(request.Id);
            }
            catch (Exception ex)
            {
                throw new Exception($"Błąd podczas usuwania urlopu {ex.Message}");
            }
        }
    }
}