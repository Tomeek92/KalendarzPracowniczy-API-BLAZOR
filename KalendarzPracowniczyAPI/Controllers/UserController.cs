using KalendarzPracowniczyApplication.Dto;
using KalendarzPracowniczyApplication.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace KalendarzPracowniczyAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UserController : ControllerBase
    {
        private readonly IUserService _userService;
        public UserController(IUserService userService)
        {
            _userService = userService;
        }
        [HttpPost]
        public async Task Create([FromBody] UserDto userDto, string password)
        {
            await _userService.Create(userDto, password);
        }
        [HttpDelete("{id}")]
        public async Task Delete(string id)
        {
            await _userService.Delete(id);
        }
        [HttpPut]
        public async Task Update([FromBody] UserDto userDto)
        {
            await _userService.Update(userDto);
        }
        [HttpGet("{id}")]
        public async Task<UserDto> GetUserById(string id)
        {
            var findUser = await _userService.GetUserById(id);
            return findUser;
        }
    }
}
