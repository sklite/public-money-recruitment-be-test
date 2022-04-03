namespace VacationRental.Domain.Rentals
{
    public class Rental : ResourceId
    {
        public int Units { get; set; }
        public int PreparationTimeInDays { get; set; }
    }
}