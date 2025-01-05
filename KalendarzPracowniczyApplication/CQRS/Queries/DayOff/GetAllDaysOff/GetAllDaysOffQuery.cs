using KalendarzPracowniczyApplication.Dto;
using MediatR;

namespace KalendarzPracowniczyApplication.CQRS.Queries.DayOff.GetAllDaysOff
{
    public class GetAllDaysOffQuery : IRequest<List<DayOffDto>>
    {
    }
}
