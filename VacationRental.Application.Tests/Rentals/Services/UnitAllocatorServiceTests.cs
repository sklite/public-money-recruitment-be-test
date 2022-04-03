using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Moq;
using VacationRental.Application.Bookings.Interfaces;
using VacationRental.Application.Rentals.Interfaces;
using VacationRental.Application.Rentals.Services;
using VacationRental.Domain.Bookings;
using VacationRental.Domain.Rentals;
using Xunit;

namespace VacationRental.Application.Tests.Rentals.Services
{
    public class UnitAllocatorServiceTests
    {
        private readonly Mock<IRentalRepository> _rentalsRepo;
        private readonly Mock<IBookingRepository> _bookingsRepo;
        public UnitAllocatorServiceTests()
        {
            _rentalsRepo = new Mock<IRentalRepository>();
            _bookingsRepo = new Mock<IBookingRepository>();
        }

        Dictionary<int, Booking> CreateBookings()
        {
            return new Dictionary<int, Booking>
            {
                { 1, new Booking { Id = 1, Nights = 5, RentalId = 111, Start = new DateTime(2000, 01, 01), Unit = 1 } },
                { 2, new Booking { Id = 2, Nights = 10, RentalId = 111, Start = new DateTime(2000, 01, 01), Unit = 2 } },
            };
        }

        [Fact]
        public async Task AllocateFreeUnit_EnoughUnits_AllocatesFreeUnit()
        {
            //Arrange
            var rentalService = new UnitAllocatorService(_rentalsRepo.Object, _bookingsRepo.Object);

            _rentalsRepo.Setup(r => r.GetRental(It.IsAny<int>()))
                .ReturnsAsync(new Rental { Id = 111, PreparationTimeInDays = 1, Units = 2 });

            _bookingsRepo.Setup(r => r.Get(It.IsAny<Func<Booking, bool>>()))
                .ReturnsAsync(CreateBookings().Values);


            //Act
            var result = await rentalService.AllocateFreeUnit(111, new DateTime(2000, 01, 07), 2);


            //Assert
            Assert.Equal(1, result);
        }

        [Fact]
        public async Task AllocateFreeUnit_NotEnoughUnits_ThrowsException()
        {
            //Arrange
            var rentalService = new UnitAllocatorService(_rentalsRepo.Object, _bookingsRepo.Object);

            _rentalsRepo.Setup(r => r.GetRental(It.IsAny<int>()))
                .ReturnsAsync(new Rental { Id = 111, PreparationTimeInDays = 1, Units = 2 });

            _bookingsRepo.Setup(r => r.Get(It.IsAny<Func<Booking, bool>>()))
                .ReturnsAsync(CreateBookings().Values);


            //Act
            Task result() => rentalService.AllocateFreeUnit(111, new DateTime(2000, 01, 05), 2);


            //Assert
            var exception = await Assert.ThrowsAsync<ApplicationException>(result);
            Assert.Equal("Not available", exception.Message);
        }
    }
}
