using KalendarzPracowniczyApplication.Dto;
using MediatR;

namespace KalendarzPracowniczyApplication.CQRS.Queries.Works.GetUserTasks
{
    public class GetUserTasksQuery : IRequest<List<WorkDto>>
    {
    }
}