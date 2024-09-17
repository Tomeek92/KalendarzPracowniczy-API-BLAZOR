using AutoMapper;
using KalendarzPracowniczyApplication.Dto;
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

            CreateMap<UserDto, User>();

            CreateMap<WorkDto, Work>()
            .ForMember(dest => dest.UserId, opt => opt.MapFrom(src => src.User.Id))
            .ForMember(dest => dest.User, opt => opt.MapFrom(src => src.User));

            CreateMap<Work, WorkDto>()
            .ForMember(dest => dest.UserId, opt => opt.MapFrom(src => src.UserId))
            .ForMember(dest => dest.User, opt => opt.MapFrom(src => src.User));

            CreateMap<WorkerDto, Worker>();
        }
    }
}