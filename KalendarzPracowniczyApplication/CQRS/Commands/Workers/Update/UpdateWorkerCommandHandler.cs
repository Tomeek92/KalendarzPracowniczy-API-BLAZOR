using AutoMapper;
using KalendarzPracowniczyDomain.Entities.Workers;
using KalendarzPracowniczyDomain.Interfaces;
using MediatR;

namespace KalendarzPracowniczyApplication.CQRS.Commands.Workers.Update
{
    public class UpdateWorkerCommandHandler : IRequestHandler<UpdateWorkerCommand>
    {
        private readonly IMapper _mapper;
        private readonly IWorkerRepository _workerRepository;
        public UpdateWorkerCommandHandler(IMapper mapper, IWorkerRepository workerRepository)
        {
            _mapper = mapper;
            _workerRepository = workerRepository;
        }
        public async Task Handle(UpdateWorkerCommand request, CancellationToken cancellationToken)
        {
            try
            {
                var mapp = _mapper.Map<Worker>(request);
                await _workerRepository.Update(mapp);
            }
            catch (AutoMapperMappingException ex)
            {
                throw new AutoMapperMappingException($"Nieprawidłowe mapowanie", ex);
            }
        }
    }
}
