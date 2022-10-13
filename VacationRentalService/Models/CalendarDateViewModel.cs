using System;
using System.Collections.Generic;
using System.Text;

namespace VacationRentalService.Models
{
    public class CalendarDateViewModel
    {
        public DateTime Date { get; set; }
        public List<CalendarBookingViewModel> Bookings { get; set; }
    }
}
