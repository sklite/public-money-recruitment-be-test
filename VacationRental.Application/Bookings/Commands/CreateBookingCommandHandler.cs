using System;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using VacationRental.Application.Bookings.Interfaces;
using VacationRental.Application.Rentals.Interfaces;
using VacationRental.Domain;
using VacationRental.Domain.Bookings;

namespace VacationRental.Application.Bookings.Commands
{
    public class CreateBookingCommandHandler : IRequestHandler<CreateBookingCommand, ResourceId>
    {
        private readonly IUnitAllocatorService _rentalService;
        private readonly IBookingRepository _bookingsRepository;

        public CreateBookingCommandHandler(IUnitAllocatorService rentalService, IBookingRepository bookingsRepository)
        {
            _rentalService = rentalService;
            _bookingsRepository = bookingsRepository;
        }

        public async Task<ResourceId> Handle(CreateBookingCommand request, CancellationToken cancellationToken)
        {
            if (request.Nights <= 0)
                throw new ApplicationException("Nights must be positive");

            var unit = await _rentalService.AllocateFreeUnit(request.RentalId, request.Start, request.Nights);

            var newBooking = new Booking
            {
                Nights = request.Nights,
                RentalId = request.RentalId,
                Start = request.Start.Date,
                Unit = unit
            };

            return await _bookingsRepository.AddBooking(newBooking);
        }
    }
}