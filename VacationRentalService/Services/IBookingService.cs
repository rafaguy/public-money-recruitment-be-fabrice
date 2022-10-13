using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using VacationRentalService.Models;

namespace VacationRentalService.Services
{
    public interface IBookingService
    {
        Task<IEnumerable<Booking>> GetAllAsync();
        Task AddAsync(Booking booking);
        Task UpdateAsync(Booking booking);
        Task<Booking> FindAsync(int bookingId);
        Task<Booking> RemoveAsync(int bookingId);
    }
}
