using System.Threading;
using System.Threading.Tasks;
using Moq;
using VacationRental.Application.Rentals.Commands;
using VacationRental.Application.Rentals.Interfaces;
using VacationRental.Domain;
using VacationRental.Domain.Rentals;
using Xunit;

namespace VacationRental.Application.Tests.Rentals
{
    public class CreateRentalCommandHandlerTests
    {
        private readonly Mock<IRentalRepository> _rentalsRepo;
        public CreateRentalCommandHandlerTests()
        {
            _rentalsRepo = new Mock<IRentalRepository>();
        }

        [Fact]
        public async Task Handle_WithProvidedCommand_CreatesVehicle()
        {
            //Arrange
            var commandHandler = new CreateRentalCommandHandler(_rentalsRepo.Object);

            var request = new CreateRentalCommand(20, 1);
            var repoResult = new ResourceId { Id = 100 };
            Rental callback = null;

            _rentalsRepo.Setup(r => r.AddRental(It.IsAny<Rental>()))
                .ReturnsAsync(repoResult)
                .Callback((Rental rental) => callback = rental);


            //Act
            var result = await commandHandler.Handle(request, CancellationToken.None);


            //Assert
            Assert.NotNull(result);
            Assert.Equal(100, result.Id);
            Assert.Equal(1, callback.PreparationTimeInDays);
            Assert.Equal(20, callback.Units);

        }
    }
}
