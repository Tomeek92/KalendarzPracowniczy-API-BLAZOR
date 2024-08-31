using KalendarzPracowniczyApplication.Dto;
using MediatR;

namespace KalendarzPracowniczyApplication.CQRS.Commands.Users.Update
{
    public class UpdateUserCommand : UserDto, IRequest
    {
    }
}
