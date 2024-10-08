using AutoMapper;
using KalendarzPracowniczyApplication.Dto;
using KalendarzPracowniczyDomain.Interfaces;
using MediatR;

namespace KalendarzPracowniczyApplication.CQRS.Queries.Workers.GetWorkerById
{
    public class GetCarByIdQueryHandler : IRequestHandler<GetCarByIdQuery, CarDto>
    {
        private readonly IMapper _mapper;
        private readonly ICarRepository _carRepository;

        public GetCarByIdQueryHandler(IMapper mapper, ICarRepository workerRepository)
        {
            _mapper = mapper;
            _carRepository = workerRepository;
        }

        public async Task<CarDto> Handle(GetCarByIdQuery request, CancellationToken cancellationToken)
        {
            try
            {
                var findId = await _carRepository.GetElementById(request.Id);
                var mapp = _mapper.Map<CarDto>(findId);
                return mapp;
            }
            catch (AutoMapperMappingException ex)
            {
                throw new AutoMapperMappingException($"Błąd przy mapowaniu", ex);
            }
        }
    }
}