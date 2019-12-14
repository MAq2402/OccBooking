using System;
using System.Collections.Generic;
using System.Text;
using AutoMapper;
using OccBooking.Application.DTOs;
using OccBooking.Domain.Entities;
using OccBooking.Persistence.Entities;

namespace OccBooking.Application.Mappings.Profiles
{
    public class UserProfile : Profile
    {
        public UserProfile()
        {
            CreateMap<User, UserDto>()
                .ForMember(dest => dest.Email, opt => opt.MapFrom(src => src.Owner.Email.Value))
                .ForMember(dest => dest.FirstName, opt => opt.MapFrom(src => src.Owner.Name.FirstName))
                .ForMember(dest => dest.LastName, opt => opt.MapFrom(src => src.Owner.Name.LastName))
                .ForMember(dest => dest.PhoneNumber, opt => opt.MapFrom(src => src.Owner.PhoneNumber.Value))
                .ForMember(dest => dest.OwnerId, opt => opt.MapFrom(src => src.Owner.Id));
        }
    }
}
