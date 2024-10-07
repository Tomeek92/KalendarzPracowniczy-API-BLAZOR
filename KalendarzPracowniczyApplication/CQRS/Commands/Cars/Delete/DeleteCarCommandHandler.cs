using AutoMapper;
using KalendarzPracowniczyDomain.Interfaces;
using MediatR;

namespace KalendarzPracowniczyApplication.CQRS.Commands.Workers.Delete
{
    public class DeleteCarCommandHandler : IRequestHandler<DeleteCarCommand>
    {
        private readonly IMapper _mapper;
        private readonly ICarRepository _workerRepository;
        public DeleteCarCommandHandler(IMapper mapper, ICarRepository workerRepository)
        {
            _mapper = mapper;
            _workerRepository = workerRepository;
        }

        public async Task Handle(DeleteCarCommand request, CancellationToken cancellationToken)
        {
            await _workerRepository.Delete(request.Id);
        }
    }
}
