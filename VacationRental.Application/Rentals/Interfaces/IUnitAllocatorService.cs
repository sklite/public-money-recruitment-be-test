using System;
using System.Threading.Tasks;

namespace VacationRental.Application.Rentals.Interfaces
{
    public interface IUnitAllocatorService
    {
        Task<int> AllocateFreeUnit(int rentalId, DateTime checkInDate, int nights);
    }
}