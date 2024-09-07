using KalendarzPracowniczyDomain.Entities.Users;
using Microsoft.AspNetCore.Identity;

namespace KalendarzPracowniczyDomain.Interfaces
{
    public interface IUserRepository
    {
        Task CreateUser(User user, string password);

        Task<User> GetUserById(string id);

        Task UpdateUser(User user);

        Task Delete(string id);

        Task<User?> FindByEmailAsync(string email);

        Task<string> LoginAsync(string userName, string password);
    }
}