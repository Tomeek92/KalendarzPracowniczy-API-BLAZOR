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

        public async Task<User?> FindByUserNameAsync(string userName)
        {
            try
            {
                return await _userManager.FindByNameAsync(userName);
            }
            catch (ArgumentNullException ex)
            {
                throw new ArgumentNullException(nameof(userName), $"User nie może być nullem {ex.Message}");
            }
            catch (InvalidOperationException ex)
            {
                throw new InvalidOperationException($"Nieprawidłowa operacja {ex.Message}");
            }
            catch (Exception ex)
            {
                throw new Exception($"Nieoczekiwany błąd {ex.Message}");
            }
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
                await UserExist(user);
                var result = await _userManager.CreateAsync(user, password);
                if (result.Succeeded)
                {
                    await SaveAsync();
                }
            }
            catch (Exception ex)
            {
                throw new Exception($"Nieoczekiwany bląd podczas tworzenia użytkownika {ex.Message}");
            }
        }

        private async Task SaveAsync()
        {
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException ex)
            {
                throw new DbUpdateException($"Błąd podczas zapisu do bazy danyc {ex.Message}");
            }
            catch (Exception ex)
            {
                throw new Exception($"Nieoczekiwany błąd {ex.Message}");
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

        private async Task UserExist(User user)
        {
            bool exist = await _context.Users.AnyAsync(u => u.Email == user.Email || u.UserName == user.UserName);
            if (exist)
            {
                throw new ArgumentException($"Użytkownik o takim {user.Email} albo nazwie użytkownika {user.UserName} już istnieje");
            }
        }

        public async Task UpdateUser(User user)
        {
            try
            {
                var findUser = await GetUserById(user.Id);
                if (findUser == null)
                {
                    throw new Exception("Użytkownik nie istnieje lub został usunięty.");
                }

                UpdateUserData(findUser, user);

                if (!string.IsNullOrEmpty(user.Password))
                {
                    await ResetPasswordAsync(findUser, user.Password);
                }

                await SaveUserChangesAsync(findUser);
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

        private void UpdateUserData(User existingUser, User newUserData)
        {
            existingUser.Email = newUserData.Email;
            existingUser.Name = newUserData.Name;
            existingUser.UserName = newUserData.UserName;
        }

        private async Task ResetPasswordAsync(User user, string newPassword)
        {
            var resetToken = await _userManager.GeneratePasswordResetTokenAsync(user);

            var resetPasswordResult = await _userManager.ResetPasswordAsync(user, resetToken, newPassword);
            if (!resetPasswordResult.Succeeded)
            {
                var errors = string.Join(", ", resetPasswordResult.Errors.Select(e => e.Description));
                throw new Exception($"Błąd podczas resetowania hasła: {errors}");
            }
        }

        private async Task SaveUserChangesAsync(User user)
        {
            var updateResult = await _userManager.UpdateAsync(user);
            if (!updateResult.Succeeded)
            {
                var errors = string.Join(", ", updateResult.Errors.Select(e => e.Description));
                throw new Exception($"Aktualizacja danych użytkownika nie powiodła się: {errors}");
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