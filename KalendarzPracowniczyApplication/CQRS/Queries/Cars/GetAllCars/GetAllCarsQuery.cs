using KalendarzPracowniczyApplication.Dto;
using MediatR;

namespace KalendarzPracowniczyApplication.CQRS.Queries.Workers.GetAllWorkers
{
    public class GetAllCarsQuery : IRequest<IEnumerable<CarDto>>
    {

    }
}
