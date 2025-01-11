using AutoMapper;
using KalendarzPracowniczyApplication.Dto;
using KalendarzPracowniczyDomain.Interfaces;
using MediatR;

namespace KalendarzPracowniczyApplication.CQRS.Queries.Cars.GetAvailableCar
{
    public class GetAvailableCarQueryHandler : IRequestHandler<GetAvailableCarQuery, List<CarDto>>
    {
        private readonly ICarRepository _carRepository;
        private readonly IMapper _mapper;
        public GetAvailableCarQueryHandler(ICarRepository carRepository, IMapper mapper)
        {
            _carRepository = carRepository;
            _mapper = mapper;
        }
        public async Task<List<CarDto>> Handle(GetAvailableCarQuery request, CancellationToken cancellationToken)
        {
            try
            {
                var availableCar = await _carRepository.GetAvailableCarsAsync(request.DateCarBusy);

                var mapp = _mapper.Map<List<CarDto>>(availableCar);
                return mapp;
            }
            catch (AutoMapperMappingException ex)
            {
                throw new Exception($"Błąd mapowania {ex.Message}");
            }
            catch (Exception ex)
            {
                throw new Exception($"Nieoczekiwany błąd {ex.Message}");
            }
        }
    }
}
