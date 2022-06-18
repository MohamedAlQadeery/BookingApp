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
        public HotelsController(DataContext dataContext,IMapper mapper)
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

            return CreatedAtAction(nameof(GetHotelById), new { id = domainHotel.Id }, domainHotel);
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

            var getHotelDto = _mapper.Map<GetHotelDto>(hotel);
            return Ok(getHotelDto);
           
        }

        [Route("{id}")]
        [HttpPut]
        public async Task<IActionResult> UpdateHotel([FromBody] CreateHotelDto updatedHotel,int id)
        {
            var toUpdate = _mapper.Map<Hotel>(updatedHotel);
            toUpdate.Id = id;

            _ctx.Hotels.Update(toUpdate);
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
