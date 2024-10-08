using AutoMapper;
using KalendarzPracowniczyDomain.Interfaces;
using MediatR;

namespace KalendarzPracowniczyApplication.CQRS.Commands.Workers.Delete
{
    public class DeleteCarCommandHandler : IRequestHandler<DeleteCarCommand>
    {
        private readonly IMapper _mapper;
        private readonly ICarRepository _carRepository;
        public DeleteCarCommandHandler(IMapper mapper, ICarRepository carRepository)
        {
            _mapper = mapper;
            _carRepository = carRepository;
        }

        public async Task Handle(DeleteCarCommand request, CancellationToken cancellationToken)
        {
            try
            {
                await _carRepository.Delete(request.Id);
            }
            catch (Exception ex)
            {
                throw new Exception($"Błąd podczas usuwania zadania {ex.Message}");
            }
        }
    }
}
