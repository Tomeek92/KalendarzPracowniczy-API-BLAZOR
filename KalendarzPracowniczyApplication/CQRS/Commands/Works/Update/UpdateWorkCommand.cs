using KalendarzPracowniczyApplication.Dto;
using MediatR;

namespace KalendarzPracowniczyApplication.CQRS.Commands.Works.Update
{
    public class UpdateWorkCommand : WorkDto, IRequest
    {
    }
}