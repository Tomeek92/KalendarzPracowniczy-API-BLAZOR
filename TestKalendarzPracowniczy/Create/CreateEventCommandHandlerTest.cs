using AutoMapper;
using KalendarzPracowniczyApplication.CQRS.Commands.Events.Create;
using KalendarzPracowniczyApplication.CQRS.Queries.Workers.GetWorkerById;
using KalendarzPracowniczyApplication.Dto;
using KalendarzPracowniczyDomain.Entities.Cars;
using KalendarzPracowniczyDomain.Entities.Events;
using KalendarzPracowniczyDomain.Interfaces;
using MediatR;
using Moq;
using Xunit;

namespace TestKalendarzPracowniczy.Create
{
    public class CreateEventCommandHandlerTest
    {
        private readonly Mock<IMediator> _mediatorMock;
        private readonly Mock<IMapper> _mapperMock;
        private readonly Mock<IEventRepository> _eventRepositoryMock;
        private readonly CreateEventCommandHandler _handler;

        public CreateEventCommandHandlerTest()
        {
            _mediatorMock = new Mock<IMediator>();
            _mapperMock = new Mock<IMapper>();
            _eventRepositoryMock = new Mock<IEventRepository>();

            _handler = new CreateEventCommandHandler(
                 _eventRepositoryMock.Object,
                _mapperMock.Object,
                 _mediatorMock.Object
            );
        }

        [Fact]
        public async Task Handle_ShouldCreateEvent_WhenCarIdIsProvided()
        {
            var command = new CreateEventCommand
            {
                Title = "TestTittle",
                Description = "TestDescription",
                CarId = Guid.NewGuid(),
                UserId = "1",
                Name = "TestName",
                StartDate = new DateTime(2024, 1, 28)
            };
            var carDto = new CarDto { Id = command.CarId ?? Guid.Empty, Model = "TestCar" };
            var eventToSave = new Event { Id = Guid.NewGuid(), Name = "TestEvent", UserId = command.UserId };
            var eventDto = new EventDto { Id = eventToSave.Id, Name = "TestEvent" };

            _mediatorMock
    .Setup(m => m.Send(It.IsAny<GetCarByIdQuery>(), It.IsAny<CancellationToken>()))
    .ReturnsAsync(carDto);

            _mapperMock
            .Setup(m => m.Map<Event>(command))
            .Returns(eventToSave);

            _mapperMock
           .Setup(m => m.Map<Car>(carDto))
           .Returns(new Car { Id = command.CarId ?? Guid.Empty, Model = "TestCar" });

            _eventRepositoryMock
           .Setup(repo => repo.Create(It.IsAny<Event>()))
           .Returns(Task.CompletedTask);

            _mapperMock
            .Setup(m => m.Map<EventDto>(It.IsAny<Event>()))
            .Returns(eventDto);

            var result = await _handler.Handle(command, CancellationToken.None);

            Assert.NotNull(result);
            Assert.Equal("TestEvent", result.Name);
            Assert.Equal(command.UserId, eventToSave.UserId);
            Assert.Equal(command.CarId, eventToSave.Car.Id);
            _mediatorMock.Verify(m => m.Send(It.IsAny<GetCarByIdQuery>(), It.IsAny<CancellationToken>()), Times.Once);
            _eventRepositoryMock.Verify(repo => repo.Create(It.IsAny<Event>()), Times.Once);
        }
    }
}