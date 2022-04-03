using System;

namespace VacationRental.Api.DTO.Responses.Bookings
{
    public class BookingResponse
    {
        public int Id { get; set; }
        public int RentalId { get; set; }
        public int Unit { get; set; }
        public DateTime Start { get; set; }
        public int Nights { get; set; }
    }
}
