using MediatR;
using VacationRental.Domain;

namespace VacationRental.Application.Rentals.Commands
{
    public class CreateRentalCommand : IRequest<ResourceIdViewModel>
    {
        public int Units { get; set; }
    }
}