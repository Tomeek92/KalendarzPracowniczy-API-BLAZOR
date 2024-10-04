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
                var existingEvent = await _eventRepository.GetElementById(request.Id);
                if (existingEvent == null)
                {
                    throw new KeyNotFoundException($"Event with id {request.Id} not found.");
                }

                var mapp = _mapper.Map<UpdateEventCommand, Event>(request, existingEvent);
                await _eventRepository.Update(mapp);
            }
            catch (AutoMapperMappingException ex)
            {
                throw new AutoMapperMappingException($"Błąd mapowania", ex);
            }
            catch (Exception ex)
            {
                throw new Exception($"Nieoczekiwany błąd {ex.Message}");
            }
        }
    }
}