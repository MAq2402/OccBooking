using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AutoMapper;
using OccBooking.Application.DTOs;
using OccBooking.Domain.Entities;

namespace OccBooking.Application.Mappings.Profiles
{
    public class PlaceProfile : Profile
    {
        public PlaceProfile()
        {
            CreateMap<Place, PlaceDto>()
                .ForMember(dest => dest.City, opt => opt.MapFrom(src => src.Address.City))
                .ForMember(dest => dest.Province, opt => opt.MapFrom(src => src.Address.Province))
                .ForMember(dest => dest.Street, opt => opt.MapFrom(src => src.Address.Street))
                .ForMember(dest => dest.ZipCode, opt => opt.MapFrom(src => src.Address.ZipCode))
                .ForMember(dest => dest.AdditionalOptions, opt => opt.MapFrom(src => src.AdditionalOptions.ToList()))
                .ForMember(dest => dest.OccasionTypes,
                    opt => opt.MapFrom(src => src.AvailableOccasionTypes.Select(x => x.Name)));
        }
    }
}