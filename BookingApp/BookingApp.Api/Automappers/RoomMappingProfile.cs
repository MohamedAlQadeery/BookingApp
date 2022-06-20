using AutoMapper;
using BookingApp.Api.Dtos;
using BookingApp.Domain.Models;

namespace BookingApp.Api.Automappers
{
    public class RoomMappingProfile : Profile
    {

        public RoomMappingProfile()
        {
            CreateMap<Room, GetRoomDto>();
            CreateMap<PostPutRoomDto, Room>();
        }
    }
}
