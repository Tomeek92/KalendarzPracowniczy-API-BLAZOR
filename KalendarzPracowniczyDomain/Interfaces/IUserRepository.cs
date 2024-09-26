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

        Task<User?> FindByUserEmailAsync(string? email);

        Task<SignInResult> Login(string userName, string password);
    }
}