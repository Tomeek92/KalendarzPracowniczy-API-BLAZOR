using MediatR;

namespace KalendarzPracowniczyApplication.CQRS.Commands.Users.LogIn
{
    public class LoginUserCommand : IRequest<Result>
    {
        public string Email { get; set; } = null!;
        public string Password { get; set; } = null!;
        public bool RememberMe { get; set; }
    }
}