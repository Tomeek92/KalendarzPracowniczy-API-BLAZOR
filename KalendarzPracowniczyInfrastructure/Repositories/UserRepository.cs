using KalendarzPracowniczyApplication.Dto;
using KalendarzPracowniczyDomain.Entities.Users;
using KalendarzPracowniczyDomain.Interfaces;
using Microsoft.AspNetCore.Identity;

namespace KalendarzPracowniczyInfrastructure.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly UserManager<User> _userManager;
        private readonly SignInManager<User> _signInManager;

        public UserRepository(UserManager<User> userManager, SignInManager<User> signInManager)
        {
            _userManager = userManager;
            _signInManager = signInManager;
        }

        public async Task Login(LoginDto user)
        {
            try
            {
                var findUser = await _userManager.FindByNameAsync(user.UserName);
                if (findUser == null)
                {
                    throw new Exception($"Nie odnaleziono użytkownika w bazie danych");
                }
                var result = await _signInManager.PasswordSignInAsync(findUser.UserName, user.Password, isPersistent: false, lockoutOnFailure: false);
                if (result.Succeeded)
                {
                }
                else if (result.IsLockedOut)
                {
                    throw new Exception("Użytkownik jest zablokowany");
                }
                else
                {
                    throw new Exception("Nieprawidłowa próba logowania");
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