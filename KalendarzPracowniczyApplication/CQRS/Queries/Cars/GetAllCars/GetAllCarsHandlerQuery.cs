using AutoMapper;
using KalendarzPracowniczyApplication.Dto;
using KalendarzPracowniczyDomain.Interfaces;
using MediatR;

namespace KalendarzPracowniczyApplication.CQRS.Queries.Workers.GetAllWorkers
{
    public class GetAllCarsHandlerQuery : IRequestHandler<GetAllCarsQuery, IEnumerable<CarDto>>
    {
        private readonly IMapper _mapper;
        private readonly ICarRepository _carRepository;

        public GetAllCarsHandlerQuery(IMapper mapper, ICarRepository carRepository)
        {
            _mapper = mapper;
            _carRepository = carRepository;
        }

        public async Task<IEnumerable<CarDto>> Handle(GetAllCarsQuery request, CancellationToken cancellationToken)
        {
            try
            {
                var workers = await _carRepository.GetAllCars();
                var mapp = _mapper.Map<IEnumerable<CarDto>>(workers);
                return mapp;
            }
            catch (AutoMapperMappingException ex)
            {
                throw new AutoMapperMappingException($"Nieprawidłowe mapowanie", ex);
            }
        }
    }
}