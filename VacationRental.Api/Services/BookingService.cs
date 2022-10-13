using System;
using System.Collections.Generic;
using System.Linq;
using VacationRental.Api.Models;

namespace VacationRental.Api.Services
{
    public class BookingService : IBookingService
    {
        private readonly IDictionary<int, RentalViewModel> _rentals;
        private readonly IDictionary<int, BookingViewModel> _bookings;
        private readonly IDictionary<int,List<PreparationTimeViewModel>> _preparationTime;

        public BookingService(IDictionary<int, RentalViewModel> rentals, IDictionary<int, BookingViewModel> bookings,
           IDictionary<int, List<PreparationTimeViewModel>> preparationTime)
        {
            _rentals = rentals;
            _bookings = bookings;
            _preparationTime = preparationTime;
        }
        public void AddBooking(int id,int rentalId, DateTime start, int nights, int availableUnit)
        {
          
            _bookings.Add(id, new BookingViewModel
            {
                Id = id,
                Nights = nights,
                RentalId = rentalId,
                Start = start,
                Unit=availableUnit
            });
          
            if(_preparationTime.ContainsKey(rentalId))
            {
                _preparationTime[rentalId].Add(new PreparationTimeViewModel
                {
                    BookingId = id,
                    StartDate = start.AddDays(nights),
                    Unit = availableUnit
                });
            }
            else
            {
                _preparationTime.Add(rentalId, new List<PreparationTimeViewModel>());
                _preparationTime[rentalId].Add(new PreparationTimeViewModel
                {
                    BookingId = id,
                    StartDate = start.AddDays(nights),
                    Unit = availableUnit
                });
            }
            
        }

        public bool CheckUnitAvailability(int rentalId, DateTime start, int nights,out int availableUnit)
        {
            var bookingForRental = _bookings.Values.Where(b => b.RentalId == rentalId).ToList();
            var preparationTimeForRental =_preparationTime.ContainsKey(rentalId)? _preparationTime[rentalId]: new List<PreparationTimeViewModel>();
            var checkoutDate = start.AddDays(nights);
            var rentalInfo = _rentals[rentalId];
            int count = 0;
            List<int> avUnit = Enumerable.Range(1, rentalInfo.Units).ToList();
            List<int> occupiedUnit = new List<int>();

            foreach (var booking in bookingForRental)
            {
                if (!(booking.Checkout <= start || checkoutDate < booking.Start) && !occupiedUnit.Contains(booking.Unit))
                {
                    occupiedUnit.Add(booking.Unit);
                    count++;
                }
                else if(preparationTimeForRental.Any(p=>(p.StartDate<=start)&&(start<p.StartDate.AddDays(rentalInfo.PreparationTimeInDays))&&p.BookingId==booking.Id ) && !occupiedUnit.Contains(booking.Unit))
                {
                    occupiedUnit.Add(booking.Unit);
                    count++;
                }
            }
            availableUnit = avUnit.Except(occupiedUnit).FirstOrDefault();
            return count < _rentals[rentalId].Units;
        }

        public BookingViewModel GetBooking(int bookingId)
        {
            if(!_bookings.ContainsKey(bookingId))
            {
                return null;
            }
            return _bookings[bookingId];
        }
       public int GetBookingCount()
        {
            return _bookings.Values.Count;
        }
       public bool CheckRentalExist(int rentalId)
        {
            return _rentals.ContainsKey(rentalId);
        }
    }
}
