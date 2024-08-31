using AutoMapper;
using KalendarzPracowniczyDomain.Entities.Events;
using KalendarzPracowniczyDomain.Interfaces;
using MediatR;

namespace KalendarzPracowniczyApplication.CQRS.Commands.Events.Create
{
    public class CreateEventCommandHandler : IRequestHandler<CreateEventCommand>
    {
        private readonly IEventRepository _eventRepository;
        private readonly IMapper _mapper;
        public CreateEventCommandHandler(IEventRepository eventRepository, IMapper mapper)
        {
            _eventRepository = eventRepository;
            _mapper = mapper;
        }
        public async Task Handle(CreateEventCommand request, CancellationToken cancellationToken)
        {
            try
            {
                var mapp = _mapper.Map<Event>(request);
                await _eventRepository.Create(mapp);
            }
            catch (AutoMapperMappingException ex)
            {
                throw new AutoMapperMappingException($"Błąd mapowania", ex);
            }
        }
    }
}
