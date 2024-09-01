using KalendarzPracowniczyApplication.Dto;
using MediatR;

namespace KalendarzPracowniczyApplication.CQRS.Commands.Workers.Update
{
    public class UpdateWorkerCommand : WorkerDto, IRequest
    {

    }
}
