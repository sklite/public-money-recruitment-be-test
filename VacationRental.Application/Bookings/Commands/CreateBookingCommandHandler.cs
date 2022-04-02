using System;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using VacationRental.Application.Bookings.Interfaces;
using VacationRental.Application.Rentals.Interfaces;
using VacationRental.Domain;

namespace VacationRental.Application.Bookings.Commands
{
    public class CreateBookingCommandHandler : IRequestHandler<CreateBookingCommand, ResourceIdViewModel>
    {
        private readonly IRentalRepository _rentalRepository;
        private readonly IBookingsRepository _bookingsRepository;

        public CreateBookingCommandHandler(IRentalRepository rentalRepository, IBookingsRepository bookingsRepository)
        {
            _rentalRepository = rentalRepository;
            _bookingsRepository = bookingsRepository;
        }

        public async Task<ResourceIdViewModel> Handle(CreateBookingCommand request, CancellationToken cancellationToken)
        {
            if (request.Nights <= 0)
                throw new ApplicationException("Nights must be positive");

            var rental = await _rentalRepository.GetRental(request.RentalId);
            var bookings = _bookingsRepository.GetAll();

            for (var i = 0; i < request.Nights; i++)
            {
                var count = 0;
                foreach (var booking in bookings.Values)
                {
                    if (booking.RentalId == request.RentalId
                        && (booking.Start <= request.Start.Date && booking.Start.AddDays(booking.Nights) > request.Start.Date)
                        || (booking.Start < request.Start.AddDays(request.Nights) && booking.Start.AddDays(booking.Nights) >= request.Start.AddDays(request.Nights))
                        || (booking.Start > request.Start && booking.Start.AddDays(booking.Nights) < request.Start.AddDays(request.Nights)))
                    {
                        count++;
                    }
                }
                if (count >= rental.Units)
                    throw new ApplicationException("Not available");
            }

            var key = new ResourceIdViewModel { Id = bookings.Keys.Count + 1 };

            var newBooking = new BookingViewModel
            {
                Id = key.Id,
                Nights = request.Nights,
                RentalId = request.RentalId,
                Start = request.Start.Date
            };

            await _bookingsRepository.AddBooking(newBooking);

            return key;
        }
    }
}