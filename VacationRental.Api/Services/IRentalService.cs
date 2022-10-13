using VacationRental.Api.Models;

namespace VacationRental.Api.Services
{
    public interface IRentalService
    {
        bool CheckAvalaibility(int rentalId);

        void Add(int id, int units, int preparationTimeInDay);
        RentalViewModel GetRental(int rentalId);
        int GetRentalCount();
      
    }
}
