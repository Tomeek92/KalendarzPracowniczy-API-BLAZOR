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
                var userName = _httpContextAccessor.HttpContext?.User?.FindFirstValue(ClaimTypes.Name);
                var surName = _httpContextAccessor.HttpContext?.User?.FindFirstValue(ClaimTypes.Surname);

                if (string.IsNullOrEmpty(userId))
                {
                    throw new UnauthorizedAccessException("Nie znaleziono zalogowanego użytkownika.");
                }

                var userDto = new UserDto
                {
                    Id = userId,
                    UserName = userName,
                    Surname = surName
                };
                return userDto;
            }
            catch (AutoMapperMappingException ex)
            {
                throw new AutoMapperMappingException($"Błąd podczas mapowania {ex.Message}");
            }
        }
    }
}