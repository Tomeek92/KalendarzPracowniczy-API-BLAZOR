using AutoMapper;
using KalendarzPracowniczyDomain.Interfaces;
using MediatR;

namespace KalendarzPracowniczyApplication.CQRS.Commands.Workers.Delete
{
    public class DeleteWorkerCommandHandler : IRequestHandler<DeleteWorkerCommand>
    {
        private readonly IMapper _mapper;
        private readonly IWorkerRepository _workerRepository;
        public DeleteWorkerCommandHandler(IMapper mapper, IWorkerRepository workerRepository)
        {
            _mapper = mapper;
            _workerRepository = workerRepository;
        }

        public async Task Handle(DeleteWorkerCommand request, CancellationToken cancellationToken)
        {
            await _workerRepository.Delete(request.Id);
        }
    }
}
