using AutoMapper;
using KalendarzPracowniczyDomain.Interfaces;
using MediatR;
using Microsoft.AspNetCore.Http;
using System.Security.Claims;

namespace KalendarzPracowniczyApplication.CQRS.Commands.Events.Delete
{
    public class DeleteEventCommandHandler : IRequestHandler<DeleteEventCommand>
    {
        private readonly IEventRepository _eventRepository;
        private readonly IMapper _mapper;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public DeleteEventCommandHandler(IEventRepository eventRepository, IMapper mapper, IHttpContextAccessor httpContextAccessor)
        {
            _eventRepository = eventRepository;
            _mapper = mapper;
            _httpContextAccessor = httpContextAccessor;
        }

        public async Task Handle(DeleteEventCommand request, CancellationToken cancellationToken)
        {
            try
            {
                var findEventId = await _eventRepository.GetElementById(request.Id);
                if (findEventId == null)
                {
                    throw new KeyNotFoundException($"Rekord o danym {request.Id} nie został znaleziony w bazie danych ");
                }
                var userId = _httpContextAccessor.HttpContext?.User?.FindFirstValue(ClaimTypes.NameIdentifier);
                var userName = _httpContextAccessor.HttpContext?.User?.FindFirstValue(ClaimTypes.Name);

                if (string.IsNullOrEmpty(userId))
                {
                    throw new UnauthorizedAccessException("Nie znaleziono zalogowanego użytkownika.");
                }
                findEventId.IsDeleted = true;
                findEventId.DeletedById = userId;
                findEventId.DeletedAt = DateTime.UtcNow;
                findEventId.DeletedBy = userName;

                await _eventRepository.SaveAsync();
            }
            catch (Exception ex)
            {
                throw new Exception($"Błąd podczas usuwania {ex.Message}");
            }
        }
    }
}