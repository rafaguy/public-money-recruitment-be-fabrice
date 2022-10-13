using System;
using System.Collections.Generic;
using System.Text;
using VacationRental.Api.Models;
using VacationRental.Api.Services;
using Xunit;

namespace VacationRental.Api.Tests
{
    public class RentalServiceTest
    {
        private readonly IDictionary<int, RentalViewModel> _rentalRepo;
        private readonly IRentalService _rentalService;
        private RentalViewModel _expectRental = new RentalViewModel
        {
            Id = 1,
            Units = 3,
            PreparationTimeInDays = 2
        };
        
        public RentalServiceTest()
        {
            _rentalRepo = new Dictionary<int, RentalViewModel>();
          
            _rentalService = new RentalService(_rentalRepo);
        }
        [Fact]
       public void AddRental_WithUnit3_And_PreparationTime2()
        {
            //arr in ctor
            //act
             _rentalService.Add(1, 3, 2);

            // Assert
            Assert.True(_rentalRepo.ContainsKey(1));
            Assert.Equal(3, _rentalRepo[1].Units);
            Assert.Equal(2, _rentalRepo[1].PreparationTimeInDays);
        }
        [Fact]
        public void GetRental_AddSomeRental_TryingToGetNewlyAddedRental()
        {
            _rentalService.Add(1, 3, 2);

            var rental = _rentalService.GetRental(1);
            Assert.Null(_rentalService.GetRental(2));

            Assert.Equal(1, rental.Id);
            Assert.Equal(3, rental.Units);
            Assert.Equal(2, rental.PreparationTimeInDays);
            Assert.False(_rentalService.CheckAvalaibility(2));
            Assert.True(_rentalService.CheckAvalaibility(1));

        }
    }
}
