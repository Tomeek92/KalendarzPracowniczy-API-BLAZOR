using AutoMapper;
using KalendarzPracowniczyApplication.Dto;
using KalendarzPracowniczyDomain.Entities.Users;
using KalendarzPracowniczyDomain.Interfaces;
using MediatR;

namespace KalendarzPracowniczyApplication.CQRS.Commands.Users.Update
{
    public class UpdateUserCommandHandler : IRequestHandler<UpdateUserCommand>
    {
        private readonly IMapper _mapper;
        private readonly IUserRepository _userRepository;
        public UpdateUserCommandHandler(IUserRepository userRepository, IMapper mapper)
        {
            _mapper = mapper;
            _userRepository = userRepository;
        }
        public async Task Handle(UpdateUserCommand request, CancellationToken cancellationToken)
        {
            try
            {
                var mapp = _mapper.Map<User>(request);
                await _userRepository.UpdateUser(mapp);
            }
            catch (AutoMapperMappingException ex)
            {
                throw new AutoMapperMappingException($"Błąd mapowania", ex);
            }
            catch(Exception ex)
            {
                throw new Exception($"Błąd w Handlerze {ex.Message}");
            }
        }
    }
}
