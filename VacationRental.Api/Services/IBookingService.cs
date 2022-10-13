using System;
using VacationRental.Api.Models;

namespace VacationRental.Api.Services
{
    public interface IBookingService
    {
        BookingViewModel GetBooking(int bookingId);

        void AddBooking(int id,int rentalId, DateTime start, int nights,int availableUnit);
        bool CheckUnitAvailability(int rentalId, DateTime start, int nights,out int availableUnit);
        int GetBookingCount();
        bool CheckRentalExist(int rentalId);
    }
}
