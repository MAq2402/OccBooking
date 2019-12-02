using System;
using System.Collections.Generic;
using System.Text;
using AutoMapper;
using OccBooking.Application.DTOs;
using OccBooking.Domain.Entities;
using OccBooking.Domain.ValueObjects;

namespace OccBooking.Application.Mappings.Profiles
{
    public class ReservationRequestProfile : Profile
    {
        public ReservationRequestProfile()
        {
            CreateMap<ReservationRequest, ReservationRequestDto>()
                .ForMember(dest => dest.ClientEmail, opt => opt.MapFrom(src => src.Client.Email.Value))
                .ForMember(dest => dest.Date, opt => opt.MapFrom(src => src.DateTime))
                .ForMember(dest => dest.Occasion, opt => opt.MapFrom(src => src.OccasionType.Name));
        }
    }
}