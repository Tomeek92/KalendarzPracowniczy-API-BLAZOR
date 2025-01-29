using AutoMapper;
using KalendarzPracowniczyApplication.CQRS.Commands.Users.Create;
using KalendarzPracowniczyDomain.Entities.Users;
using KalendarzPracowniczyDomain.Interfaces;
using Moq;
using Xunit;

namespace TestKalendarzPracowniczy.Create
{
    public class CreateUserCommandHandlerTest
    {
        private readonly Mock<IUserRepository> _userRepositoryMock;
        private readonly Mock<IMapper> _mapperMock;
        private readonly CreateUserCommandHandler _handler;

        public CreateUserCommandHandlerTest()
        {
            _userRepositoryMock = new Mock<IUserRepository>();
            _mapperMock = new Mock<IMapper>();
            _mapperMock
           .Setup(m => m.Map<User>(It.IsAny<CreateUserCommand>()))
           .Returns((CreateUserCommand cmd) => new User
           {
               UserName = cmd.UserName,
               Password = cmd.Password
           });
            _handler = new CreateUserCommandHandler(_userRepositoryMock.Object, _mapperMock.Object);
        }

        [Fact]
        public async Task Handle_ShouldCreateUser_WhenCommandIsValid()
        {
            var command = new CreateUserCommand
            {
                Id = Guid.NewGuid().ToString(),
                UserName = "testUser",
                Password = "testPassword"
            };
            await _handler.Handle(command, CancellationToken.None);

            _userRepositoryMock.Verify(repo => repo.CreateUser(It.Is<User>(u => u.UserName == "testUser"), "testPassword"), Times.Once);
        }
    }
}