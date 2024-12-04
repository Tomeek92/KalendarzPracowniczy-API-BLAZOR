using KalendarzPracowniczyApplication.Dto;
using MediatR;

namespace KalendarzPracowniczyApplication.CQRS.Commands.DayOff.Create
{
    public class CreateDayOffCommand : DayOffDto, IRequest
    {
    }
}