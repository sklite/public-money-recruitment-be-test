using System;
using System.Threading;
using System.Threading.Tasks;
using Moq;
using VacationRental.Application.Bookings.Interfaces;
using VacationRental.Application.Bookings.Queries;
using VacationRental.Domain.Bookings;
using Xunit;

namespace VacationRental.Application.Tests.Bookings
{
    public class GetBookingQueryHandlerTests
    {
        private readonly Mock<IBookingRepository> _bookingsRepo;
        public GetBookingQueryHandlerTests()
        {
            _bookingsRepo = new Mock<IBookingRepository>();
        }

        [Fact]
        public async Task Handle_WithProvidedCommand_ReturnsBooking()
        {
            //Arrange
            var commandHandler = new GetBookingQueryHandler(_bookingsRepo.Object);

            var request = new GetBookingQuery(100);
            var repoResult = new Booking { Id = 100, Nights = 9, RentalId = 2, Start = new DateTime(2000, 01,01), Unit = 5};
            int callback = 0;

            _bookingsRepo.Setup(r => r.GetBooking(It.IsAny<int>()))
                .ReturnsAsync(repoResult)
                .Callback((int bookingId) => callback = bookingId);

            //Act
            var result = await commandHandler.Handle(request, CancellationToken.None);

            //Assert
            Assert.NotNull(result);
            Assert.Equal(100, callback);
            Assert.Equal(100, result.Id);
            Assert.Equal(2, result.RentalId);
            Assert.Equal(9, result.Nights);
            Assert.Equal(new DateTime(2000, 01, 01), result.Start);
        }
    }
}
