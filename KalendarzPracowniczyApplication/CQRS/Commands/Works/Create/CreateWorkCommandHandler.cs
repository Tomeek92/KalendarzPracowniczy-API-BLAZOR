using AutoMapper;
using KalendarzPracowniczyApplication.CQRS.Queries.Users.LoggedUser;
using KalendarzPracowniczyDomain.Entities.Works;
using KalendarzPracowniczyDomain.Interfaces;
using MediatR;

namespace KalendarzPracowniczyApplication.CQRS.Commands.Works.Create
{
    public class CreateWorkCommandHandler : IRequestHandler<CreateWorkCommand>
    {
        private readonly IMapper _mapper;
        private readonly IWorkRepository _workRepository;
        private readonly IMediator _mediator;

        public CreateWorkCommandHandler(IMapper mapper, IWorkRepository workRepository, IMediator mediator)
        {
            _mapper = mapper;
            _mediator = mediator;
            _workRepository = workRepository;
        }

        public async Task Handle(CreateWorkCommand request, CancellationToken cancellationToken)
        {
            try
            {
                var loggedUser = await _mediator.Send(new LoggedUserQuery(), cancellationToken);
                if (loggedUser == null)
                {
                    throw new Exception("Nie można utworzyć wydarzenia bez zalogowanego użytkownika.");
                }
                var mapp = _mapper.Map<Work>(request);

                mapp.UserId = loggedUser.Id;
                

                await _workRepository.AddTaskAsync(mapp);
            }
            catch (AutoMapperMappingException ex)
            {
                throw new AutoMapperMappingException($"Błąd podczas mapowania{ex.Message}");
            }
        }
    }
}