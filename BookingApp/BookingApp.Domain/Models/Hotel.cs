﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookingApp.Domain.Models
{
    public class Hotel
    {
        public int Id { get; set; }
        public string? Name { get; set; }

        public int Stars { get; set; }

        public string? Address { get; set; }

        public string? City { get; set; }
        public string? Country { get; set; }
        public List<Room>? Rooms { get; set; }

        public string? Description { get; set; }
    }
}
