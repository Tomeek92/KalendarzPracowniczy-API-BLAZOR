using KalendarzPracowniczyApplication.Dto;
using MediatR;

namespace KalendarzPracowniczyApplication.CQRS.Commands.Works.Delete
{
    public class DeleteWorkCommand :WorkDto, IRequest
    {
        public Guid Id { get; set; }

        public DeleteWorkCommand(Guid id)
        {
            Id = id;
        }
    }
}