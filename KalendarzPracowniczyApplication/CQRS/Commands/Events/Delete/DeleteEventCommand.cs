using KalendarzPracowniczyApplication.Dto;
using MediatR;

namespace KalendarzPracowniczyApplication.CQRS.Commands.Events.Delete
{
    public class DeleteEventCommand : EventDto, IRequest
    {
        public Guid Id { get; set; }
        public DeleteEventCommand(Guid id)
        {
            Id = id;
        }
    }
}
