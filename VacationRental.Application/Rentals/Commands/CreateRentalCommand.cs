using MediatR;
using VacationRental.Domain;

namespace VacationRental.Application.Rentals.Commands
{
    public record CreateRentalCommand(int Units, int PreparationTimeInDays) : IRequest<ResourceId>;
}