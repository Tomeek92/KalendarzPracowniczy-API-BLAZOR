using AutoMapper;
using KalendarzPracowniczyApplication.Dto;
using KalendarzPracowniczyDomain.Interfaces;
using MediatR;

namespace KalendarzPracowniczyApplication.CQRS.Queries.Cars.GetAvaliableCar
{
    public class GetAvliableCarQueryHandler : IRequestHandler<GetAvliableCarQuery, List<CarDto>>
    {
        private readonly ICarRepository _carRepository;
        private readonly IMapper _mapper;

        public GetAvliableCarQueryHandler(ICarRepository carRepository, IMapper mapper)
        {
            _carRepository = carRepository;
            _mapper = mapper;
        }

        public async Task<List<CarDto>> Handle(GetAvliableCarQuery request, CancellationToken cancellationToken)
        {
            try
            {
                var availableCars = await _carRepository.GetAvailableCarsAsync(request.Year);

                var mapp = _mapper.Map<List<CarDto>>(availableCars);
                return mapp;
            }
            catch (AutoMapperMappingException ex)
            {
                throw new AutoMapperMappingException($"Błąd mapowania {ex.Message}");
            }
        }
    }
}