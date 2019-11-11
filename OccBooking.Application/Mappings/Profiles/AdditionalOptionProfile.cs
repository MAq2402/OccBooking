using System;
using System.Collections.Generic;
using System.Text;
using AutoMapper;
using OccBooking.Application.DTOs;
using OccBooking.Domain.ValueObjects;

namespace OccBooking.Application.Mappings.Profiles
{
    public class AdditionalOptionProfile : Profile
    {
        public AdditionalOptionProfile()
        {
            CreateMap<PlaceAdditionalOption, AdditionalOptionDto>();
        }
    }
}
