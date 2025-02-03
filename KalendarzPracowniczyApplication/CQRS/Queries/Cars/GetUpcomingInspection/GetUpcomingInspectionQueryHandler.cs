using AutoMapper;
using KalendarzPracowniczyApplication.Dto;
using KalendarzPracowniczyDomain.Interfaces;
using MediatR;

namespace KalendarzPracowniczyApplication.CQRS.Queries.Cars.GetUpcomingInspection
{
    public class GetUpcomingInspectionQueryHandler : IRequestHandler<GetUpcomingInspectionQuery, List<CarDto>>
    {
        private readonly ICarRepository _carRepository;
        private readonly IMapper _mapper;
        public GetUpcomingInspectionQueryHandler(ICarRepository repository, IMapper mapper)
        {
            _carRepository = repository;
            _mapper = mapper;
        }
        public async Task<List<CarDto>> Handle(GetUpcomingInspectionQuery request, CancellationToken cancellationToken)
        {
            try
            {
                var cars = await _carRepository.GetAllCars();
                var mapp = _mapper.Map<List<CarDto>>(cars);
                var today = DateOnly.FromDateTime(DateTime.Now);
                return mapp
                  .Where(car => car.CarInspection.HasValue
                  && car.CarInspection.Value >= today
                  && car.CarInspection.Value <= today.AddDays(7))
                 .Select(car => new CarDto
                 {
                     Id = car.Id,
                     Name = car.Name,
                     CarPlatesNumber = car.CarPlatesNumber,
                     CarInspection = car.CarInspection
                 })
                 .ToList();
            }
            catch (AutoMapperMappingException ex)
            {
                throw new AutoMapperMappingException($"Błąd mapowania {ex.Message}");
            }
            catch (Exception ex)
            {
                throw new Exception($"Nieoczekiwany błąd {ex.Message}");
            }
        }
    }
}
