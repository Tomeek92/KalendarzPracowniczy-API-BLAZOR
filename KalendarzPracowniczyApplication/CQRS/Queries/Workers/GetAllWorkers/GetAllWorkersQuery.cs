using KalendarzPracowniczyApplication.Dto;
using MediatR;

namespace KalendarzPracowniczyApplication.CQRS.Queries.Workers.GetAllWorkers
{
    public class GetAllWorkersQuery : IRequest<IEnumerable<WorkerDto>>
    {

    }
}
