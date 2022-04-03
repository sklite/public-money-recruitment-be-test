using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using VacationRental.Application.Bookings.Interfaces;
using VacationRental.Application.Extensions;
using VacationRental.Application.Rentals.Interfaces;
using VacationRental.Domain.Calendars;

namespace VacationRental.Application.Calendar.Queries
{
    public class GetCalendarQueryHandler : IRequestHandler<GetCalendarQuery, Domain.Calendars.Calendar>
    {
        private readonly IBookingRepository _bookingsRepository;
        private readonly IRentalRepository _rentalRepository;

        public GetCalendarQueryHandler(IBookingRepository bookingsRepository, IRentalRepository rentalRepository)
        {
            _bookingsRepository = bookingsRepository;
            _rentalRepository = rentalRepository;
        }

        public async Task<Domain.Calendars.Calendar> Handle(GetCalendarQuery request, CancellationToken cancellationToken)
        {
            if (request.Nights < 0)
                throw new ApplicationException("Nights must be positive");

            var rental = await _rentalRepository.GetRental(request.RentalId);
            var bookings = await _bookingsRepository.Get(booking => booking.RentalId == request.RentalId);

            var result = new Domain.Calendars.Calendar
            {
                RentalId = request.RentalId,
                Dates = new List<CalendarDate>()
            };

            for (var i = 0; i < request.Nights; i++)
            {
                var date = new CalendarDate
                {
                    Date = request.Start.Date.AddDays(i),
                    Bookings = new List<CalendarBooking>(),
                    PreparationTimes = new List<CalendarPreparationTime>()
                };

                foreach (var booking in bookings)
                {
                    var checkInDate = booking.Start;
                    var checkOutDate = booking.Start.AddDays(booking.Nights);
                    var preparationFinishDate = checkOutDate.AddDays(rental.PreparationTimeInDays);

                    if (date.Date.IsBetweenDates(checkInDate, checkOutDate))
                    {
                        date.Bookings.Add(new CalendarBooking { Id = booking.Id, Unit = booking.Unit});
                    }

                    if (date.Date.IsBetweenDates(checkOutDate, preparationFinishDate))
                    {
                        date.PreparationTimes.Add(new CalendarPreparationTime(){Unit = booking.Unit});
                    }
                }

                result.Dates.Add(date);
            }

            return result;
        }
    }
}