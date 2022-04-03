using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using VacationRental.Application.Bookings.Interfaces;
using VacationRental.Domain;
using VacationRental.Domain.Bookings;

namespace VacationRental.Infrastructure
{
    public class BookingRepository : IBookingRepository
    {
        private readonly IDictionary<int, Booking> _bookings;

        public BookingRepository(IDictionary<int, Booking> bookings)
        {
            _bookings = bookings;
        }
        
        public async Task<ResourceId> AddBooking(Booking booking)
        {
            booking.Id = _bookings.Count + 1;
            _bookings[booking.Id] = booking;

            return booking;
        }

        public async Task<Booking> GetBooking(int bookingId)
        {
            if (!_bookings.ContainsKey(bookingId))
                throw new ApplicationException("Booking not found");

            return _bookings[bookingId];
        }

        public async Task<IEnumerable<Booking>> Get(Func<Booking, bool> filter)
        {
            return _bookings.Values.Where(filter);
        }
    }
}