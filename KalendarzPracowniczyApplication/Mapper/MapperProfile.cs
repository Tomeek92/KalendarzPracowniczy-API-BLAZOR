using AutoMapper;
using KalendarzPracowniczyApplication.Dto;
using KalendarzPracowniczyDomain.Entities.Workers;

namespace KalendarzPracowniczyApplication.Mapper
{
    public class MapperProfile : Profile
    {
        public MapperProfile()
        {
            CreateMap<EventDto, KalendarzPracowniczyDomain.Entities.Events.Event>();
            CreateMap<WorkerDto, Worker>();
        }
    }
}