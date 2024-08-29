using AutoMapper;
using KalendarzPracowniczyApplication.Dto;
using KalendarzPracowniczyApplication.Interfaces;
using KalendarzPracowniczyDomain.Entities.Users;
using KalendarzPracowniczyDomain.Interfaces;

namespace KalendarzPracowniczyApplication.Service
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;
        private readonly IMapper _mapper;
        public UserService(IUserRepository userRepository, IMapper mapper)
        {
            _userRepository = userRepository;
            _mapper = mapper;
        }
        public async Task Create(UserDto userDto, string password)
        {
            try
            {
                var mapp = _mapper.Map<User>(userDto);
                await _userRepository.CreateUser(mapp, password);
            }
            catch (AutoMapperMappingException ex)
            {
                throw new AutoMapperMappingException($"Błąd mapowania", ex);
            }
        }
        public async Task Delete(string id)
        {
            await _userRepository.Delete(id);
        }
        public async Task Update(UserDto userDto)
        {
            try
            {
                var mapp = _mapper.Map<User>(userDto);
                await _userRepository.UpdateUser(mapp);
            }
            catch (AutoMapperMappingException ex)
            {
                throw new AutoMapperMappingException($"Błąd mapowania", ex);
            }
        }
        public async Task<UserDto> GetUserById(string id)
        {
            try
            {
                var existingUser = await _userRepository.GetUserById(id);
                var mapp = _mapper.Map<UserDto>(id);
                return mapp;
            }
            catch (AutoMapperMappingException ex)
            {
                throw new AutoMapperMappingException($"Błąd mapowania", ex);
            }
        }
    }
}
