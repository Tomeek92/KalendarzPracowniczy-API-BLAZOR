using AutoMapper;
using KalendarzPracowniczyApplication.Dto;
using KalendarzPracowniczyDomain.Interfaces;
using MediatR;
using Microsoft.AspNetCore.Http;
using System.Security.Claims;

namespace KalendarzPracowniczyApplication.CQRS.Queries.Users.LoggedUser
{
    public class LoggedUserQueryHandler : IRequestHandler<LoggedUserQuery, UserDto>
    {
        private readonly IMapper _mapper;
        private readonly IUserRepository _userRepository;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public LoggedUserQueryHandler(IUserRepository userRepository, IMapper mapper, IHttpContextAccessor httpContextAccessor)
        {
            _mapper = mapper;
            _userRepository = userRepository;
            _httpContextAccessor = httpContextAccessor;
        }

        public async Task<UserDto> Handle(LoggedUserQuery request, CancellationToken cancellationToken)
        {
            try
            {
                var userId = _httpContextAccessor.HttpContext.User.FindFirstValue(ClaimTypes.NameIdentifier);

                if (userId == null)
                {
                    throw new Exception($"Nie znaleziono użytkownika");
                }

                var user = await _userRepository.GetUserById(userId);

                if (user == null)
                {
                    throw new Exception($"Nie znaleziono użytkownika");
                }
                var userDto = _mapper.Map<UserDto>(user);
                return userDto;
            }
            catch (AutoMapperMappingException ex)
            {
                throw new AutoMapperMappingException($"Błąd podczas mapowania {ex.Message}");
            }
        }
    }
}