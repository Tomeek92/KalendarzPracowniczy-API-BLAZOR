using KalendarzPracowniczyApplication.Dto;
using MediatR;

namespace KalendarzPracowniczyApplication.CQRS.Commands.Events.Create
{
    public class CreateEventCommand : EventDto, IRequest
    {

    }
}
