using KalendarzPracowniczyApplication.Dto;
using MediatR;

namespace KalendarzPracowniczyApplication.CQRS.Commands.Users.Login
{
    public class LoginCommand : IRequest<UserDto>
    {
        public string UserName { get; set; }
        public string Password { get; set; }
        public string Email { get; set; }

        public LoginCommand()
        { }

        public LoginCommand(string userName, string password, string email)
        {
            UserName = userName;
            Password = password;
            Email = email;
        }
    }
}