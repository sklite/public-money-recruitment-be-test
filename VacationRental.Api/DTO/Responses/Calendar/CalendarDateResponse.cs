using System;
using System.Collections.Generic;

namespace VacationRental.Api.DTO.Responses.Calendar
{
    public class CalendarDateResponse
    {
        public DateTime Date { get; set; }
        public List<CalendarBookingResponse> Bookings { get; set; }
        public List<CalendarPreparationTimeResponse> PreparationTimes { get; set; }

    }
}
