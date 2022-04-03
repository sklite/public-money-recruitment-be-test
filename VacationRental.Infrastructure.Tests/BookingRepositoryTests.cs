using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using VacationRental.Domain.Bookings;
using Xunit;

namespace VacationRental.Infrastructure.Tests
{
    public class BookingRepositoryTests
    {
        Dictionary<int, Booking> CreateBookings()
        {
            return new Dictionary<int, Booking>
            {
                { 1, new Booking { Id = 1, Nights = 5, RentalId = 444, Start = new DateTime(2000, 01, 16), Unit = 2 } },
                { 2, new Booking { Id = 2, Nights = 5, RentalId = 123, Start = new DateTime(2000, 01, 15), Unit = 1 } },
            };
        }

        [Fact]
        public async Task GetBooking_BookingExists_ReturnsBooking()
        {
            //Arrange
            var repo = new BookingRepository(CreateBookings());

            //Act
            var bookingResult = await repo.GetBooking(2);

            //Assert
            Assert.NotNull(bookingResult);
            Assert.Equal(2, bookingResult.Id);
            Assert.Equal(5, bookingResult.Nights);
            Assert.Equal(123, bookingResult.RentalId);
            Assert.Equal(new DateTime(2000, 01, 15), bookingResult.Start);
        }

        [Fact]
        public async Task GetBooking_BookingDoesntExist_ThrowsException()
        {
            //Arrange
            var repo = new BookingRepository(CreateBookings());

            //Act
            Task result() => repo.GetBooking(3);

            //Assert
            var exception = await Assert.ThrowsAsync<ApplicationException>(result);
            Assert.Equal("Booking not found", exception.Message);
        }

        [Fact]
        public async Task AddBooking_CorrectNewData_IncrementsId()
        {
            //Arrange
            var repo = new BookingRepository(CreateBookings());
            var newBook = new Booking()
            {
                Nights = 9,
                RentalId = 4,
                Start = DateTime.Now,
                Unit = 5
            };

            //Act
            var bookingResult = await repo.AddBooking(newBook);

            //Assert
            Assert.NotNull(bookingResult);
            Assert.Equal(3, newBook.Id);
            Assert.Equal(3, bookingResult.Id);
        }

        [Fact]
        public async Task Get_BookingExists_ReturnsBooking()
        {
            //Arrange
            var repo = new BookingRepository(CreateBookings());

            //Act
            var bookingResult = await repo.Get(book => book.RentalId == 444);

            //Assert
            Assert.NotNull(bookingResult);
            var bookingList = bookingResult.ToList();

            Assert.Single(bookingList);
            Assert.Equal(1, bookingList[0].Id);
            Assert.Equal(5, bookingList[0].Nights);
            Assert.Equal(444, bookingList[0].RentalId);
            Assert.Equal(new DateTime(2000, 01, 16), bookingList[0].Start);
        }
    }
}
