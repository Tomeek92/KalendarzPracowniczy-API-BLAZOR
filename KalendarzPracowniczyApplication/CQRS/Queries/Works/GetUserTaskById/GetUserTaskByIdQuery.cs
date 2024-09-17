using KalendarzPracowniczyApplication.Dto;
using MediatR;

namespace KalendarzPracowniczyApplication.CQRS.Queries.Works.GetUserTaskById
{
    public class GetUserTaskByIdQuery : IRequest<WorkDto>
    {
        public Guid Id { get; set; }

        public GetUserTaskByIdQuery(Guid id)
        {
            Id = id;
        }
    }
}