using System;
using MediatR;
using VacationRental.Domain;

namespace VacationRental.Application.Bookings.Commands
{
    public class CreateBookingCommand : IRequest<ResourceIdViewModel>
    {
        public int RentalId { get; set; }

        public DateTime Start
        {
            get => _startIgnoreTime;
            set => _startIgnoreTime = value.Date;
        }

        private DateTime _startIgnoreTime;
        public int Nights { get; set; }
    }
}