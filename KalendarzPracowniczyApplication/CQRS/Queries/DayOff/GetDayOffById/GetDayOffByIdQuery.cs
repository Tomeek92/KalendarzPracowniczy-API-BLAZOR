using KalendarzPracowniczyApplication.Dto;
using MediatR;

namespace KalendarzPracowniczyApplication.CQRS.Queries.DayOff.GetDayOffById
{
    public class GetDayOffByIdQuery : IRequest<DayOffDto>
    {
        public Guid Id { get; set; }

        public GetDayOffByIdQuery(Guid id)
        {
            Id = id;
        }
    }
}