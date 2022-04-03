using AutoMapper;
using VacationRental.Api.DTO.Responses;
using VacationRental.Api.DTO.Responses.Rentals;
using VacationRental.Domain.Rentals;

namespace VacationRental.Api.Mappers
{
    public class RentalMapperProfile : Profile
    {
        public RentalMapperProfile()
        {
            CreateMap<Rental, RentalResponse>().ReverseMap();
            CreateMap<Rental, ResourceIdResponse>();
        }
    }
}