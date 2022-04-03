using AutoMapper;
using VacationRental.Api.DTO.Responses.Calendar;
using VacationRental.Domain.Calendars;

namespace VacationRental.Api.Mappers
{
    public class CalendarMapperProfile : Profile
    {
        public CalendarMapperProfile()
        {
            CreateMap<Calendar, CalendarResponse>().ReverseMap();
            CreateMap<CalendarDate, CalendarDateResponse>().ReverseMap();
            CreateMap<CalendarBooking, CalendarBookingResponse>().ReverseMap();
            CreateMap<CalendarPreparationTime, CalendarPreparationTimeResponse>().ReverseMap();
        }
    }
}