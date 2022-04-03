using System.Threading;
using System.Threading.Tasks;
using Moq;
using VacationRental.Application.Rentals.Interfaces;
using VacationRental.Application.Rentals.Queries;
using VacationRental.Domain.Rentals;
using Xunit;

namespace VacationRental.Application.Tests.Rentals
{
    public class GetRentalQueryHandlerTests
    {
        private readonly Mock<IRentalRepository> _rentalsRepo;
        public GetRentalQueryHandlerTests()
        {
            _rentalsRepo = new Mock<IRentalRepository>();
        }

        [Fact]
        public async Task Handle_WithProvidedCommand_CreatesVehicle()
        {
            //Arrange
            var commandHandler = new GetRentalQueryHandler(_rentalsRepo.Object);

            var request = new GetRentalQuery(100);
            var repoResult = new Rental { Id = 100, PreparationTimeInDays = 2, Units = 20};
            int callback = 0;

            _rentalsRepo.Setup(r => r.GetRental(It.IsAny<int>()))
                .ReturnsAsync(repoResult)
                .Callback((int rentalId) => callback = rentalId);


            //Act
            var result = await commandHandler.Handle(request, CancellationToken.None);


            //Assert
            Assert.NotNull(result);
            Assert.Equal(100, result.Id);
            Assert.Equal(2, result.PreparationTimeInDays);
            Assert.Equal(20, result.Units);
            Assert.Equal(100, callback);
        }
    }
}
