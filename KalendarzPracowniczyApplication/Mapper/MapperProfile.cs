using AutoMapper;
using KalendarzPracowniczyApplication.CQRS.Commands.Users.Login;
using KalendarzPracowniczyApplication.Dto;
using KalendarzPracowniczyDomain.Entities.Users;
using KalendarzPracowniczyDomain.Entities.Workers;

namespace KalendarzPracowniczyApplication.Mapper
{
    public class MapperProfile : Profile
    {
        public MapperProfile()
        {
            CreateMap<EventDto, KalendarzPracowniczyDomain.Entities.Events.Event>()
               .ForMember(dest => dest.UserId, opt => opt.MapFrom(src => src.UserDtoId.ToString()));
            CreateMap<UserDto, User>();
            CreateMap<User, UserDto>();

            CreateMap<WorkerDto, Worker>();
            CreateMap<LoginCommand, User>()
            .ForMember(dest => dest.UserName, opt => opt.MapFrom(src => src.UserName))
            .ForMember(dest => dest.Email, opt => opt.MapFrom(src => src.Email))
            .ForMember(dest => dest.PasswordHash, opt => opt.MapFrom(src => src.Password));
        }
    }
}