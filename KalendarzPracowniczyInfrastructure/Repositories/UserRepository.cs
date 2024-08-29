using KalendarzPracowniczyDomain.Entities.Users;
using KalendarzPracowniczyDomain.Interfaces;
using Microsoft.AspNetCore.Identity;

namespace KalendarzPracowniczyInfrastructure.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly UserManager<User> _userManager;
        public UserRepository(UserManager<User> userManager)
        {
            _userManager = userManager;
        }
        public async Task CreateUser(User user, string password)
        {
            try
            {
                await _userManager.CreateAsync(user, password);
            }
            catch (Exception ex)
            {
                throw new Exception($"Nieoczekiwany bląd podczas tworzenia użytkownika");
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
