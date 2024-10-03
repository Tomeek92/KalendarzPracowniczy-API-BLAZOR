using AutoMapper;
using KalendarzPracowniczyApplication.Dto;
using KalendarzPracowniczyDomain.Interfaces;
using MediatR;

namespace KalendarzPracowniczyApplication.CQRS.Queries.Events.GetElementById
{
    public class GetElementByIdEventQueryHandler : IRequestHandler<GetElementByIdEventQuery, EventDto>
    {
        private readonly IMapper _mapper;
        private readonly IEventRepository _eventRepository;

        public GetElementByIdEventQueryHandler(IEventRepository eventRepository, IMapper mapper)
        {
            _mapper = mapper;
            _eventRepository = eventRepository;
        }

        public async Task<EventDto> Handle(GetElementByIdEventQuery request, CancellationToken cancellationToken)
        {
            try
            {
                var existingEvent = await _eventRepository.GetElementById(request.Id);
                if (existingEvent == null)
                {
                    throw new KeyNotFoundException($"Nie odnaleziono eventu");
                }
                var mapp = _mapper.Map<EventDto>(existingEvent);
                return mapp;
            }
            catch (AutoMapperMappingException ex)
            {
                throw new AutoMapperMappingException($"Błąd mapowania", ex);
            }
        }
    }
}