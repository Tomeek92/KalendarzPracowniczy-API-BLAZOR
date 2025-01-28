using KalendarzPracowniczyApplication.Dto;
using MediatR;

namespace KalendarzPracowniczyApplication.CQRS.Queries.Users.Login
{
    public class LoginQuery : IRequest<LoginDto>
    {
        public string Password { get; set; }
        public string UserName { get; set; }
    }
}