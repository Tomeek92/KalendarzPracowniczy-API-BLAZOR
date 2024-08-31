using AutoMapper;
using KalendarzPracowniczyDomain.Entities.Events;
using KalendarzPracowniczyDomain.Interfaces;
using MediatR;

namespace KalendarzPracowniczyApplication.CQRS.Commands.Events.Update
{
    public class UpdateEventCommandHandler : IRequestHandler<UpdateEventCommand>
    {
        private readonly IEventRepository _eventRepository;
        private readonly IMapper _mapper;
        public UpdateEventCommandHandler(IEventRepository eventRepository, IMapper mapper)
        {
            _eventRepository = eventRepository;
            _mapper = mapper;
        }

        public async Task Handle(UpdateEventCommand request, CancellationToken cancellationToken)
        {
            try
            {
                var mapp = _mapper.Map<Event>(request);
                await _eventRepository.Update(mapp);
            }
            catch (AutoMapperMappingException ex)
            {
                throw new AutoMapperMappingException($"Błąd mapowania", ex);
            }
        }
    }
}
