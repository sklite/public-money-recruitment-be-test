using System;
using MediatR;
using VacationRental.Domain;

namespace VacationRental.Application.Bookings.Commands
{
    public record CreateBookingCommand(int Nights, int RentalId, DateTime Start) : IRequest<ResourceId>;
}