using KalendarzPracowniczyApplication.Dto;
using MediatR;

namespace KalendarzPracowniczyApplication.CQRS.Queries.Events.GetAll
{
    public class GetAllEventQuery : IRequest<IEnumerable<EventDto>>
    {
    }
}
