using AutoMapper;
using KalendarzPracowniczyDomain.Interfaces;
using MediatR;

namespace KalendarzPracowniczyApplication.CQRS.Commands.Events.Delete
{
    public class DeleteEventCommandHandler : IRequestHandler<DeleteEventCommand>
    {
        private readonly IEventRepository _eventRepository;
        private readonly IMapper _mapper;
        public DeleteEventCommandHandler(IEventRepository eventRepository, IMapper mapper)
        {
            _eventRepository = eventRepository;
            _mapper = mapper;
        }
        public async Task Handle(DeleteEventCommand request, CancellationToken cancellationToken)
        {
            await _eventRepository.Delete(request.Id);
        }
    }
}
