using KalendarzPracowniczyDomain.Entities.Users;
using MediatR;
using Microsoft.AspNetCore.Identity;

namespace KalendarzPracowniczyApplication.CQRS.Commands.Users.Login
{
    public class LoginCommandHandler : IRequestHandler<LoginCommand>
    {
        private readonly SignInManager<User> _signInManager;
        private readonly UserManager<User> _userManager;

        public LoginCommandHandler(SignInManager<User> signInManager, UserManager<User> userManager)
        {
            _signInManager = signInManager;
            _userManager = userManager;
        }

        public async Task Handle(LoginCommand request, CancellationToken cancellationToken)
        {
            try
            {
                var user = await _userManager.FindByNameAsync(request.UserName);
                if (user == null)
                {
                    throw new Exception("Nie odnaleziono użytkownika");
                }
                var result = await _signInManager.PasswordSignInAsync(user.UserName, request.Password, isPersistent: false, lockoutOnFailure: false);
            }
            catch (Exception ex)
            {
                throw new Exception($"Nieoczekiwany błąd {ex.Message}");
            }
        }
    }
}