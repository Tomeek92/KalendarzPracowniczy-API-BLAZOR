using AutoMapper;
using KalendarzPracowniczyApplication.Dto;
using KalendarzPracowniczyDomain.Entities.Users;
using KalendarzPracowniczyDomain.Interfaces;
using MediatR;
using Microsoft.AspNetCore.Identity;

namespace KalendarzPracowniczyApplication.CQRS.Queries.Users.Login
{
    public class LoginQueryHandler : IRequestHandler<LoginQuery, LoginDto>
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

        public async Task<LoginDto> Handle(LoginQuery request, CancellationToken cancellationToken)
        {
            try
            {
                var userMap = _mapper.Map<User>(request);
                if (request == null || string.IsNullOrEmpty(request.UserName) || string.IsNullOrEmpty(request.Password))
                {
                    throw new Exception("Niepoprawne dane logowania.");
                }
                var user = await _userRepository.FindByUserNameAsync(request.UserName);

                if (user == null)
                {
                    throw new Exception("nie znaleziono użytkownika");
                }

                var result = await _userRepository.Login(request.UserName, request.Password);
                if (!result.Succeeded)
                {
                    throw new Exception("Nieprawidłowe dane logowania");
                }

                var userDto = _mapper.Map<LoginDto>(user);
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