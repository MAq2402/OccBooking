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
        }
    }
}
