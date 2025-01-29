using AutoMapper;
using KalendarzPracowniczyApplication.CQRS.Commands.Workers.Create;
using KalendarzPracowniczyDomain.Entities.Cars;
using KalendarzPracowniczyDomain.Interfaces;
using Moq;
using Xunit;

namespace TestKalendarzPracowniczy.Create
{
    public class CreateCarCommandHandlerTest
    {
        private readonly Mock<ICarRepository> _carRepository;
        private readonly Mock<IMapper> _mapper;
        private readonly CreateCarCommandHandler _createCarCommandHandler;

        public CreateCarCommandHandlerTest()
        {
            _carRepository = new Mock<ICarRepository>();
            _mapper = new Mock<IMapper>();
            _createCarCommandHandler = new CreateCarCommandHandler(_mapper.Object, _carRepository.Object);
        }

        [Fact]
        public async Task Handle_ShouldThrowException_WhenMappingFails()
        {
            var command = new CreateCarCommand();

            _mapper.Setup(mapp => mapp.Map<Car>(command))
                .Throws(new AutoMapperMappingException("Błąd podczas mapowania"));

            var cancellationToken = CancellationToken.None;

            var exception = await Assert.ThrowsAsync<AutoMapperMappingException>(() =>
            _createCarCommandHandler.Handle(command, cancellationToken));

            Assert.Contains("Błąd przy mapowaniu", exception.Message);
        }

        [Fact]
        public async Task Handle_ShouldCreateCar_WhenMappingSuccess()
        {
            var command = new CreateCarCommand();
            var car = new Car();

            var carRepositoryMock = new Mock<ICarRepository>();
            var mapperMock = new Mock<IMapper>();

            mapperMock.Setup(m => m.Map<Car>(command))
                      .Returns(car);

            var handler = new CreateCarCommandHandler(mapperMock.Object, carRepositoryMock.Object);
            var cancellationToken = CancellationToken.None;

            await handler.Handle(command, cancellationToken);

            carRepositoryMock.Verify(repo => repo.Create(It.Is<Car>(c => c == car)), Times.Once);
        }
    }
}