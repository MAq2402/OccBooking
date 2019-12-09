using System;
using System.Collections.Generic;
using System.Text;
using AutoMapper;
using OccBooking.Application.DTOs;
using OccBooking.Domain.Entities;

namespace OccBooking.Application.Mappings.Profiles
{
    public class HallReservationProfile : Profile
    {
        public HallReservationProfile()
        {
            CreateMap<HallReservation, HallReservationDto>()
                .ForMember(dest => dest.AmountOfPeople,
                    opt => opt.MapFrom(src => src.ReservationRequest.AmountOfPeople))
                .ForMember(dest => dest.ClientEmail,
                    opt => opt.MapFrom(src => src.ReservationRequest.Client.Email.Value))
                .ForMember(dest => dest.Cost,
                    opt => opt.MapFrom(src => src.ReservationRequest.Cost))
                .ForMember(dest => dest.Occasion,
                    opt => opt.MapFrom(src => src.ReservationRequest.OccasionType.Name));
        }
    }
}