using KalendarzPracowniczyDomain.Entities.Users;
using KalendarzPracowniczyDomain.Interfaces;
using KalendarzPracowniczyInfrastructureDbContext;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace KalendarzPracowniczyInfrastructure.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly UserManager<User> _userManager;
        private readonly SignInManager<User> _signInManager;
        private readonly KalendarzPracowniczyDbContext _context;
        private readonly ILogger<AuthenticationService> _logger;

        public UserRepository(UserManager<User> userManager, SignInManager<User> signInManager, KalendarzPracowniczyDbContext context, ILogger<AuthenticationService> logger)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _context = context;
            _logger = logger;
        }

        public async Task<User?> FindByEmailAsync(string email)
        {
            return await _context.Users.FirstOrDefaultAsync(x => x.Email == email);
        }

        public async Task<string> LoginAsync(string userName, string password)
        {
            try
            {
                var result = await _signInManager.PasswordSignInAsync(userName, password, isPersistent: false, lockoutOnFailure: false);

                if (result.Succeeded)
                {
                    return "Login successful!";
                }
                else if (result.IsLockedOut)
                {
                    _logger.LogWarning("User account locked out.");
                    return "Your account is locked out.";
                }
                else if (result.IsNotAllowed)
                {
                    _logger.LogWarning("User is not allowed to sign in.");
                    return "Sign-in is not allowed.";
                }
                else
                {
                    _logger.LogWarning("Invalid login attempt.");
                    return "Invalid login attempt.";
                }
            }
            catch (Exception ex)
            {
                _logger.LogError($"Nieoczekiwany błąd podczas logowania");

                return "Błąd podczas logowania! Spróbuj później";
            }
        }

        public async Task CreateUser(User user, string password)
        {
            try
            {
                await _userManager.CreateAsync(user, password);
            }
            catch (Exception ex)
            {
                throw new Exception($"Nieoczekiwany bląd podczas tworzenia użytkownika {ex}");
            }
        }

        public async Task<User> GetUserById(string id)
        {
            try
            {
                var findUser = await _userManager.FindByIdAsync(id);
                if (findUser == null)
                {
                    throw new KeyNotFoundException($"Nieznaleziono użytkownika o podanym Id");
                }
                return findUser;
            }
            catch (Exception ex)
            {
                throw new Exception($"Nieoczekiwany błąd podczas pobierania Id użytkownika", ex);
            }
        }

        public async Task UpdateUser(User user)
        {
            try
            {
                var findUser = await _userManager.FindByIdAsync(user.Id);
                if (findUser == null)
                {
                    throw new KeyNotFoundException($"Nie znaleziono użytkownika w bazie danych");
                }
                await _userManager.UpdateAsync(user);
            }
            catch (Exception ex)
            {
                throw new Exception($"Nieoczekiwany błąd podczas aktulizacji danych użytkownika");
            }
        }

        public async Task Delete(string id)
        {
            try
            {
                var findUser = await _userManager.FindByIdAsync(id);
                if (findUser == null)
                {
                    throw new KeyNotFoundException($"Nie znaleziono użytkownika w bazie danych");
                }
                await _userManager.DeleteAsync(findUser);
            }
            catch (Exception ex)
            {
                throw new Exception($"Nieoczekiwany błąd podczas usuwania użytkownika", ex);
            }
        }
    }
}