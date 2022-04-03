using MediatR;
using VacationRental.Domain.Bookings;

namespace VacationRental.Application.Bookings.Queries
{
    public record GetBookingQuery(int BookingId) : IRequest<Booking>;
}