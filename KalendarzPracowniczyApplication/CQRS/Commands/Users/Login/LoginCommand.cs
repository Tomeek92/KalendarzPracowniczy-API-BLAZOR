using MediatR;

namespace KalendarzPracowniczyApplication.CQRS.Commands.Users.Login
{
    public class LoginCommand : IRequest
    {
        public string UserName { get; set; } = null!;
        public string Password { get; set; } = null!;

        public LoginCommand()
        {
        }
    }
}