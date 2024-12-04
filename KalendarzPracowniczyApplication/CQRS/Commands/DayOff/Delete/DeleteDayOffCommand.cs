using KalendarzPracowniczyApplication.Dto;
using MediatR;

namespace KalendarzPracowniczyApplication.CQRS.Commands.DayOff.Delete
{
    public class DeleteDayOffCommand : DayOffDto, IRequest
    {
        public Guid Id { get; set; }

        public DeleteDayOffCommand(Guid id)
        {
            Id = id;
        }
    }
}