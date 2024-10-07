using AutoMapper;
using KalendarzPracowniczyDomain.Entities.Cars;
using KalendarzPracowniczyDomain.Interfaces;
using MediatR;

namespace KalendarzPracowniczyApplication.CQRS.Commands.Workers.Update
{
    public class UpdateCarCommandHandler : IRequestHandler<UpdateCarCommand>
    {
        private readonly IMapper _mapper;
        private readonly ICarRepository _carRepository;

        public UpdateCarCommandHandler(IMapper mapper, ICarRepository workerRepository)
        {
            _mapper = mapper;
            _carRepository = workerRepository;
        }

        public async Task Handle(UpdateCarCommand request, CancellationToken cancellationToken)
        {
            try
            {
                var mapp = _mapper.Map<Car>(request);
                await _carRepository.Update(mapp);
            }
            catch (AutoMapperMappingException ex)
            {
                throw new AutoMapperMappingException($"Nieprawidłowe mapowanie", ex);
            }
        }
    }
}