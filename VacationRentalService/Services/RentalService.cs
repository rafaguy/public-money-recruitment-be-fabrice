using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using VacationRentalService.Data;
using VacationRentalService.Models;

namespace VacationRentalService.Services
{

    public class RentalService : IRentalService
    {
        private readonly VacationRentalDbContext _context;
        public RentalService(VacationRentalDbContext context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
        }
        public async Task AddAsync(Rental rental)
        {
          await  _context.Rentals.AddAsync(rental);
           await _context.SaveChangesAsync();
        }
        public async Task<IEnumerable<Rental>> GetAllAsync()
        {
            return await _context.Rentals.ToListAsync();
        }

        public async Task UpdateAsync(Rental rental)
        {
            _context.Rentals.Update(rental);
          await  _context.Rentals.SingleAsync();
        }
      public  async Task<Rental> FindAsync(int rentalId)
        {
          return await _context.Rentals.FirstOrDefaultAsync(r => r.RentalId == rentalId);
        }

       public async Task<Rental> RemoveAsync(int rentalId)
        {
            var rentalToRemove =await _context.Rentals.FirstOrDefaultAsync(r => r.RentalId == rentalId);
            if(rentalToRemove==null)
            {
                return null;
            }
            _context.Rentals.Remove(rentalToRemove);
           await _context.SaveChangesAsync();
            return rentalToRemove;
        }
    }
}
