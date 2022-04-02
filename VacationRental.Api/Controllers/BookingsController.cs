using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using VacationRental.Api.Models;
using VacationRental.Application.Bookings.Commands;
using VacationRental.Application.Bookings.Queries;
using VacationRental.Domain;

namespace VacationRental.Api.Controllers
{
    [Route("api/v1/bookings")]
    [ApiController]
    public class BookingsController : ControllerBase
    {
        private readonly IMediator _mediator;

        public BookingsController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet]
        [Route("{bookingId:int}")]
        public async Task<BookingViewModel> Get(int bookingId, CancellationToken token)
        {
            var getBookingQuery = new GetBookingQuery
            {
                BookingId = bookingId
            };

            return await _mediator.Send(getBookingQuery, token);
        }

        [HttpPost]
        public async Task<ResourceIdViewModel> Post(BookingBindingModel model)
        {
            var createBookingCommand = new CreateBookingCommand
            {
                Nights = model.Nights,
                RentalId = model.RentalId,
                Start = model.Start
            };

            return await _mediator.Send(createBookingCommand);
        }
    }
}
