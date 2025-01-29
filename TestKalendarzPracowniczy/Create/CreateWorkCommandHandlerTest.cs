using AutoMapper;
using KalendarzPracowniczyApplication.CQRS.Commands.Works.Create;
using KalendarzPracowniczyDomain.Entities.Users;
using KalendarzPracowniczyDomain.Entities.Works;
using KalendarzPracowniczyDomain.Interfaces;
using Moq;
using NuGet.Frameworks;
using Xunit;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;

namespace TestKalendarzPracowniczy.Create
{
    public class CreateWorkCommandHandlerTest
    {
        private readonly Mock<IWorkRepository> _workRepository;
        private readonly Mock<IMapper> _mapper;
        private readonly Mock<IUserRepository> _userRepository;
        private readonly CreateWorkCommandHandler _createWorkCommandHandler;

        public CreateWorkCommandHandlerTest()
        {
            _mapper = new Mock<IMapper>();
            _workRepository = new Mock<IWorkRepository>();
            _userRepository = new Mock<IUserRepository>();
            _createWorkCommandHandler = new CreateWorkCommandHandler(_mapper.Object, _workRepository.Object, _userRepository.Object);
        }

        [Fact]
        public async Task CreateWork_ShouldThrowException_WhenUserDoesntExist()
        {
            var request = new CreateWorkCommand { UserId = "1" };
            _userRepository.Setup(repo => repo.GetUserById(request.UserId))
                .ReturnsAsync((User)null);

            var cancellationToken = CancellationToken.None;

            var exception = await Assert.ThrowsAsync<Exception>(() => _createWorkCommandHandler.Handle(request, cancellationToken));

            Assert.Contains("Nie można utworzyć wydarzenia bez zalogowanego użytkownika.", exception.Message);
        }

        [Fact]
        public async Task Handle_ShouldThrowAutoMapperException_WhenMappingFails()
        {
            var command = new CreateWorkCommand { UserId = "1" };
            var user = new User { Id = "1" };

            _userRepository.Setup(repo => repo.GetUserById(command.UserId))
                .ReturnsAsync(user);
            _mapper.Setup(mapp => mapp.Map<Work>(command))
                .Throws(new AutoMapperMappingException("Błąd podczas mapowania"));

            var cancellationToken = CancellationToken.None;

            var exception = await Assert.ThrowsAnyAsync<AutoMapperMappingException>(() =>
            _createWorkCommandHandler.Handle(command, cancellationToken));

            Assert.Contains("Błąd podczas mapowania", exception.Message);
        }

        [Fact]
        public async Task Handle_ShouldAddWorksAsync_WhenUserExistingAndMappingSuccess()
        {
            var command = new CreateWorkCommand { UserId = "1" };
            var user = new User { Id = "1" };
            var work = new Work { UserId = user.Id };

            _userRepository.Setup(repo => repo.GetUserById(command.UserId))
                .ReturnsAsync(user);
            _mapper.Setup(mapp => mapp.Map<Work>(command))
                .Returns(work);

            var cancellationToken = CancellationToken.None;

            await _createWorkCommandHandler.Handle(command, cancellationToken);

            _workRepository.Verify(repo => repo.AddTaskAsync(It.Is<Work>(w => w.UserId == user.Id)), Times.Once);
        }
    }
}