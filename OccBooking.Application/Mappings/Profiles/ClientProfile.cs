using System;
using System.Collections.Generic;
using System.Text;
using AutoMapper;
using OccBooking.Application.DTOs;
using OccBooking.Domain.ValueObjects;

namespace OccBooking.Application.Mappings.Profiles
{
    public class ClientProfile : Profile
    {
        public ClientProfile()
        {
            CreateMap<Client, ClientDto>()
                .ForMember(dest => dest.Email, opt => opt.MapFrom(src => src.Email.Value))
                .ForMember(dest => dest.FirstName, opt => opt.MapFrom(src => src.Name.FirstName))
                .ForMember(dest => dest.LastName, opt => opt.MapFrom(src => src.Name.LastName))
                .ForMember(dest => dest.PhoneNumber, opt => opt.MapFrom(src => src.PhoneNumber.Value));
        }
    }
}
