using System.Collections.Generic;

namespace VacationRental.Domain.Calendars
{
    public class Calendar
    {
        public int RentalId { get; set; }
        public List<CalendarDate> Dates { get; set; }

    }
}
