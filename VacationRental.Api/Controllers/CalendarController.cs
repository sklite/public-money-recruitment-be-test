using System;
using System.Threading.Tasks;
using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using VacationRental.Api.DTO.Responses.Calendar;
using VacationRental.Application.Calendar.Queries;

namespace VacationRental.Api.Controllers
{
    [Route("api/v1/calendar")]
    [ApiController]
    public class CalendarController : ControllerBase
    {
        private readonly IMediator _mediator;
        private readonly IMapper _mapper;

        public CalendarController(IMediator mediator, IMapper mapper)
        {
            _mediator = mediator;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<CalendarResponse> Get(int rentalId, DateTime start, int nights)
        {
            var result = await _mediator.Send(new GetCalendarQuery(rentalId, start, nights));
            return _mapper.Map<CalendarResponse>(result);
        }
    }
}
