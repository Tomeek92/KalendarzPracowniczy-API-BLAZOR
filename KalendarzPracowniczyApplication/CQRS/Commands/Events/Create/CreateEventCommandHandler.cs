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
                CarDto? currentCar = null;

                if (request.CarId.HasValue)
                {
                    currentCar = await _mediator.Send(new GetCarByIdQuery(request.CarId.Value), cancellationToken);
                }

                var newEvent = _mapper.Map<Event>(request);
                newEvent.UserId = request.UserId;
                newEvent.User = null;

                var mappCar = _mapper.Map<Car>(currentCar);
                newEvent.Car = mappCar;

                await _eventRepository.Create(newEvent, cancellationToken);

                var eventDto = _mapper.Map<EventDto>(newEvent);

                return eventDto;
            }
            catch (AutoMapperMappingException ex)
            {
                throw new AutoMapperMappingException($"Błąd mapowania", ex);
            }
            catch (Exception ex)
            {
                throw new Exception($"{ex.Message}");
            }
        }
    }
}