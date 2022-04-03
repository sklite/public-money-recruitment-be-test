using System.Collections.Generic;

namespace VacationRental.Api.DTO.Responses.Calendar
{
    public class CalendarResponse
    {
        public int RentalId { get; set; }
        public List<CalendarDateResponse> Dates { get; set; }

    }
}
