using KalendarzPracowniczyApplication.Dto;
using MediatR;

namespace KalendarzPracowniczyApplication.CQRS.Commands.DayOff.Update
{
    public class UpdateDayOffCommand : DayOffDto, IRequest
    {
    }
}