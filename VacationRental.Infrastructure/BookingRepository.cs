using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using VacationRental.Application.Bookings.Interfaces;
using VacationRental.Domain;

namespace VacationRental.Infrastructure
{
    public class BookingRepository : IBookingsRepository
    {
        private readonly IDictionary<int, BookingViewModel> _bookings;

        public BookingRepository(IDictionary<int, BookingViewModel> bookings)
        {
            _bookings = bookings;
        }

        public async Task AddBooking(BookingViewModel booking)
        {
            _bookings[booking.Id] = booking;
        }

        public async Task<BookingViewModel> GetBooking(int bookingId)
        {
            if (!_bookings.ContainsKey(bookingId))
                throw new ApplicationException("Booking not found");

            return _bookings[bookingId];
        }

        public IDictionary<int, BookingViewModel> GetAll()
        {
            return _bookings;
        }
    }
}