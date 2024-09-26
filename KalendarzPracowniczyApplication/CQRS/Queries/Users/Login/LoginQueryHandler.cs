using AutoMapper;
using KalendarzPracowniczyApplication.Dto;
using KalendarzPracowniczyDomain.Entities.Users;
using KalendarzPracowniczyDomain.Interfaces;
using MediatR;
using Microsoft.AspNetCore.Identity;

namespace KalendarzPracowniczyApplication.CQRS.Queries.Users.Login
{
    public class LoginQueryHandler : IRequestHandler<LoginQuery, UserDto>
    {
        private readonly IUserRepository _userRepository;
        private readonly IMapper _mapper;
        private readonly SignInManager<User> _signInManager;

        public LoginQueryHandler(IUserRepository userRepository, IMapper mapper, SignInManager<User> signInManager)
        {
            _userRepository = userRepository;
            _mapper = mapper;
            _signInManager = signInManager;
        }

        public async Task<UserDto> Handle(LoginQuery request, CancellationToken cancellationToken)
        {
            try
            {
                var userMap = _mapper.Map<User>(request);
                if (request == null || string.IsNullOrEmpty(request.Email) || string.IsNullOrEmpty(request.Password))
                {
                    throw new Exception("Niepoprawne dane logowania.");
                }
                var user = await _userRepository.FindByUserEmailAsync(request.Email);

                if (user == null)
                {
                    throw new Exception("nie znaleziono użytkownika");
                }

                var result = await _signInManager.PasswordSignInAsync(request.UserName, request.Password, isPersistent: false, lockoutOnFailure: false);
                if (!result.Succeeded)
                {
                    throw new Exception("Nieprawidłowe dane logowania");
                }
                var userDto = _mapper.Map<UserDto>(user);
                return userDto;
            }
            catch (AutoMapperMappingException ex)
            {
                throw new AutoMapperMappingException($"Błąd podczas mapowania {ex.Message}");
            }
            catch (Exception ex)
            {
                throw new Exception($"Błąd podczas logoawania {ex.Message}");
            }
        }
    }
}