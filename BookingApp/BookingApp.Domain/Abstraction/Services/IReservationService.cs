using BookingApp.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookingApp.Domain.Abstraction.Services
{
    public interface IReservationService
    {
        Task<Reservation> MakeReservationAsync(Reservation reservation);
    }
}
