using Application.DTOs;
using AutoMapper;
using Domain.Enums;
using Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Mappings
{
    public class AutoMapperProfiles : Profile
    {
        public AutoMapperProfiles() 
        {
            CreateMap<HotelDTO, Hotel>()
            .ForMember(target => target.Id, opt => opt.MapFrom(source => Guid.NewGuid()))
            .ForMember(target => target.Status, opt => opt.MapFrom(source => HotelEnum.Enabled));

            CreateMap<RoomDTO, Room>()
            .ForMember(target => target.Id, opt => opt.MapFrom(source => Guid.NewGuid()))
            .ForMember(target => target.Status, opt => opt.MapFrom(source => RoomStatus.Enabled));

            CreateMap<ContactBookingDTO, ContactEmergency>()
            .ForMember(target => target.Id, opt => opt.MapFrom(source => Guid.NewGuid()));

            CreateMap<GuestsBookingDTO, Guest>();

        }
    }
}
