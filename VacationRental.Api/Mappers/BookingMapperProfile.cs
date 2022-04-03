using AutoMapper;
using VacationRental.Api.DTO.Responses;
using VacationRental.Api.DTO.Responses.Bookings;
using VacationRental.Domain.Bookings;

namespace VacationRental.Api.Mappers
{
    public class BookingMapperProfile : Profile
    {
        public BookingMapperProfile()
        {
            CreateMap<Booking, BookingResponse>().ReverseMap();
            CreateMap<Booking, ResourceIdResponse>();
        }
    }
}