using System;
using System.Collections.Generic;
using System.Linq;
using VacationRental.Api.Models;

namespace VacationRental.Api.Services
{
    public class RentalService : IRentalService
    {
        private readonly IDictionary<int, RentalViewModel> _rentals;
      
        public RentalService(IDictionary<int, RentalViewModel> rental)
        {
            _rentals = rental;
           
        }
        public void Add(int id, int units, int preparationTimeInDay)
        {
            _rentals.Add(id, new RentalViewModel
            {
                Id = id,
                Units = units,
                PreparationTimeInDays = preparationTimeInDay
            }) ;
        }
       public RentalViewModel GetRental(int rentalId)
        {
            if(!_rentals.ContainsKey(rentalId))
            {
                return null;
            }
            return _rentals[rentalId];
        }
        public bool CheckAvalaibility(int rentalId)
        {
            return _rentals.ContainsKey(rentalId);
        }

        public int GetRentalCount()
        {
            return _rentals.Keys.Count;
        }

      
    }
}
