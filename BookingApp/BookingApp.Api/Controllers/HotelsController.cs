using AutoMapper;
using BookingApp.Api.Dtos;
using BookingApp.Api.Services;
using BookingApp.Dal;
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

        private readonly DataContext _ctx;
        private readonly IMapper _mapper;
        public HotelsController(DataContext dataContext, IMapper mapper)
        {
            _ctx = dataContext;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<IActionResult> GetHotels()
        {
            var hotels = await _ctx.Hotels.ToListAsync();
            var getHotelsDto = _mapper.Map<List<GetHotelDto>>(hotels);
            return Ok(getHotelsDto);
        }


        [HttpPost]
        public async Task<IActionResult> CreateHotel([FromBody] CreateHotelDto hotel)
        {
            var domainHotel = _mapper.Map<Hotel>(hotel);
            _ctx.Hotels.Add(domainHotel);
            await _ctx.SaveChangesAsync();

            return CreatedAtAction(nameof(GetHotelById), new { id = domainHotel.HotelId }, domainHotel);
        }

        [Route("{id}")]
        [HttpGet]
        public async Task<IActionResult> GetHotelById(int id)
        {

            var hotel = await _ctx.Hotels.FirstOrDefaultAsync(h => h.HotelId == id);
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

            _ctx.Hotels.Update(toUpdate);
            await _ctx.SaveChangesAsync();

            return NoContent();
        }

        [Route("{id}")]
        [HttpDelete]
        public async Task<IActionResult> DeleteHotel(int id)
        {

            var hotel = await _ctx.Hotels.FirstOrDefaultAsync(h => h.HotelId == id);
            if (hotel == null)
            {
                return NotFound();
            }
            _ctx.Hotels.Remove(hotel);
            await _ctx.SaveChangesAsync();
            return NoContent();


        }
        
        [Route("{hotelId}/rooms")]
        [HttpGet]
        public async Task<IActionResult> GetHotelRooms(int hotelId)
        {
            var domainRooms = await _ctx.Rooms.Where(r => r.HotelId == hotelId).ToListAsync();

            var getRooms = _mapper.Map<List<GetRoomDto>>(domainRooms);

            return Ok(getRooms);
        }

        [Route("{hotelId}/rooms")]
        [HttpPost]
        public async Task<IActionResult> AddHotelRoom(int hotelId,[FromBody] PostPutRoomDto room)
        {
            var domainRoom = _mapper.Map<Room>(room);
            domainRoom.HotelId = hotelId;
            _ctx.Rooms.Add(domainRoom);
            await _ctx.SaveChangesAsync();

            var getRoomDto = _mapper.Map<GetRoomDto>(domainRoom);
            return CreatedAtAction(nameof(GetHotelRoomById), new { hotelId = hotelId, roomId = getRoomDto.RoomId }, getRoomDto);
        }

        [Route("{hotelId}/rooms/{roomId}")]
        [HttpGet]
        public async Task<IActionResult> GetHotelRoomById(int hotelId,int roomId)
        {
            var room = await _ctx.Rooms.FirstOrDefaultAsync(r => r.HotelId == hotelId && r.RoomId == roomId);
            
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
            room.HotelId = hotelId;
            room.RoomId = roomId;

            _ctx.Rooms.Update(room);
            await _ctx.SaveChangesAsync();
            return NoContent();
        }


        [Route("{hotelId}/rooms/{roomId}")]
        [HttpDelete]
        public async Task<IActionResult> DeleteHotelRoomById(int hotelId,int roomId)
        {
            var room = await _ctx.Rooms.FirstOrDefaultAsync(r => r.HotelId == hotelId && r.RoomId == roomId);
            if(room == null)
            {
                return NotFound("Room not found");
            }
            _ctx.Rooms.Remove(room);
            await _ctx.SaveChangesAsync();
            return NoContent();
        }
    }
}
