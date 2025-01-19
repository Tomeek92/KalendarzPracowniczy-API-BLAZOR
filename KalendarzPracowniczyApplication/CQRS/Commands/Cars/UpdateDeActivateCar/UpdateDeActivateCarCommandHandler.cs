using AutoMapper;
using KalendarzPracowniczyApplication.Dto;
using KalendarzPracowniczyDomain.Entities.Cars;
using KalendarzPracowniczyDomain.Interfaces;
using MediatR;

namespace KalendarzPracowniczyApplication.CQRS.Commands.Cars.UpdateDeActivateCar
{
    public class UpdateDeActivateCarCommandHandler : IRequestHandler<UpdateDeActivateCarCommand>
    {
        private readonly IMapper _mapper;
        private readonly ICarRepository _carRepository;
        public UpdateDeActivateCarCommandHandler(IMapper mapper, ICarRepository carRepository)
        {
            _mapper = mapper;
            _carRepository = carRepository;
        }
        public async Task Handle(UpdateDeActivateCarCommand request, CancellationToken cancellationToken)
        {
            try
            {
                var mapp = _mapper.Map<Car>(request);
                var car = await _carRepository.GetElementById(request.Id);
                if (car == null)
                {
                    throw new Exception($"Nieodnaleziono samochodu");
                }
                if (!(car.IsActive ?? false))
                {
                    throw new Exception($"Samochód jest już dezaktywowany");
                }
                car.IsActive = mapp.IsActive;
                car.IsActive = false;
                await _carRepository.Update(car);
            }
            catch (AutoMapperMappingException ex)
            {
                throw new AutoMapperMappingException($"Błąd podczas mapowania {ex.Message}");
            }
            catch (Exception ex)
            {
                throw new Exception($"Nieoczekiwany błąd {ex.Message}");
            }
        }
    }
}
