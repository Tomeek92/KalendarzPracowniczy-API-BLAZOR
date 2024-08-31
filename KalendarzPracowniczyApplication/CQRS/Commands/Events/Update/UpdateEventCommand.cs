using KalendarzPracowniczyApplication.Dto;
using MediatR;

namespace KalendarzPracowniczyApplication.CQRS.Commands.Events.Update
{
    public class UpdateEventCommand : EventDto, IRequest
    {
    }
}
