using System.Threading;
using System.Threading.Tasks;
using MediatR;
using VacationRental.Application.Bookings.Interfaces;
using VacationRental.Domain;

namespace VacationRental.Application.Bookings.Queries
{
    public class GetBookingQueryHandler: IRequestHandler<GetBookingQuery, BookingViewModel>
    {
        private readonly IBookingsRepository _bookingsRepository;

        public GetBookingQueryHandler(IBookingsRepository bookingsRepository)
        {
            _bookingsRepository = bookingsRepository;
        }

        public async Task<BookingViewModel> Handle(GetBookingQuery request, CancellationToken cancellationToken)
        {
            return await _bookingsRepository.GetBooking(request.BookingId);
        }
    }
}