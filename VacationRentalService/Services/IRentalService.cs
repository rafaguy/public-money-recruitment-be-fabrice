using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using VacationRentalService.Models;

namespace VacationRentalService.Services
{
    public interface IRentalService
    {
        Task<IEnumerable<Rental>> GetAllAsync();
        Task AddAsync(Rental rental);
        Task UpdateAsync(Rental rental );
        Task<Rental> FindAsync(int rentalId);
        Task<Rental> RemoveAsync(int rentalId);

    }
}
