using AutoMapper;
using KalendarzPracowniczyApplication.CQRS.Commands.DayOff.Create;
using KalendarzPracowniczyDomain.Entities.UserDayOff;
using KalendarzPracowniczyDomain.Interfaces;
using Moq;
using Xunit;

namespace TestKalendarzPracowniczy.Create
{
    public class CreateDayOffCommandHandlerTest
    {
        private readonly Mock<IDayOffRepository> _dayOffRepositoryMock;
        private readonly Mock<IMapper> _mapper;
        private readonly CreateDayOffCommandHandler _handler;

        public CreateDayOffCommandHandlerTest()
        {
            _dayOffRepositoryMock = new Mock<IDayOffRepository>();
            _mapper = new Mock<IMapper>();
            _handler = new CreateDayOffCommandHandler(_mapper.Object, _dayOffRepositoryMock.Object);
        }

        [Fact]
        public async Task Handle_ShouldCreateDayOff_WhenMapperIsSuccess()
        {
            var command = new CreateDayOffCommand { DateDayOff = new DateOnly(2025, 8, 22) };
            var mappedDayOff = new DayOff { DateDayOff = new DateOnly(2025, 8, 22) };

            _mapper.Setup(mapp => mapp.Map<DayOff>(command))
                .Returns(mappedDayOff);

            var cancellationToken = CancellationToken.None;

            await _handler.Handle(command, cancellationToken);

            _dayOffRepositoryMock.Verify(repo => repo.Create(It.Is<DayOff>(d => d.DateDayOff == mappedDayOff.DateDayOff)), Times.Once());
        }

        [Fact]
        public async Task Handle_ShouldThrowAutomapperException_WhenMapperIsFail()
        {
            var command = new CreateDayOffCommand();

            _mapper.Setup(mapp => mapp.Map<DayOff>(command))
                .Throws(new AutoMapperMappingException("Błąd podczas mapowania"));

            var cancellationToken = CancellationToken.None;

            var exception = await Assert.ThrowsAsync<AutoMapperMappingException>(() =>
                _handler.Handle(command, cancellationToken));

            Assert.Contains("Błąd podczas mapowania", exception.Message);
        }
    }
}