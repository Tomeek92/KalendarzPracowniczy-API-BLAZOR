using AutoMapper;
using KalendarzPracowniczyDomain.Entities.Workers;
using KalendarzPracowniczyDomain.Interfaces;
using MediatR;

namespace KalendarzPracowniczyApplication.CQRS.Commands.Workers.Create
{
    public class CreateWorkerCommandHandler : IRequestHandler<CreateWorkerCommand>
    {
        private readonly IMapper _mapper;
        private readonly IWorkerRepository _workerRepository;
        public CreateWorkerCommandHandler(IMapper mapper, IWorkerRepository workerRepository)
        {
            _mapper = mapper;
            _workerRepository = workerRepository;
        }
        public async Task Handle(CreateWorkerCommand request, CancellationToken cancellationToken)
        {
            try
            {
                var mapp = _mapper.Map<Worker>(request);
                await _workerRepository.Create(mapp);
            }
            catch (AutoMapperMappingException ex)
            {
                throw new AutoMapperMappingException($"Błąd przy mapowaniu", ex);
            }
        }
    }
}
