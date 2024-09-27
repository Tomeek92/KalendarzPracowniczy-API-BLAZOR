using KalendarzPracowniczyDomain.Entities.Users;
using MediatR;
using Microsoft.AspNetCore.Identity;

namespace KalendarzPracowniczyApplication.CQRS.Commands.Users.Logout
{
    public class LogOutCommandHandler : IRequestHandler<LogOutCommand>
    {
        private readonly SignInManager<User> _signInManager;

        public LogOutCommandHandler(SignInManager<User> signInManager)
        {
            _signInManager = signInManager;
        }

        public async Task Handle(LogOutCommand request, CancellationToken cancellationToken)
        {
            await _signInManager.SignOutAsync();
        }
    }
}