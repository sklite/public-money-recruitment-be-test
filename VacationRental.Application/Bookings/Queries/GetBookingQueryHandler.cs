using System.Threading;
using System.Threading.Tasks;
using MediatR;
using VacationRental.Application.Bookings.Interfaces;
using VacationRental.Domain.Bookings;

namespace VacationRental.Application.Bookings.Queries
{
    public class GetBookingQueryHandler: IRequestHandler<GetBookingQuery, Booking>
    {
        private readonly IBookingRepository _bookingsRepository;

        public GetBookingQueryHandler(IBookingRepository bookingsRepository)
        {
            _bookingsRepository = bookingsRepository;
        }

        public async Task<Booking> Handle(GetBookingQuery request, CancellationToken cancellationToken)
        {
            return await _bookingsRepository.GetBooking(request.BookingId);
        }
    }
}