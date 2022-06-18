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
        public HotelsController(DataContext dataContext)
        {
            _ctx = dataContext;
        }

        [HttpGet]
        public async Task<IActionResult> GetHotels()
        {
            var hotels = await _ctx.Hotels.ToListAsync();
            return Ok(hotels);
        }


        [HttpPost]
        public async Task<IActionResult> CreateHotel([FromBody] Hotel hotel)
        {

            _ctx.Hotels.Add(hotel);
            await _ctx.SaveChangesAsync();

            return CreatedAtAction(nameof(GetHotelById), new { id = hotel.Id }, hotel);
        }

        [Route("{id}")]
        [HttpGet]
        public async Task<IActionResult> GetHotelById(int id)
        {

            var hotel = await _ctx.Hotels.FirstOrDefaultAsync(h => h.Id == id);
            if(hotel == null)
            {
                return NotFound();
            }
            return Ok(hotel);
           
        }

        [Route("{id}")]
        [HttpPut]
        public async Task<IActionResult> UpdateHotel([FromBody] Hotel updatedHotel,int id)
        {
            var hotel = await _ctx.Hotels.FirstOrDefaultAsync(h => h.Id == id);
            if(hotel == null)
            {
                return NotFound();
            }
            hotel.Name = updatedHotel.Name;
            hotel.Stars = updatedHotel.Stars;
            hotel.Description = updatedHotel.Description;

            _ctx.Hotels.Update(hotel);

            await _ctx.SaveChangesAsync();
            return NoContent();
        }

        [Route("{id}")]
        [HttpDelete]
        public async Task<IActionResult> DeleteHotel(int id)
        {

            var hotel = await _ctx.Hotels.FirstOrDefaultAsync(h => h.Id == id);
           if(hotel == null)
            {
                return NotFound();
            }
            _ctx.Hotels.Remove(hotel);
            await _ctx.SaveChangesAsync();
            return NoContent();


        }
    }
}
