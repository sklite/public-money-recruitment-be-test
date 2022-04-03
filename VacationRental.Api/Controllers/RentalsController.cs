using System.Threading.Tasks;
using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using VacationRental.Api.DTO.Requests;
using VacationRental.Api.DTO.Responses.Rentals;
using VacationRental.Application.Rentals.Commands;
using VacationRental.Application.Rentals.Queries;
using ResourceIdResponse = VacationRental.Api.DTO.Responses.ResourceIdResponse;

namespace VacationRental.Api.Controllers
{
    [Route("api/v1/rentals")]
    [ApiController]
    public class RentalsController : ControllerBase
    {
        private readonly IMediator _mediator;
        private readonly IMapper _mapper;

        public RentalsController(IMediator mediator, IMapper mapper)
        {
            _mediator = mediator;
            _mapper = mapper;
        }

        [HttpGet("{rentalId:int}")]
        public async Task<RentalResponse> Get(int rentalId)
        {
            var result = await _mediator.Send(new GetRentalQuery(rentalId));
            return _mapper.Map<RentalResponse>(result);
        }

        [HttpPost]
        public async Task<ResourceIdResponse> Post(RentalRequest model)
        {
            var result = await _mediator.Send(new CreateRentalCommand(model.Units, model.PreparationTimeInDays));
            return _mapper.Map<ResourceIdResponse>(result);
        }
    }
}
