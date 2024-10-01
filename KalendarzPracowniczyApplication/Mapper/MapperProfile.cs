using AutoMapper;
using KalendarzPracowniczyApplication.CQRS.Commands.Events.Create;
using KalendarzPracowniczyApplication.CQRS.Queries.Users.Login;
using KalendarzPracowniczyApplication.Dto;
using KalendarzPracowniczyDomain.Entities.Events;
using KalendarzPracowniczyDomain.Entities.Users;
using KalendarzPracowniczyDomain.Entities.Workers;
using KalendarzPracowniczyDomain.Entities.Works;

namespace KalendarzPracowniczyApplication.Mapper
{
    public class MapperProfile : Profile
    {
        public MapperProfile()
        {
            CreateMap<EventDto, KalendarzPracowniczyDomain.Entities.Events.Event>()
               .ForMember(dest => dest.UserId, opt => opt.MapFrom(src => src.UserDtoId.ToString()));

            CreateMap<CreateEventCommand, Event>()
           .ForMember(dest => dest.Id, opt => opt.Ignore())
           .ForMember(dest => dest.UserId, opt => opt.MapFrom(src => src.UserDtoId));

            CreateMap<UserDto, User>()
            .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id));

            CreateMap<User, UserDto>();


            CreateMap<WorkDto, Work>()
            .ForMember(dest => dest.UserId, opt => opt.MapFrom(src => src.User.Id))
            .ForMember(dest => dest.User, opt => opt.MapFrom(src => src.User));

            CreateMap<LoginQuery, User>()
            .ForMember(dest => dest.UserName, opt => opt.MapFrom(src => src.UserName))
            .ForMember(dest => dest.PasswordHash, opt => opt.MapFrom(src => src.Password))
            .ForMember(dest => dest.Email, opt => opt.MapFrom(src => src.Email));

            CreateMap<Work, WorkDto>()
            .ForMember(dest => dest.UserId, opt => opt.MapFrom(src => src.UserId))
            .ForMember(dest => dest.User, opt => opt.MapFrom(src => src.User));

            CreateMap<WorkerDto, Worker>();
        }
    }
}