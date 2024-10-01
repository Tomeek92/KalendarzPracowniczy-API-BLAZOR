using KalendarzPracowniczyApplication.CQRS.Commands.Users.Create;
using KalendarzPracowniczyApplication.CQRS.Commands.Users.Delete;
using KalendarzPracowniczyApplication.CQRS.Commands.Users.Logout;
using KalendarzPracowniczyApplication.CQRS.Commands.Users.Update;
using KalendarzPracowniczyApplication.CQRS.Queries.Users.GetUserById;
using KalendarzPracowniczyApplication.CQRS.Queries.Users.LoggedUser;
using KalendarzPracowniczyApplication.CQRS.Queries.Users.Login;
using KalendarzPracowniczyApplication.Dto;
using MediatR;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace KalendarzPracowniczyAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UserController : ControllerBase
    {
        private readonly IMediator _mediator;

        public UserController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost("logout")]
        public async Task<IActionResult> Logout()
        {
            await _mediator.Send(new LogOutCommand());
            return Ok();
        }

        [HttpGet("me")]
        public async Task<IActionResult> GetCurrentUser()
        {
            var userDto = await _mediator.Send(new LoggedUserQuery());

            if (userDto == null)
            {
                return Unauthorized(new { message = "Nie jesteś zalogowany." });
            }

            return Ok(userDto);
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginQuery command)
        {
            try
            {
                var userDto = await _mediator.Send(command);

                if (userDto == null)
                {
                    Console.WriteLine("Nieprawidłowe dane logowania.");
                    return Unauthorized(new { message = "Nieprawidłowe dane logowania." });
                }

                var claims = new List<Claim>
        {
            new Claim(ClaimTypes.Name, command.UserName),
            new Claim(ClaimTypes.NameIdentifier, userDto.Id.ToString()),
            new Claim("UserName", command.UserName),
            new Claim("Surname", userDto.Surname)
        };

                var claimsIdentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
                var authProperties = new AuthenticationProperties
                {
                    IsPersistent = true,
                };
                Console.WriteLine("Przed SignInAsync");
                await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, new ClaimsPrincipal(claimsIdentity), authProperties);
                Console.WriteLine("Po SignInAsync - Zalogowano");
                return Ok(userDto);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Wystąpił błąd: {ex.Message}");
                return StatusCode(500, new { message = $"Nieoczekiwany błąd: {ex.Message}" });
            }
        }

        [HttpPost("Create")]
        public async Task<IActionResult> Create([FromBody] CreateUserCommand userCommand)
        {
            try
            {
                await _mediator.Send(userCommand);
                return Ok();
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error {ex}");
            }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(string id)
        {
            try
            {
                var command = new DeleteUserCommand(id);
                await _mediator.Send(command);
                return Ok();
            }
            catch (KeyNotFoundException ex)
            {
                return NotFound($"Nie odnaleziono użytkownika do usunięcia");
            }
            catch (Exception ex)
            {
                return StatusCode(500, "Internal server error");
            }
        }

        [HttpPut]
        public async Task<IActionResult> Update([FromBody] UpdateUserCommand userUpdateCommand)
        {
            try
            {
                await _mediator.Send(userUpdateCommand);
                return Ok();
            }
            catch (KeyNotFoundException ex)
            {
                return NotFound($"Nie odnaleziono użytkownika");
            }
            catch (Exception)
            {
                return StatusCode(500, "Internal server error");
            }
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetUserById(string id)
        {
            try
            {
                var query = new GetUserByIdQuery(id);
                var result = await _mediator.Send(query);
                return Ok(result);
            }
            catch (KeyNotFoundException ex)
            {
                return NotFound($"Nie odnaleziono użytkownika");
            }
            catch (Exception ex)
            {
                return StatusCode(500, "Internal server error");
            }
        }
    }
}