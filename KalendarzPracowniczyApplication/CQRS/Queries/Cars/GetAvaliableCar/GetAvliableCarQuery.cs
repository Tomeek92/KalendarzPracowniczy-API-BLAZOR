using KalendarzPracowniczyApplication.Dto;
using MediatR;

namespace KalendarzPracowniczyApplication.CQRS.Queries.Cars.GetAvaliableCar
{
    public class GetAvliableCarQuery : IRequest<List<CarDto>>
    {
        public DateTime Year { get; set; }
    }
}