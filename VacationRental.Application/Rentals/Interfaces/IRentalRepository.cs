using System.Threading.Tasks;
using VacationRental.Domain;

namespace VacationRental.Application.Rentals.Interfaces
{
    public interface IRentalRepository
    {
        Task<ResourceIdViewModel> AddRental(int units);
        Task<RentalViewModel> GetRental(int rentalId);
    }
}