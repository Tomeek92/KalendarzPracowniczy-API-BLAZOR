using AutoMapper;
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

            CreateMap<WorkerDto, Worker>();
        }
    }
}