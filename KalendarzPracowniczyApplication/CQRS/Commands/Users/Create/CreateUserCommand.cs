using KalendarzPracowniczyApplication.Dto;
using MediatR;

namespace KalendarzPracowniczyApplication.CQRS.Commands.Users.Create
{
    public class CreateUserCommand : UserDto, IRequest
    {

    }
}
