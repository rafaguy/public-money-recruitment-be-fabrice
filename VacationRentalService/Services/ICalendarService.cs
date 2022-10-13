using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using VacationRentalService.Models;

namespace VacationRentalService.Services
{
    public interface ICalendarService
    {
        Task<CalendarViewModel> GetCalendar(int rentalId, DateTime start, int nights);
    }
}
