using System;
using System.Collections.Generic;
using System.Linq;
using VacationRental.Api.Models;

namespace VacationRental.Api.Services
{
    public class CalendarService : ICalendarService
    {
        private readonly IDictionary<int, RentalViewModel> _rentals;
        private readonly IDictionary<int, BookingViewModel> _bookings;
        private readonly IDictionary<int, List<PreparationTimeViewModel>> _preparationTime;
        public CalendarService(IDictionary<int, RentalViewModel> rentals, IDictionary<int, BookingViewModel> bookings, IDictionary<int, List<PreparationTimeViewModel>> preparationTime)
        {
            _rentals = rentals;
            _bookings = bookings;
            _preparationTime = preparationTime;
        }

        public CalendarViewModel RetrieveBookingInfo(int rentalId, DateTime start, int nights)
        {
            var result = new CalendarViewModel
            {
                RentalId = rentalId,
                Dates = new List<CalendarDateViewModel>()
            };
            var preparationTimeForRental =_preparationTime.ContainsKey(rentalId)? _preparationTime[rentalId]:new List<PreparationTimeViewModel>();
            var rentalInfo = _rentals[rentalId];
            for (var i = 0; i < nights; i++)
            {
                var date = new CalendarDateViewModel
                {
                    Date = start.Date.AddDays(i),
                    Bookings = new List<CalendarBookingViewModel>(),
                    PreparationTimes=new List<UnitViewModel>()
                    
                };

                var bookingsForRental = _bookings.Values.Where(b => b.RentalId == rentalId).ToList();
                foreach (var booking in bookingsForRental)
                {
                    if (booking.Start <= date.Date && booking.Start.AddDays(booking.Nights) > date.Date)
                    {
                        date.Bookings.Add(new CalendarBookingViewModel { Id = booking.Id,Unit=booking.Unit });
                    }
                    if(preparationTimeForRental.Any(p=>p.BookingId==booking.Id&&p.StartDate<=date.Date&&p.StartDate.AddDays(rentalInfo.PreparationTimeInDays)>date.Date ))
                    {
                        var preparation = preparationTimeForRental.Single(p => p.BookingId == booking.Id && p.StartDate <= date.Date && p.StartDate.AddDays(rentalInfo.PreparationTimeInDays) > date.Date);
                        date.PreparationTimes.Add(new UnitViewModel { Unit = preparation.Unit });
                    }
                }

                result.Dates.Add(date);
            }

            return result;
        }
      public  bool CheckRentalExist(int rentalId)
        {
            return _rentals.ContainsKey(rentalId);
        }
    }
}
