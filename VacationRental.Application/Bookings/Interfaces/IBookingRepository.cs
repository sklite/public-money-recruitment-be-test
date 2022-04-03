using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using VacationRental.Domain;
using VacationRental.Domain.Bookings;

namespace VacationRental.Application.Bookings.Interfaces
{
    public interface IBookingRepository
    {
        Task<ResourceId> AddBooking(Booking booking);
        Task<Booking> GetBooking(int bookingId);
        Task<IEnumerable<Booking>> Get(Func<Booking, bool> filter);
    }
}