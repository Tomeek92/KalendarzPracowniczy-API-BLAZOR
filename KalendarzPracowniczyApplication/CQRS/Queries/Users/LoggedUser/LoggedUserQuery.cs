using KalendarzPracowniczyApplication.Dto;
using MediatR;

namespace KalendarzPracowniczyApplication.CQRS.Queries.Users.LoggedUser
{
    public class LoggedUserQuery : IRequest<UserDto>
    {
    }
}