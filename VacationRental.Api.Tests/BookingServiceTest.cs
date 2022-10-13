using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using VacationRental.Api.Models;
using VacationRental.Api.Services;
using Xunit;

namespace VacationRental.Api.Tests
{
    public class BookingServiceTest
    {
        private readonly IDictionary<int, RentalViewModel> _rentals;
        private readonly IDictionary<int, BookingViewModel> _bookings;
        private readonly IDictionary<int, List<PreparationTimeViewModel>> _preparationTime;
        private readonly IBookingService _bookingService;
        public BookingServiceTest()
        {
            _rentals = new Dictionary<int, RentalViewModel>();
            _bookings = new Dictionary<int, BookingViewModel>();
            _preparationTime = new Dictionary<int, List<PreparationTimeViewModel>>();
            _bookingService = new BookingService(_rentals, _bookings, _preparationTime);
            // Add some  rental test
            _rentals.Add(1, new RentalViewModel
            {
                Id = 1,
                Units = 3,
                PreparationTimeInDays = 2
            });

        }

        [Fact]
        public void AddBookingTest()
        {
            var bookingId = 1;
            var rentalId = 1;
            var start = new DateTime(2022, 10, 12);
            var night=3;
            var availableUnit = 1;
            _bookingService.AddBooking(bookingId, rentalId,start, night, availableUnit);
            Assert.True(_bookings.ContainsKey(1));
            Assert.Equal(bookingId, _bookings[1].Id);
            Assert.Equal(start, _bookings[1].Start);
            Assert.Equal(night, _bookings[1].Nights);
            Assert.Equal(availableUnit, _bookings[1].Unit);

            Assert.Equal(_bookings[1].Unit, _preparationTime[1].First().Unit);
            Assert.Equal(new DateTime(2022, 10, 12).AddDays(night), _preparationTime[1].First().StartDate);
        }
        [Fact]
        public void CheckAvailability_WithDifferentScenario()
        {
            // check book availability for rentalId=1,startdate='2022-10-12',nights=3 there must be rental available as there are
            // 3 units
            var bookingId = 1;
            var rentalId = 1;
            var start = new DateTime(2022, 10, 12);
            var night = 3;
            
           var unitAvailability= _bookingService.CheckUnitAvailability(rentalId, start, night, out int availableUnit);
            Assert.True(unitAvailability);
          

            // Add the booking
            _bookingService.AddBooking(bookingId, rentalId, start, night, availableUnit);

            // Check  booking availability with the same start date and night, should return true and available unit is 2
            unitAvailability = _bookingService.CheckUnitAvailability(rentalId, start, night, out  availableUnit);
            Assert.True(unitAvailability);
         
            // Add the booking
            _bookingService.AddBooking(2, rentalId, start, night, availableUnit);
            // Check  booking availability with the same start date and night, should return true and available unit is 3
            unitAvailability = _bookingService.CheckUnitAvailability(rentalId, start, night, out availableUnit);
            Assert.True(unitAvailability);
           
            // Add the booking
            _bookingService.AddBooking(3, rentalId, start, night, availableUnit);

            // Check  booking availability with the same start date and night, should return False as all unit occupied
            unitAvailability = _bookingService.CheckUnitAvailability(rentalId, start, night, out availableUnit);

            Assert.False(unitAvailability);
            // check book availability for rentalId=1,startdate='2022-10-15',nights=3. there is no unit available as all rent are 
            // occupied for preparations
            unitAvailability = _bookingService.CheckUnitAvailability(rentalId, start.AddDays(night), night, out availableUnit);

            Assert.False(unitAvailability);
        }

    }
}
