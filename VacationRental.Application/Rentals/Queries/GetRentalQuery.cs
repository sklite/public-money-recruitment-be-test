using MediatR;
using VacationRental.Domain;

namespace VacationRental.Application.Rentals.Queries
{
    public class GetRentalQuery : IRequest<RentalViewModel>
    {
        public int RentalId { get; set; }
    }
}