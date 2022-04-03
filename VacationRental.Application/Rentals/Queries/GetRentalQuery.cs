using MediatR;
using VacationRental.Domain.Rentals;

namespace VacationRental.Application.Rentals.Queries
{
    public record GetRentalQuery(int RentalId) : IRequest<Rental>;
}