using AutoMapper;
using KalendarzPracowniczyDomain.Entities.Users;
using KalendarzPracowniczyDomain.Interfaces;
using MediatR;
using Microsoft.AspNet.Identity;

namespace KalendarzPracowniczyApplication.CQRS.Commands.Users.Create
{
    public class CreateUserCommandHandler : IRequestHandler<CreateUserCommand>
    {
        private readonly IMapper _mapper;
        private readonly IUserRepository _userRepository;

        public CreateUserCommandHandler(IUserRepository userRepository, IMapper mapper)
        {
            _mapper = mapper;
            _userRepository = userRepository;
        }

        public async Task Handle(CreateUserCommand request, CancellationToken cancellationToken)
        {
            try
            {
                var mapp = _mapper.Map<User>(request);
                
                if (string.IsNullOrEmpty(mapp.Id))
                {
                    mapp.Id = Guid.NewGuid().ToString();  
                }
                
                await _userRepository.CreateUser(mapp, request.Password);
            }
            catch (AutoMapperMappingException ex)
            {
                throw new AutoMapperMappingException($"Błąd mapowania", ex);
            }
        }
    }
}