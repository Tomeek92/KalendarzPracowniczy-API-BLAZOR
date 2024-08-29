using KalendarzPracowniczyDomain.Entities.Users;

namespace KalendarzPracowniczyDomain.Interfaces
{
    public interface IUserRepository
    {
        Task CreateUser(User user, string password);
        Task<User> GetUserById(string id);
        Task UpdateUser(User user);
        Task Delete(string id);
    }
}
