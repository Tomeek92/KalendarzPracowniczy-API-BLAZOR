using AutoMapper;
using KalendarzPracowniczyApplication.CQRS.Queries.Users.LoggedUser;
using KalendarzPracowniczyApplication.CQRS.Queries.Workers.GetWorkerById;
using KalendarzPracowniczyApplication.Dto;
using KalendarzPracowniczyDomain.Entities.Cars;
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
                var currentCar = await _mediator.Send(new GetCarByIdQuery(request.CarId), cancellationToken);
                if (currentCar == null)
                {
                    throw new Exception("Nie odnaleziono samochodu podczas tworzenia wyjazdu");
                }

                var newEvent = _mapper.Map<Event>(request);

                newEvent.UserId = request.UserId;
                newEvent.User = null;

                newEvent.Car = _mapper.Map<Car>(currentCar);

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