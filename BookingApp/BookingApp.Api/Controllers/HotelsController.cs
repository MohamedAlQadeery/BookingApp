using BookingApp.Api.Services;
using BookingApp.Domain.Models;
using Microsoft.AspNetCore.Mvc;

namespace BookingApp.Api.Controllers
{
    // url : /hotels
    [ApiController]
    [Route("api/[controller]")]
    public class HotelsController : Controller
    {
        private readonly MyFirstServices _myFirstService;
        private readonly HttpContext? _httpContext;

        public HotelsController(MyFirstServices myFirstServices, IHttpContextAccessor httpContextAccessor)
        {
            _myFirstService = myFirstServices;
            _httpContext = httpContextAccessor.HttpContext;
        }

        [HttpGet]
        public IActionResult GetHotels()
        {
            _httpContext.Request.Headers.TryGetValue("datetime-middleware",out var header);
            //return Ok(_myFirstService.GetHotels());
            return Ok(header);
        }


        [HttpPost]
        public IActionResult CreateHotel([FromBody] Hotel hotel)
        {
            _myFirstService.GetHotels().Add(hotel);
            return CreatedAtAction(nameof(GetHotelById), new { id = hotel.Id }, hotel);
        }

        [Route("{id}")]
        [HttpGet]
        public IActionResult GetHotelById(int id)
        {
            var foundHotel = _myFirstService.GetHotels().FirstOrDefault(hotel => hotel.Id == id);
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
            var foundHotel = _myFirstService.GetHotels().FirstOrDefault(h => h.Id == id);
            if (foundHotel == null) return NotFound();
            _myFirstService.GetHotels().Remove(foundHotel);
            _myFirstService.GetHotels().Add(updatedHotel);

            return NoContent();
        }

        [Route("{id}")]
        [HttpDelete]
        public IActionResult DeleteHotel(int id)
        {
            var foundHotel = _myFirstService.GetHotels().FirstOrDefault(h => h.Id == id);
            if (foundHotel == null) return NotFound();
            _myFirstService.GetHotels().Remove(foundHotel);

            return NoContent();


        }
    }
}
