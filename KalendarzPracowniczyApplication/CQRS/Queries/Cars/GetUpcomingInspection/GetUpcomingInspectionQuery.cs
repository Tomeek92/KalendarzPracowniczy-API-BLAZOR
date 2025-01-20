using KalendarzPracowniczyApplication.Dto;
using MediatR;

namespace KalendarzPracowniczyApplication.CQRS.Queries.Cars.GetUpcomingInspection
{
    public class GetUpcomingInspectionQuery : IRequest<List<CarDto>>
    {
    }
}
