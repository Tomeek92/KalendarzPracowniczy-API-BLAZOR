using AutoMapper;
using KalendarzPracowniczyApplication.Dto;
using KalendarzPracowniczyDomain.Entities.UserDayOff;
using KalendarzPracowniczyDomain.Interfaces;
using MediatR;

namespace KalendarzPracowniczyApplication.CQRS.Queries.DayOff.GetDayOffById
{
    public class GetDayOffByIdQueryHandler : IRequestHandler<GetDayOffByIdQuery, DayOffDto>
    {
        private readonly IMapper _mapper;
        private readonly IDayOffRepository _dayOffRepository;

        public GetDayOffByIdQueryHandler(IMapper mapper, IDayOffRepository dayOffRepository)
        {
            _mapper = mapper;
            _dayOffRepository = dayOffRepository;
        }

        public async Task<DayOffDto> Handle(GetDayOffByIdQuery request, CancellationToken cancellationToken)
        {
            try
            {
                var getElementById = await _dayOffRepository.GetElementById(request.Id);
                if (getElementById == null)
                {
                    throw new Exception("Nie odnaleziono dnia wolnego");
                }
                var mapp = _mapper.Map<DayOffDto>(getElementById);
                return mapp;
            }
            catch (AutoMapperMappingException ex)
            {
                throw new AutoMapperMappingException($"Błąd mapowania", ex);
            }
            catch (Exception ex)
            {
                throw new Exception($"Nieoczekiwany błąd {ex.Message}");
            }
        }
    }
}