using KalendarzPracowniczyDomain.Entities.Users;
using MediatR;
using Microsoft.AspNetCore.Identity;

namespace KalendarzPracowniczyApplication.CQRS.Commands.Users.LogIn
{
    internal class LoginUserCommandHandler : IRequestHandler<LoginUserCommand, Result>
    {
        private readonly SignInManager<User> _signInManager;

        public LoginUserCommandHandler(SignInManager<User> signInManager)
        {
            _signInManager = signInManager;
        }

        public async Task<Result> Handle(LoginUserCommand request, CancellationToken cancellationToken)
        {
            try
            {
                var result = await _signInManager.PasswordSignInAsync(request.Password, request.Email, request.RememberMe, lockoutOnFailure: false);
                if (result.Succeeded)
                {
                    return new Result { Success = true };
                }
                else if (result.IsLockedOut)
                {
                    return new Result { Success = false, ErrorMessage = "Użytkownik jest zablokowany" };
                }
                else if (result.RequiresTwoFactor)
                {
                    return new Result { Success = false, ErrorMessage = "Potrzebna weryfikacja dwuetapowa" };
                }
                else
                {
                    return new Result { Success = false, ErrorMessage = "Invalid login attempt." };
                }
            }
            catch (Exception ex)
            {
                throw new Exception($"{ex.Message}");
            }
        }
    }
}