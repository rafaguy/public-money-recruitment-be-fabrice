using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using VacationRentalService.Data;
using VacationRentalService.Models;

namespace VacationRentalService.Services
{
    public class BookingService : IBookingService
    {
        private readonly VacationRentalDbContext _context;
        public BookingService(VacationRentalDbContext context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
        }
        public async Task AddAsync(Booking booking)
        {
          await  _context.Bookings.AddAsync(booking);
            await _context.SaveChangesAsync();
        }

        public async Task<Booking> FindAsync(int bookingId)
        {
            var booking =await _context.Bookings.FirstOrDefaultAsync(b => b.BookingId == bookingId);
            return booking;
        }

        public async Task<IEnumerable<Booking>> GetAllAsync()
        {
            return await _context.Bookings.ToListAsync();
        }

        public async Task<Booking> RemoveAsync(int bookingId)
        {
            var bookingToRemove =await _context.Bookings.FirstOrDefaultAsync(b => b.BookingId == bookingId);
            if(bookingToRemove==null)
            {
                return null;
            }
            _context.Bookings.Remove(bookingToRemove);
           await _context.SaveChangesAsync();
            return bookingToRemove;
        }

        public async Task UpdateAsync(Booking booking)
        {
            _context.Bookings.Update(booking);
            await _context.SaveChangesAsync();
        }
    }
}
