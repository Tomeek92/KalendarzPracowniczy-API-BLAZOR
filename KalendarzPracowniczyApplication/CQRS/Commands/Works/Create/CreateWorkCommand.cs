using KalendarzPracowniczyApplication.Dto;
using MediatR;

namespace KalendarzPracowniczyApplication.CQRS.Commands.Works.Create
{
    public class CreateWorkCommand : WorkDto, IRequest
    {
    }
}