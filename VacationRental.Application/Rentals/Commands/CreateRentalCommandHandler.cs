using MediatR;
using System.Threading;
using System.Threading.Tasks;
using VacationRental.Application.Rentals.Interfaces;
using VacationRental.Domain;

namespace VacationRental.Application.Rentals.Commands
{
    public class CreateRentalCommandHandler : IRequestHandler<CreateRentalCommand, ResourceIdViewModel>
    {
        private readonly IRentalRepository _rentalRepository;

        public CreateRentalCommandHandler(IRentalRepository rentalRepository)
        {
            _rentalRepository = rentalRepository;
        }

        public async Task<ResourceIdViewModel> Handle(CreateRentalCommand request, CancellationToken cancellationToken)
        {
            return await _rentalRepository.AddRental(request.Units);
        }
    }
}