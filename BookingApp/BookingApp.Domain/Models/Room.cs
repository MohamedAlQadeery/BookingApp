using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookingApp.Domain.Models
{
    public class Room
    {
        public int Id { get; set; }
        public int RoomNumber { get; set; }

        public double Surface { get; set; }

        public bool NeedsRepair { get; set; }
    }

}
