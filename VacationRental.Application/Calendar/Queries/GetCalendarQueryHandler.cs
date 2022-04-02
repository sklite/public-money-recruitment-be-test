using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using VacationRental.Application.Bookings.Interfaces;
using VacationRental.Application.Rentals.Interfaces;
using VacationRental.Domain;

namespace VacationRental.Application.Calendar.Queries
{
    public class GetCalendarQueryHandler : IRequestHandler<GetCalendarQuery, CalendarViewModel>
    {
        private readonly IBookingsRepository _bookingsRepository;
        private readonly IRentalRepository _rentalRepository;

        public GetCalendarQueryHandler(IBookingsRepository bookingsRepository, IRentalRepository rentalRepository)
        {
            _bookingsRepository = bookingsRepository;
            _rentalRepository = rentalRepository;
        }

        public async Task<CalendarViewModel> Handle(GetCalendarQuery request, CancellationToken cancellationToken)
        {
            if (request.Nights < 0)
                throw new ApplicationException("Nights must be positive");

            await _rentalRepository.GetRental(request.RentalId);
            var bookings = _bookingsRepository.GetAll();

            var result = new CalendarViewModel
            {
                RentalId = request.RentalId,
                Dates = new List<CalendarDateViewModel>()
            };
            for (var i = 0; i < request.Nights; i++)
            {
                var date = new CalendarDateViewModel
                {
                    Date = request.Start.Date.AddDays(i),
                    Bookings = new List<CalendarBookingViewModel>()
                };

                foreach (var booking in bookings.Values)
                {
                    if (booking.RentalId == request.RentalId
                        && booking.Start <= date.Date && booking.Start.AddDays(booking.Nights) > date.Date)
                    {
                        date.Bookings.Add(new CalendarBookingViewModel { Id = booking.Id });
                    }
                }

                result.Dates.Add(date);
            }

            return result;
        }
    }
}