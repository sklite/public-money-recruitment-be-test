using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using VacationRental.Application.Rentals.Interfaces;
using VacationRental.Domain;

namespace VacationRental.Infrastructure
{
    public class RentalRepository : IRentalRepository
    {
        private readonly IDictionary<int, RentalViewModel> _rentals;

        public RentalRepository(IDictionary<int, RentalViewModel> rentals)
        {
            _rentals = rentals;
        }

        public async Task<ResourceIdViewModel> AddRental(int units)
        {
            var key = new ResourceIdViewModel { Id = _rentals.Keys.Count + 1 };

            _rentals.Add(key.Id, new RentalViewModel
            {
                Id = key.Id,
                Units = units
            });

            return key;
        }

        public async Task<RentalViewModel> GetRental(int rentalId)
        {
            if (!_rentals.ContainsKey(rentalId))
                throw new ApplicationException("Rental not found");

            return _rentals[rentalId];
        }
    }
}