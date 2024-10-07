using AutoMapper;
using KalendarzPracowniczyApplication.Dto;
using KalendarzPracowniczyDomain.Interfaces;
using MediatR;

namespace KalendarzPracowniczyApplication.CQRS.Queries.Workers.GetAllWorkers
{
    public class GetAllCarsHandlerQuery : IRequestHandler<GetAllCarsQuery, IEnumerable<CarDto>>
    {
        private readonly IMapper _mapper;
        private readonly ICarRepository _workerRepository;

        public GetAllCarsHandlerQuery(IMapper mapper, ICarRepository workerRepository)
        {
            _mapper = mapper;
            _workerRepository = workerRepository;
        }

        public async Task<IEnumerable<CarDto>> Handle(GetAllCarsQuery request, CancellationToken cancellationToken)
        {
            try
            {
                var workers = await _workerRepository.GetAllWorkers();
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