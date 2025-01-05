using AutoMapper;
using KalendarzPracowniczyApplication.Dto;
using KalendarzPracowniczyDomain.Interfaces;
using MediatR;

namespace KalendarzPracowniczyApplication.CQRS.Queries.DayOff.GetAllDaysOff
{
    public class GetAllDaysOffQueryHandler : IRequestHandler<GetAllDaysOffQuery, List<DayOffDto>>
    {
        private readonly IMapper _mapper;
        private readonly IDayOffRepository _dayOffRepository;
        public GetAllDaysOffQueryHandler(IMapper mapper, IDayOffRepository dayOffRepository)
        {
            _mapper = mapper;
            _dayOffRepository = dayOffRepository;
        }
        public async Task<List<DayOffDto>> Handle(GetAllDaysOffQuery request, CancellationToken cancellationToken)
        {
            try
            {
                var daysOff = await _dayOffRepository.GetAllDaysOff();
                var mapp = _mapper.Map<List<DayOffDto>>(daysOff);
                return mapp;
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
