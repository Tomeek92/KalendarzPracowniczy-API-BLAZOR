using KalendarzPracowniczyApplication.Dto;
using MediatR;

namespace KalendarzPracowniczyApplication.CQRS.Queries.Cars.GetAvailableCar
{
    public class GetAvailableCarQuery : IRequest<List<CarDto>>
    {
        public DateTime? DateCarBusy { get; set; }
    }
}
