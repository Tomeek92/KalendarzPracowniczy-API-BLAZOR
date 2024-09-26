using KalendarzPracowniczyApplication.Dto;
using MediatR;

namespace KalendarzPracowniczyApplication.CQRS.Commands.Events.Create
{
    public class CreateEventCommand : EventDto, IRequest
    {
        public string UserDtoId { get; set; }
        public string CreateBy { get; set; }
    }
}