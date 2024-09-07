using AutoMapper;
using KalendarzPracowniczyApplication.Dto;
using KalendarzPracowniczyDomain.Entities.Users;
using KalendarzPracowniczyDomain.Interfaces;
using MediatR;
using Microsoft.AspNetCore.Identity;

namespace KalendarzPracowniczyApplication.CQRS.Commands.Users.Login
{
    public class LoginCommandHandler : IRequestHandler<LoginCommand, UserDto>
    {
        private readonly IMapper _mapper;
        private readonly IUserRepository _userRepository;
        private readonly SignInManager<User> _signInManager;

        public LoginCommandHandler(IUserRepository userRepository, IMapper mapper, SignInManager<User> signInManager)
        {
            _mapper = mapper;
            _userRepository = userRepository;
            _signInManager = signInManager;
        }

        public async Task<UserDto> Handle(LoginCommand request, CancellationToken cancellationToken)
        {
            try
            {
                var user = _mapper.Map<User>(request);

                var existingUser = await _userRepository.FindByEmailAsync(user.Email);

                if (existingUser == null)
                {
                    throw new Exception("Nie znaleziono użytkownika");
                }

                var loginResult = await _signInManager.PasswordSignInAsync(request.UserName, request.Password, isPersistent: false, lockoutOnFailure: false);
                if (!loginResult.Succeeded)
                {
                    throw new Exception("Nieprawidłowe dane logowania");
                }
                var userDto = _mapper.Map<UserDto>(existingUser);
                return userDto;
            }
            catch (AutoMapperMappingException ex)
            {
                throw new AutoMapperMappingException($"Błąd podczas mapowania {ex.Message}");
            }
        }
    }
}