using KalendarzPracowniczyApplication.Dto;
using MediatR;

namespace KalendarzPracowniczyApplication.CQRS.Queries.Workers.GetWorkerById
{
    public class GetCarByIdQuery : IRequest<CarDto>
    {
        public Guid Id { get; set; }
        public GetCarByIdQuery(Guid id)
        {
            Id = id;
        }
    }
}
