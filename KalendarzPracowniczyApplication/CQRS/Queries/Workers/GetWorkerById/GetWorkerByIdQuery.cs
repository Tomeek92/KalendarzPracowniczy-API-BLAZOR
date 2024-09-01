using KalendarzPracowniczyApplication.Dto;
using MediatR;

namespace KalendarzPracowniczyApplication.CQRS.Queries.Workers.GetWorkerById
{
    public class GetWorkerByIdQuery : IRequest<WorkerDto>
    {
        public Guid Id { get; set; }
        public GetWorkerByIdQuery(Guid id)
        {
            Id = id;
        }
    }
}
