using System;
using VacationRental.Api.Models;

namespace VacationRental.Api.Services
{
    public interface ICalendarService
    {
        CalendarViewModel RetrieveBookingInfo(int rentalId, DateTime start, int nights);
        bool CheckRentalExist(int rentalId);
    }
}
