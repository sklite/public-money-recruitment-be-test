using System.Threading.Tasks;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using VacationRental.Api.Models;
using VacationRental.Application.Rentals.Commands;
using VacationRental.Application.Rentals.Queries;
using VacationRental.Domain;

namespace VacationRental.Api.Controllers
{
    [Route("api/v1/rentals")]
    [ApiController]
    public class RentalsController : ControllerBase
    {
        private readonly IMediator _mediator;

        public RentalsController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet]
        [Route("{rentalId:int}")]
        public async Task<RentalViewModel> Post(int rentalId)
        {
            var command = new GetRentalQuery { RentalId = rentalId };
            return await _mediator.Send(command);
        }

        [HttpPost]
        public async Task<ResourceIdViewModel> Post(RentalBindingModel model)
        {
            var command = new CreateRentalCommand { Units = model.Units };
            return await _mediator.Send(command);
        }
    }
}
