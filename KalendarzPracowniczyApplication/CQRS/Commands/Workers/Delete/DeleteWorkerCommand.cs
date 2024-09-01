using KalendarzPracowniczyApplication.Dto;
using MediatR;

namespace KalendarzPracowniczyApplication.CQRS.Commands.Workers.Delete
{
    public class DeleteWorkerCommand : WorkerDto, IRequest
    {
        public Guid Id { get; set; }
        public DeleteWorkerCommand(Guid id)
        {
            Id = id;
        }
    }
}
