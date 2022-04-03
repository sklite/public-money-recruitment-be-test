using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using VacationRental.Application.Rentals.Interfaces;
using VacationRental.Domain;
using VacationRental.Domain.Rentals;

namespace VacationRental.Infrastructure
{
    public class RentalRepository : IRentalRepository
    {
        private readonly IDictionary<int, Rental> _rentals;

        public RentalRepository(IDictionary<int, Rental> rentals)
        {
            _rentals = rentals;
        }

        public async Task<ResourceId> AddRental(Rental rental)
        {
            rental.Id = _rentals.Keys.Count + 1;
            _rentals[rental.Id] = rental;

            return rental;
        }

        public async Task<Rental> GetRental(int rentalId)
        {
            if (!_rentals.ContainsKey(rentalId))
                throw new ApplicationException("Rental not found");

            return _rentals[rentalId];
        }
    }
}