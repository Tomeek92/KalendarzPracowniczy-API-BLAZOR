using AutoMapper;
using KalendarzPracowniczyDomain.Entities.Cars;
using KalendarzPracowniczyDomain.Interfaces;
using MediatR;

namespace KalendarzPracowniczyApplication.CQRS.Commands.Workers.Create
{
    public class CreateCarCommandHandler : IRequestHandler<CreateCarCommand>
    {
        private readonly IMapper _mapper;
        private readonly ICarRepository _carRepository;

        public CreateCarCommandHandler(IMapper mapper, ICarRepository workerRepository)
        {
            _mapper = mapper;
            _carRepository = workerRepository;
        }

        public async Task Handle(CreateCarCommand request, CancellationToken cancellationToken)
        {
            try
            {
                var mapp = _mapper.Map<Car>(request);
                await _carRepository.Create(mapp);
            }
            catch (AutoMapperMappingException ex)
            {
                throw new AutoMapperMappingException($"Błąd przy mapowaniu", ex);
            }
        }
    }
}