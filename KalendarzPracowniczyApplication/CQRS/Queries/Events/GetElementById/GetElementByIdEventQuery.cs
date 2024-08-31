using KalendarzPracowniczyApplication.Dto;
using MediatR;

namespace KalendarzPracowniczyApplication.CQRS.Queries.Events.GetElementById
{
    public class GetElementByIdEventQuery : IRequest<EventDto>
    {
        public Guid Id { get; set; }
        public GetElementByIdEventQuery(Guid id)
        {
            Id = id;
        }
    }
}
