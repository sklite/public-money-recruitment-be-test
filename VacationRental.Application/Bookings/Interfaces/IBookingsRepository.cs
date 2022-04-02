using System.Collections.Generic;
using System.Threading.Tasks;
using VacationRental.Domain;

namespace VacationRental.Application.Bookings.Interfaces
{
    public interface IBookingsRepository
    {
        Task AddBooking(BookingViewModel booking);
        Task<BookingViewModel> GetBooking(int bookingId);
        IDictionary<int, BookingViewModel> GetAll();
    }
}