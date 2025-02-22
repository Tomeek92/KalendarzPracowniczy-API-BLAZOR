﻿using AutoMapper;
using KalendarzPracowniczyApplication.CQRS.Commands.Events.Create;
using KalendarzPracowniczyApplication.CQRS.Commands.Events.Update;
using KalendarzPracowniczyApplication.CQRS.Commands.Users.Create;
using KalendarzPracowniczyApplication.CQRS.Queries.Users.Login;
using KalendarzPracowniczyApplication.Dto;
using KalendarzPracowniczyDomain.Entities.Cars;
using KalendarzPracowniczyDomain.Entities.Events;
using KalendarzPracowniczyDomain.Entities.UserDayOff;
using KalendarzPracowniczyDomain.Entities.Users;
using KalendarzPracowniczyDomain.Entities.Works;

namespace KalendarzPracowniczyApplication.Mapper
{
    public class MapperProfile : Profile
    {
        public MapperProfile()
        {
            CreateMap<EventDto, Event>()
            .ForMember(dest => dest.UserId, opt => opt.MapFrom(src => src.User.Id))
             .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id))
            .ForMember(dest => dest.StartDate, opt => opt.MapFrom(src => src.StartDate))
            .ForMember(dest => dest.Description, opt => opt.MapFrom(src => src.Description))
            .ForMember(dest => dest.Title, opt => opt.MapFrom(src => src.Title))
             .ForMember(dest => dest.Car, opt => opt.MapFrom(src => src.Car));

            CreateMap<Event, EventDto>()
            .ForMember(dest => dest.StartDate, opt => opt.MapFrom(src => src.StartDate))
            .ForMember(dest => dest.Description, opt => opt.MapFrom(src => src.Description))
            .ForMember(dest => dest.Title, opt => opt.MapFrom(src => src.Title))
            .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id))
            .ForMember(dest => dest.UserId, opt => opt.MapFrom(src => src.UserId))
            .ForMember(dest => dest.Car, opt => opt.MapFrom(src => src.Car));

            CreateMap<UserDto, User>()
            .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id));

            CreateMap<User, UserDto>();

            CreateMap<WorkDto, Work>()
            .ForMember(dest => dest.UserId, opt => opt.MapFrom(src => src.User.Id))
            .ForMember(dest => dest.User, opt => opt.MapFrom(src => src.User))
            .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id));

            CreateMap<LoginQuery, User>()
            .ForMember(dest => dest.UserName, opt => opt.MapFrom(src => src.UserName))
            .ForMember(dest => dest.PasswordHash, opt => opt.MapFrom(src => src.Password));

            CreateMap<Work, WorkDto>()
            .ForMember(dest => dest.UserId, opt => opt.MapFrom(src => src.UserId))
            .ForMember(dest => dest.User, opt => opt.MapFrom(src => src.User))
            .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id));

            CreateMap<CarDto, Car>()
            .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.Name))
            .ForMember(dest => dest.Model, opt => opt.MapFrom(src => src.Model))
            .ForMember(dest => dest.CarPlatesNumber, opt => opt.MapFrom(src => src.CarPlatesNumber))
            .ForMember(dest => dest.Production, opt => opt.MapFrom(src => src.Production))
            .ForMember(dest => dest.CarKm, opt => opt.MapFrom(src => src.CarKm))
            .ForMember(dest => dest.CarInspection, opt => opt.MapFrom(src => src.CarInspection));
            CreateMap<Car, CarDto>()
            .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.Name))
            .ForMember(dest => dest.Model, opt => opt.MapFrom(src => src.Model))
            .ForMember(dest => dest.CarPlatesNumber, opt => opt.MapFrom(src => src.CarPlatesNumber))
            .ForMember(dest => dest.Production, opt => opt.MapFrom(src => src.Production))
            .ForMember(dest => dest.CarKm, opt => opt.MapFrom(src => src.CarKm))
            .ForMember(dest => dest.CarInspection, opt => opt.MapFrom(src => src.CarInspection));

            CreateMap<UpdateEventCommand, Event>();

            CreateMap<DayOff, DayOffDto>();

            CreateMap<DayOffDto, DayOff>();

            CreateMap<LoginDto, User>();

            CreateMap<User, LoginDto>();

            CreateMap<CreateEventCommand, Event>()
            .ForMember(dest => dest.Title, opt => opt.MapFrom(src => src.Title))
            .ForMember(dest => dest.StartDate, opt => opt.MapFrom(src => src.StartDate))
            .ForMember(dest => dest.Description, opt => opt.MapFrom(src => src.Description));
            CreateMap<CreateUserCommand, User>()
            .ForMember(dest => dest.Id, opt => opt.Ignore());
        }
    }
}