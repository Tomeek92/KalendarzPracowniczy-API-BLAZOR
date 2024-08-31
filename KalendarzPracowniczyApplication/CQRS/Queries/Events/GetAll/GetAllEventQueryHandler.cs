using AutoMapper;
using KalendarzPracowniczyApplication.Dto;
using KalendarzPracowniczyDomain.Interfaces;
using MediatR;

namespace KalendarzPracowniczyApplication.CQRS.Queries.Events.GetAll
{
    public class GetAllEventQueryHandler : IRequestHandler<GetAllEventQuery, IEnumerable<EventDto>>
    {
        private readonly IEventRepository _eventRepository;
        private readonly IMapper _mapper;
        public GetAllEventQueryHandler(IEventRepository eventRepository, IMapper mapper)
        {
            _eventRepository = eventRepository;
            _mapper = mapper;
        }
        public async Task<IEnumerable<EventDto>> Handle(GetAllEventQuery request, CancellationToken cancellationToken)
        {
            try
            {
                var getAllEvents = await _eventRepository.GettAllEvents();
                var mapp = _mapper.Map<IEnumerable<EventDto>>(getAllEvents);
                return mapp;
            }
            catch (AutoMapperMappingException ex)
            {
                throw new AutoMapperMappingException($"Błąd mapowania", ex);
            }
        }
    }
}
