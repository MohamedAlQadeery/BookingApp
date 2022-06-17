using BookingApp.Domain.Models;

namespace BookingApp.Api.Services
{
    public class MyFirstServices
    {
        private readonly DataSource _dataSource;

        public MyFirstServices(DataSource dataSource)
        {
            _dataSource = dataSource;
        }

        public List<Hotel> GetHotels()
        {
            return _dataSource.Hotels;
        }
    }
}
