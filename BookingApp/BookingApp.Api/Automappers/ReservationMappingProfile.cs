using AutoMapper;
using BookingApp.Api.Dtos;
using BookingApp.Domain.Models;

namespace BookingApp.Api.Automappers
{
    public class ReservationMappingProfile : Profile
    {
        public ReservationMappingProfile()
        {
            CreateMap<ReservationPutPostDto, Reservation>();
        }
    }
}
