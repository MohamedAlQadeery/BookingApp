using BookingApp.Domain.Models;
using Microsoft.AspNetCore.Mvc;

namespace BookingApp.Api.Controllers
{
    // url : /hotels
    [ApiController]
    [Route("api/[controller]")]
    public class HotelsController : Controller
    {
        private readonly DataSource _data;

        public HotelsController(DataSource dataSource)
        {
            _data = dataSource;
        }

        [HttpGet]
        public IActionResult GetHotels()
        {
            return Ok(_data.Hotels);
        }


        [HttpPost]
        public IActionResult CreateHotel([FromBody] Hotel hotel)
        {
            _data.Hotels.Add(hotel);
            return CreatedAtAction(nameof(GetHotelById), new { id = hotel.Id }, hotel);
        }

        [Route("{id}")]
        [HttpGet]
        public IActionResult GetHotelById(int id)
        {
            var foundHotel = _data.Hotels.FirstOrDefault(hotel => hotel.Id == id);
            if (foundHotel == null)
            {
                return NotFound();
            }

            return Ok(foundHotel);
        }

        [Route("{id}")]
        [HttpPut]
        public IActionResult UpdateHotel([FromBody] Hotel updatedHotel,int id)
        {
            var foundHotel = _data.Hotels.FirstOrDefault(h => h.Id == id);
            if (foundHotel == null) return NotFound();
            _data.Hotels.Remove(foundHotel);
            _data.Hotels.Add(updatedHotel);

            return NoContent();
        }

        [Route("{id}")]
        [HttpDelete]
        public IActionResult DeleteHotel(int id)
        {
            var foundHotel = _data.Hotels.FirstOrDefault(h => h.Id == id);
            if (foundHotel == null) return NotFound();
            _data.Hotels.Remove(foundHotel);

            return NoContent();


        }
    }
}
