using System;
using MediatR;

namespace VacationRental.Application.Calendar.Queries
{
    public record GetCalendarQuery(int RentalId, DateTime Start, int Nights) : IRequest<Domain.Calendars.Calendar>;
}