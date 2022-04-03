using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using VacationRental.Domain.Rentals;
using Xunit;

namespace VacationRental.Infrastructure.Tests
{
    public class RentalRepositoryTests
    {
        Dictionary<int, Rental> CreateRentals()
        {
            return new Dictionary<int, Rental>
            {
                { 1, new Rental { Id = 1, PreparationTimeInDays = 2, Units = 3 } },
                { 2, new Rental { Id = 2, PreparationTimeInDays = 1, Units = 10 } }
            };
        }

        [Fact]
        public async Task GetRental_RentalExists_ReturnsRental()
        {
            //Arrange
            var repo = new RentalRepository(CreateRentals());

            //Act
            var rentalResult = await repo.GetRental(2);

            //Assert
            Assert.NotNull(rentalResult);
            Assert.Equal(2, rentalResult.Id);
            Assert.Equal(1, rentalResult.PreparationTimeInDays);
            Assert.Equal(10, rentalResult.Units);
        }

        [Fact]
        public async Task GetRental_rentalDoesntExist_ThrowsException()
        {
            //Arrange
            var repo = new RentalRepository(CreateRentals());

            //Act
            Task result() => repo.GetRental(3);

            //Assert
            var exception = await Assert.ThrowsAsync<ApplicationException>(result);
            Assert.Equal("Rental not found", exception.Message);
        }

        [Fact]
        public async Task AddRental_CorrectNewData_IncrementsId()
        {
            //Arrange
            var repo = new RentalRepository(CreateRentals());
            var newRental = new Rental()
            {
                PreparationTimeInDays = 3,
                Units = 4
            };

            //Act
            var rentalResult = await repo.AddRental(newRental);

            //Assert
            Assert.NotNull(rentalResult);
            Assert.Equal(3, rentalResult.Id);
            Assert.Equal(3, newRental.Id);
        }
    }
}
