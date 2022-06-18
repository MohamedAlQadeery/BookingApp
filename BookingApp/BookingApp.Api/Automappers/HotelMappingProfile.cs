using AutoMapper;
using BookingApp.Api.Dtos;
using BookingApp.Domain.Models;

namespace BookingApp.Api.Automappers
{
    public class HotelMappingProfile : Profile
    {
        public HotelMappingProfile()
        {
            CreateMap<CreateHotelDto, Hotel>();
            CreateMap<Hotel, GetHotelDto>();
        }
    }
}
