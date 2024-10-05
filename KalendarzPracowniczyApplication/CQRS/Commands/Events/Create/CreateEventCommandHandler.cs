using AutoMapper;
using KalendarzPracowniczyApplication.CQRS.Queries.Users.LoggedUser;
using KalendarzPracowniczyApplication.Dto;
using KalendarzPracowniczyDomain.Entities.Events;
using KalendarzPracowniczyDomain.Interfaces;
using MediatR;

namespace KalendarzPracowniczyApplication.CQRS.Commands.Events.Create
{
    public class CreateEventCommandHandler : IRequestHandler<CreateEventCommand, EventDto>
    {
        private readonly IEventRepository _eventRepository;
        private readonly IMapper _mapper;
        private readonly IMediator _mediator;

        public CreateEventCommandHandler(IEventRepository eventRepository, IMapper mapper, IMediator mediator)
        {
            _eventRepository = eventRepository;
            _mapper = mapper;
            _mediator = mediator;
        }

        public async Task<EventDto> Handle(CreateEventCommand request, CancellationToken cancellationToken)
        {
            try
            {
                var loggedUser = await _mediator.Send(new LoggedUserQuery(), cancellationToken);
                if (loggedUser == null)
                {
                    throw new Exception("Nie można utworzyć wydarzenia bez zalogowanego użytkownika.");
                }
                var newEvent = _mapper.Map<Event>(request);

                newEvent.UserId = loggedUser.Id;

                newEvent.User = null;

                await _eventRepository.Create(newEvent, cancellationToken);

                var eventDto = _mapper.Map<EventDto>(newEvent);
                return eventDto;
            }
            catch (AutoMapperMappingException ex)
            {
                throw new AutoMapperMappingException($"Błąd mapowania", ex);
            }
        }
    }
}