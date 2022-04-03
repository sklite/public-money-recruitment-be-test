using System;
using System.Linq;
using System.Threading.Tasks;
using VacationRental.Application.Bookings.Interfaces;
using VacationRental.Application.Rentals.Interfaces;

namespace VacationRental.Application.Rentals.Services
{
    public class UnitAllocatorService : IUnitAllocatorService
    {
        private readonly IRentalRepository _rentalRepository;
        private readonly IBookingRepository _bookingsRepository;

        public UnitAllocatorService(IRentalRepository rentalRepository, IBookingRepository bookingsRepository)
        {
            _rentalRepository = rentalRepository;
            _bookingsRepository = bookingsRepository;
        }

        public async Task<int> AllocateFreeUnit(int rentalId, DateTime checkInDate, int nights)
        {
            var rental = await _rentalRepository.GetRental(rentalId);
            var rentalBookings = await _bookingsRepository.Get(book => book.RentalId == rental.Id);

            var requestEnd = checkInDate.AddDays(nights + rental.PreparationTimeInDays);
            var freeUnits = Enumerable.Range(1, rental.Units).ToHashSet();

            foreach (var booking in rentalBookings)
            {
                var bookingEnd = booking.Start.AddDays(booking.Nights + rental.PreparationTimeInDays);

                if (checkInDate < bookingEnd && booking.Start < requestEnd)
                    freeUnits.Remove(booking.Unit);
            }

            if (!freeUnits.Any())
                throw new ApplicationException("Not available");

            return freeUnits.First();
        }
    }
}