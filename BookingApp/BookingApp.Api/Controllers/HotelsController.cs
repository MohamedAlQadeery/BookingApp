using AutoMapper;
using BookingApp.Api.Dtos;
using BookingApp.Api.Services;
using BookingApp.Dal;
using BookingApp.Domain.Abstraction.Repositories;
using BookingApp.Domain.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace BookingApp.Api.Controllers
{
    // url : /hotels
    [ApiController]
    [Route("api/[controller]")]
    public class HotelsController : Controller
    {
        private readonly IHotelsRepository _hotelRepo;
      
        private readonly IMapper _mapper;
        public HotelsController(IMapper mapper, IHotelsRepository hotelRepo)
        {
            _mapper = mapper;
            _hotelRepo = hotelRepo;

        }

        [HttpGet]
        public async Task<IActionResult> GetHotels()
        {
            var hotels = await _hotelRepo.GetAllHotelsAsync();
            var getHotelsDto = _mapper.Map<List<GetHotelDto>>(hotels);
            return Ok(getHotelsDto);
        }


        [HttpPost]
        public async Task<IActionResult> CreateHotel([FromBody] CreateHotelDto hotel)
        {
            var domainHotel = _mapper.Map<Hotel>(hotel);
            await _hotelRepo.CreateHotelAsync(domainHotel);
            var getHotel = _mapper.Map<GetHotelDto>(domainHotel);
            return CreatedAtAction(nameof(GetHotelById), new { id = domainHotel.HotelId }, getHotel);
        }

        [Route("{id}")]
        [HttpGet]
        public async Task<IActionResult> GetHotelById(int id)
        {

            var hotel = await _hotelRepo.GetHotelByIdAsync(id);
            if (hotel == null)
            {
                return NotFound();
            }

            var getHotelDto = _mapper.Map<GetHotelDto>(hotel);
            return Ok(getHotelDto);

        }

        [Route("{id}")]
        [HttpPut]
        public async Task<IActionResult> UpdateHotel([FromBody] CreateHotelDto updatedHotel, int id)
        {
            var toUpdate = _mapper.Map<Hotel>(updatedHotel);
            toUpdate.HotelId = id;
            await _hotelRepo.UpdateHotelAsync(toUpdate);
           

            return NoContent();
        }

        [Route("{id}")]
        [HttpDelete]
        public async Task<IActionResult> DeleteHotel(int id)
        {

            var hotel = await _hotelRepo.DeleteHotelByIdAsync(id);
            if (hotel == null)
            {
                return NotFound();
            }
           
            return NoContent();


        }
        
        [Route("{hotelId}/rooms")]
        [HttpGet]
        public async Task<IActionResult> GetHotelRooms(int hotelId)
        {
            var domainRooms = await _hotelRepo.GetHotelRoomsAsync(hotelId);

            var getRooms = _mapper.Map<List<GetRoomDto>>(domainRooms);

            return Ok(getRooms);
        }

        [Route("{hotelId}/rooms")]
        [HttpPost]
        public async Task<IActionResult> AddHotelRoom(int hotelId,[FromBody] PostPutRoomDto room)
        {
            var domainRoom = _mapper.Map<Room>(room);
           await _hotelRepo.AddHotelRoomAsync(hotelId,domainRoom);
                
            var getRoomDto = _mapper.Map<GetRoomDto>(domainRoom);
            return CreatedAtAction(nameof(GetHotelRoomById), new { hotelId = hotelId, roomId = getRoomDto.RoomId }, getRoomDto);
        }

        [Route("{hotelId}/rooms/{roomId}")]
        [HttpGet]
        public async Task<IActionResult> GetHotelRoomById(int hotelId,int roomId)
        {
            var room = await _hotelRepo.GetHotelRoomByIdAsync(hotelId, roomId);
            
            if(room == null)
            {
                return NotFound("Room is not found");
            }
            var getRoom = _mapper.Map<GetRoomDto>(room);

            return Ok(getRoom);
        }

        [Route("{hotelId}/rooms/{roomId}")]
        [HttpPut]
        public async Task<IActionResult> UpdateHotelRoomById(int hotelId , int roomId,[FromBody] PostPutRoomDto updatedRoom)
        {
            var room = _mapper.Map<Room>(updatedRoom);
            room.RoomId = roomId;
            await _hotelRepo.UpdateHotelRoomAsync(hotelId, room);
            return NoContent();
        }


        [Route("{hotelId}/rooms/{roomId}")]
        [HttpDelete]
        public async Task<IActionResult> DeleteHotelRoomById(int hotelId,int roomId)
        {
            var room = await _hotelRepo.DeleteHotelRoomAsync(hotelId, roomId);
            if(room == null)
            {
                return NotFound("Room not found");
            }
          
            return NoContent();
        }
    }
}


