using KalendarzPracowniczyApplication.Dto;
using MediatR;

namespace KalendarzPracowniczyApplication.CQRS.Queries.DayOff.GetDayOffById
{
    public class GetDayOffByIdQuery : IRequest<List<DayOffDto>>
    {
        public string UserId { get; set; }

        public GetDayOffByIdQuery(string userId)
        {
            UserId = userId;
        }
    }
}