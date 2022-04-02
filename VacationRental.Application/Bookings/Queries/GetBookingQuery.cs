using MediatR;
using VacationRental.Domain;

namespace VacationRental.Application.Bookings.Queries
{
    public class GetBookingQuery : IRequest<BookingViewModel>
    {
        public int BookingId { get; set; }
    }
}