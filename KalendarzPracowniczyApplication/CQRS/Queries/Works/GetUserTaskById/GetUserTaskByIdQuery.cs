using KalendarzPracowniczyApplication.Dto;
using MediatR;

namespace KalendarzPracowniczyApplication.CQRS.Queries.Works.GetUserTaskById
{
    public class GetUserTaskByIdQuery : IRequest<List<WorkDto>>
    {
        public string UserId { get; set; }

        public GetUserTaskByIdQuery(string userid)
        {
            UserId = userid;
        }
    }
}