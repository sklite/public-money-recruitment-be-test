using System;

namespace VacationRental.Domain.Bookings
{
    public class Booking : ResourceId
    {
        public int RentalId { get; set; }
        public int Unit { get; set; }
        public DateTime Start { get; set; }
        public int Nights { get; set; }
    }
}