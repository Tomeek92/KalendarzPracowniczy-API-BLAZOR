using KalendarzPracowniczyDomain.Entities.Users;
using KalendarzPracowniczyDomain.Interfaces;
using KalendarzPracowniczyInfrastructureDbContext;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace KalendarzPracowniczyInfrastructure.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly UserManager<User> _userManager;
        private readonly SignInManager<User> _signInManager;
        private readonly KalendarzPracowniczyDbContext _context;

        public UserRepository(UserManager<User> userManager, SignInManager<User> signInManager, KalendarzPracowniczyDbContext context)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _context = context;
        }

        public async Task<User?> FindByUserEmailAsync(string email)
        {
            return await _userManager.FindByEmailAsync(email);
        }

        public async Task<SignInResult> Login(string userName, string password)
        {
            try
            {
                var result = await _signInManager.PasswordSignInAsync(userName, password, isPersistent: false, lockoutOnFailure: false);
                if (result.Succeeded)
                {
                    return SignInResult.Success;
                }
                else
                {
                    return SignInResult.Failed;
                }
            }
            catch (Exception ex)
            {
                throw new Exception($"Błąd podczas logowania: {ex.Message}");
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

        public async Task<List<User>> GetAllUsers()
        {
            try
            {
                var allUsers = await _userManager.Users.ToListAsync();
                if (allUsers == null)
                {
                    throw new Exception($"Nie znaleziono użytkowników");
                }
                return allUsers;
            }
            catch (Exception ex)
            {
                throw new Exception($"Nieoczekiwany błąd {ex.Message}");
            }
        }

        public async Task UpdateUser(User user)
        {
            try
            {
                var findUser = await _userManager.FindByIdAsync(user.Id);
                if (findUser == null)
                {
                    throw new Exception("Użytkownik nie istnieje lub został usunięty.");
                }

                findUser.Email = user.Email;
                findUser.Name = user.Name;
                findUser.UserName = user.UserName;

                if (!string.IsNullOrEmpty(user.Password))
                {
                    var resetToken = await _userManager.GeneratePasswordResetTokenAsync(findUser);

                    var resetPasswordResult = await _userManager.ResetPasswordAsync(findUser, resetToken, user.Password);
                    if (!resetPasswordResult.Succeeded)
                    {
                        throw new Exception($"Błąd podczas resetowania hasła: {string.Join(", ", resetPasswordResult.Errors.Select(e => e.Description))}");
                    }
                }
                var updateResult = await _userManager.UpdateAsync(findUser);
                if (!updateResult.Succeeded)
                {
                    throw new Exception($"Aktualizacja danych użytkownika nie powiodła się: {string.Join(", ", updateResult.Errors.Select(e => e.Description))}");
                }
            }
            catch (DbUpdateConcurrencyException)
            {
                throw new Exception("Dane użytkownika zostały zmodyfikowane lub usunięte przez inny proces.");
            }
            catch (Exception ex)
            {
                throw new Exception($"Nieoczekiwany błąd: {ex.Message}");
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