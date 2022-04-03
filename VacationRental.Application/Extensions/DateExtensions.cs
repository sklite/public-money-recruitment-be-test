using System;

namespace VacationRental.Application.Extensions
{
    public static class DateExtensions
    {
        public static bool IsBetweenDates(this DateTime datetime, DateTime begin, DateTime end)
        {
            return datetime >= begin && datetime < end;
        }
    }
}