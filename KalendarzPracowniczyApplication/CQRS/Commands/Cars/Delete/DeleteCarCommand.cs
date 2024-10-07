using KalendarzPracowniczyApplication.Dto;
using MediatR;

namespace KalendarzPracowniczyApplication.CQRS.Commands.Workers.Delete
{
    public class DeleteCarCommand : CarDto, IRequest
    {
        public Guid Id { get; set; }
        public DeleteCarCommand(Guid id)
        {
            Id = id;
        }
    }
}
