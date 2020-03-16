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
            CreateMap<HallReservation, HallReservationDto>();

            CreateMap<ReservationRequest, HallReservationDto>()
                .ForMember(dest => dest.AmountOfPeople,
                    opt => opt.MapFrom(src => src.AmountOfPeople))
                .ForMember(dest => dest.ClientEmail,
                    opt => opt.MapFrom(src => src.Client.Email.Value))
                .ForMember(dest => dest.Cost,
                    opt => opt.MapFrom(src => src.Cost))
                .ForMember(dest => dest.Occasion,
                    opt => opt.MapFrom(src => src.OccasionType.Name))
                .ForAllOtherMembers(opt => opt.Ignore());
        }
    }
}