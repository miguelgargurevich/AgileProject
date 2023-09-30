﻿using System;
using AutoMapper;
using AgileProject.Model;
using AgileProject.Entidades;

namespace AgileProject.Mapping
{
	public class MappingProfile : Profile
	{
		public MappingProfile()
		{
            //CreateMap<CalendarModel, Calendar>()
            //    .ForMember(dest => dest.id, opt => opt.MapFrom(src => src.Id))
            //    .ForMember(dest => dest.title, opt => opt.MapFrom(src => src.Title))
            //    .ForMember(dest => dest.start, opt => opt.MapFrom(src => src.StartDate))
            //    .ForMember(dest => dest.end, opt => opt.MapFrom(src => src.EndDate))
            //    .ForMember(dest => dest.allDay, opt => opt.MapFrom(src => src.AllDay))
            //    .ForMember(dest => dest.color, opt => opt.MapFrom(src => src.Color))
            //    .ForMember(dest => dest.EventTypeId, opt => opt.MapFrom(src => src.EventTypeId))
            //    .ForMember(dest => dest.type, opt => opt.MapFrom(src => src.EventTypeName))
            //    .ForMember(dest => dest.CalendarTypeId, opt => opt.MapFrom(src => src.CalendarTypeId))
            //    .ForMember(dest => dest.CalendarTypeName, opt => opt.MapFrom(src => src.CalendarTypeName))
            //    .ForMember(dest => dest.description, opt => opt.MapFrom(src => src.Description))
            //    .ForMember(dest => dest.UserCreate, opt => opt.MapFrom(src => src.UserCreate))
            //    .ForMember(dest => dest.DateCreate, opt => opt.MapFrom(src => src.DateCreate));

            CreateMap<Usuario, AspNetUsersModel>();
            CreateMap<AspNetUsersModel, Usuario>();

            CreateMap<CalendarModel, Calendar>();
            CreateMap<Calendar, CalendarModel>();

            CreateMap<EventType, EventTypeModel>();
            CreateMap<EventTypeModel, EventType>();

            CreateMap<EventTypeModel, EventType>();
            CreateMap<EventType, EventTypeModel>();

            //CreateMap<EventType, EventTypeModel>()
            //    .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id))
            //    .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.Name))
            //    .ForMember(dest => dest.Color, opt => opt.MapFrom(src => src.Color));

            //CreateMap<EventTypeModel, EventType>()
            //    .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id))
            //    .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.Name))
            //    .ForMember(dest => dest.Color, opt => opt.MapFrom(src => src.Color));

            //CreateMap<AspNetUsersModel, AspNetUsers>()
            //    .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id))
            //    .ForMember(dest => dest.UserName, opt => opt.MapFrom(src => src.UserName))
            //    .ForMember(dest => dest.Nombres, opt => opt.MapFrom(src => src.Nombres))
            //    .ForMember(dest => dest.ApellidoPaterno, opt => opt.MapFrom(src => src.ApellidoPaterno))
            //    .ForMember(dest => dest.ApellidoMaterno, opt => opt.MapFrom(src => src.ApellidoMaterno))
            //    .ForMember(dest => dest.FechaNacimiento, opt => opt.MapFrom(src => src.FechaNacimiento))
            //    .ForMember(dest => dest.PhoneNumber, opt => opt.MapFrom(src => src.PhoneNumber))
            //    ;


        }
	}
}

