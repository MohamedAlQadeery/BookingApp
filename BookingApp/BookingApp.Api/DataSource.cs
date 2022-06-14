using BookingApp.Domain.Models;

namespace BookingApp.Api
{
    public class DataSource
    {
        public DataSource()
        {
            Hotels = GetHotels();
        }

       
        public List<Hotel> Hotels { get; set; }

        private List<Hotel>? GetHotels()
        {
            return new List<Hotel> { };
        }


    }
}
