using System;
using MediatR;
using VacationRental.Domain;

namespace VacationRental.Application.Calendar.Queries
{
    public class GetCalendarQuery : IRequest<CalendarViewModel>
    {
        public int RentalId { get; set; }
        public DateTime Start { get; set; }
        public int Nights { get; set; }
    }
}