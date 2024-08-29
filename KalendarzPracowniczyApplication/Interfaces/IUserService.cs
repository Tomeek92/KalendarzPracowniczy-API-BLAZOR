using KalendarzPracowniczyApplication.Dto;

namespace KalendarzPracowniczyApplication.Interfaces
{
    public interface IUserService
    {
        Task Create(UserDto userDto, string password);
        Task Delete(string id);
        Task Update(UserDto userDto);
        Task<UserDto> GetUserById(string id);
    }
}
