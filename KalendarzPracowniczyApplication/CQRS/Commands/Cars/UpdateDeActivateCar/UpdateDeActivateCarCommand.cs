using KalendarzPracowniczyApplication.Dto;
using MediatR;

namespace KalendarzPracowniczyApplication.CQRS.Commands.Cars.UpdateDeActivateCar
{
    public class UpdateDeActivateCarCommand : CarDto, IRequest
    {
        public Guid CarId { get; set; }
    }
}
