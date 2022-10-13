﻿using System;
using System.Collections.Generic;
using System.Text;

namespace VacationRentalService.Models
{
    public class CalendarViewModel
    {
        public int RentalId { get; set; }
        public List<CalendarDateViewModel> Dates { get; set; }
    }
}
