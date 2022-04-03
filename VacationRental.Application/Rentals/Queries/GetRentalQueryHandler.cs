using System.Threading;
using System.Threading.Tasks;
using MediatR;
using VacationRental.Application.Rentals.Interfaces;
using VacationRental.Domain.Rentals;

namespace VacationRental.Application.Rentals.Queries
{
    public class GetRentalQueryHandler: IRequestHandler<GetRentalQuery, Rental>
    {
        private readonly IRentalRepository _repository;

        public GetRentalQueryHandler(IRentalRepository repository)
        {
            _repository = repository;
        }

        public async Task<Rental> Handle(GetRentalQuery request, CancellationToken cancellationToken)
        {
            return await _repository.GetRental(request.RentalId);
        }
    }
}