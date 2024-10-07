using KalendarzPracowniczyApplication.Dto;
using MediatR;

namespace KalendarzPracowniczyApplication.CQRS.Commands.Workers.Create
{
    public class CreateCarCommand : CarDto, IRequest
    {
    }
}
