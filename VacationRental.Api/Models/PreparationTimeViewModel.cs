using System;

namespace VacationRental.Api.Models
{
    public class PreparationTimeViewModel
    {
        public int BookingId { get; set; }
        public DateTime StartDate { get; set; }
        
        public int Unit { get; set; }
    }
}
