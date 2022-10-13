using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using VacationRentalService.Models;

namespace VacationRentalService.Data
{
    public class VacationRentalDbContext : DbContext
    {
        public VacationRentalDbContext(DbContextOptions<VacationRentalDbContext> options) : base(options)
        {

        }

        public DbSet<Booking> Bookings { get; set; }
        public DbSet<Rental> Rentals { get; set; }
    }
}
