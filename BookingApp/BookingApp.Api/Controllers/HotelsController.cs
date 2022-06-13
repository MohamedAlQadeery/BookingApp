using Microsoft.AspNetCore.Mvc;

namespace BookingApp.Api.Controllers
{
    // url : /hotels
    [ApiController]
    [Route("[controller]")]
    public class HotelsController : Controller
    {
        [HttpGet]
        public IActionResult GetRooms()
        {
            return Ok("Get Rooms Successfully");
        }

    }
}
