using System;
using System.Collections.Generic;
using System.Text;

namespace VacationRentalService.Models
{
    public class Booking
    {
        public int BookingId { get; set; }
        public int RentalId { get; set; }
        public Rental Rental { get; set; }
        public DateTime Start { get; set; }
        public int Nights { get; set; }
    }
}
