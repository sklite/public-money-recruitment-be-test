﻿using System;
using System.Collections.Generic;

namespace VacationRental.Domain
{
    public class CalendarDateViewModel
    {
        public DateTime Date { get; set; }
        public List<CalendarBookingViewModel> Bookings { get; set; }
    }
}
