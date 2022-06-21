using BookingApp.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookingApp.Domain.Abstraction.Repositories
{
    public interface IHotelsRepository
    {
        Task<List<Hotel>> GetAllHotelsAsync();
        Task<Hotel> CreateHotelAsync(Hotel hotel);
        Task<Hotel> GetHotelByIdAsync(int id);
        Task<Hotel> UpdateHotelAsync(Hotel hotel);
        Task<Hotel> DeleteHotelByIdAsync(int id);


        Task<List<Room>> GetHotelRoomsAsync(int hotelId);
        Task<Room> AddHotelRoomAsync(int hotelId, Room room);
        Task<Room> UpdateHotelRoomAsync(int hotelId, Room room);
        Task<Room> DeleteHotelRoomAsync(int hotelId, int roomId);
        Task<Room> GetHotelRoomByIdAsync(int hotelId, int roomId);


    }
}
