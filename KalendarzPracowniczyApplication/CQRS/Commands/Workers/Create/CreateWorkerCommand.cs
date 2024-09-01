using KalendarzPracowniczyApplication.Dto;
using MediatR;

namespace KalendarzPracowniczyApplication.CQRS.Commands.Workers.Create
{
    public class CreateWorkerCommand : WorkerDto, IRequest
    {
    }
}
