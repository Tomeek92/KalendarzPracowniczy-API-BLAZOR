using AutoMapper;
using KalendarzPracowniczyDomain.Entities.Works;
using KalendarzPracowniczyDomain.Interfaces;
using MediatR;

namespace KalendarzPracowniczyApplication.CQRS.Commands.Works.Delete
{
    public class DeleteWorkCommandHandler : IRequestHandler<DeleteWorkCommand>
    {
        private readonly IMapper _mapper;
        private readonly IWorkRepository _workRepository;

        public DeleteWorkCommandHandler(IMapper mapper, IWorkRepository workRepository)
        {
            _mapper = mapper;
            _workRepository = workRepository;
        }

        public async Task Handle(DeleteWorkCommand request, CancellationToken cancellationToken)
        {
            try
            {
                await _workRepository.DeleteTaskAsync(request.Id);
            }
            catch (Exception ex)
            {
                throw new Exception($"Błąd podczas usuwania zadania {ex.Message}");
            }
        }
    }
}