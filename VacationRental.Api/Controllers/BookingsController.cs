using System.Threading.Tasks;
using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using VacationRental.Api.DTO.Requests;
using VacationRental.Api.DTO.Responses;
using VacationRental.Api.DTO.Responses.Bookings;
using VacationRental.Application.Bookings.Commands;
using VacationRental.Application.Bookings.Queries;

namespace VacationRental.Api.Controllers
{
    [Route("api/v1/bookings")]
    [ApiController]
    public class BookingsController : ControllerBase
    {
        private readonly IMediator _mediator;
        private readonly IMapper _mapper;

        public BookingsController(IMediator mediator, IMapper mapper)
        {
            _mediator = mediator;
            _mapper = mapper;
        }

        [HttpGet("{bookingId:int}")]
        public async Task<BookingResponse> Get(int bookingId)
        {
            var result = await _mediator.Send(new GetBookingQuery(bookingId));
            return _mapper.Map<BookingResponse>(result);
        }

        [HttpPost]
        public async Task<ResourceIdResponse> Post(BookingRequest model)
        {
            var result = await _mediator.Send(new CreateBookingCommand(model.Nights, model.RentalId, model.Start));
            return _mapper.Map<ResourceIdResponse>(result);
        }
    }
}
