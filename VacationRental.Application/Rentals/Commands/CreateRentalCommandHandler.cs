using MediatR;
using System.Threading;
using System.Threading.Tasks;
using VacationRental.Application.Rentals.Interfaces;
using VacationRental.Domain;
using VacationRental.Domain.Rentals;

namespace VacationRental.Application.Rentals.Commands
{
    public class CreateRentalCommandHandler : IRequestHandler<CreateRentalCommand, ResourceId>
    {
        private readonly IRentalRepository _rentalRepository;

        public CreateRentalCommandHandler(IRentalRepository rentalRepository)
        {
            _rentalRepository = rentalRepository;
        }

        public async Task<ResourceId> Handle(CreateRentalCommand request, CancellationToken cancellationToken)
        {
            var newRental = new Rental
            {
                PreparationTimeInDays = request.PreparationTimeInDays,
                Units = request.Units
            };

            return await _rentalRepository.AddRental(newRental);
        }
    }
}