using System.Threading.Tasks;
using VacationRental.Domain;
using VacationRental.Domain.Rentals;

namespace VacationRental.Application.Rentals.Interfaces
{
    public interface IRentalRepository
    {
        Task<ResourceId> AddRental(Rental rental);
        Task<Rental> GetRental(int rentalId);
    }
}