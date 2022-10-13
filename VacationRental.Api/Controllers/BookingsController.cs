using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using VacationRental.Api.Models;
using VacationRental.Api.Services;

namespace VacationRental.Api.Controllers
{
    [Route("api/v1/bookings")]
    [ApiController]
    public class BookingsController : ControllerBase
    {
        private readonly IBookingService _bookingService;
      
        public BookingsController(
            IBookingService bookingService,IRentalService rentalService)
        {
            _bookingService = bookingService;
           
        }

        [HttpGet]
        [Route("{bookingId:int}")]
        public BookingViewModel  Get(int bookingId)
        {
            var booking = _bookingService.GetBooking(bookingId);
            if(booking==null)
            {
                throw new ApplicationException("Booking not found");
            }
            return booking;
        }

        [HttpPost]
        public ActionResult< ResourceIdViewModel> Post(BookingBindingModel model)
        {
            if (model.Nights <= 0)
                throw new ApplicationException("Nigts must be positive");
            if (!_bookingService.CheckRentalExist(model.RentalId))
                throw new ApplicationException("Rental not found");

            var unitAvailability = _bookingService.CheckUnitAvailability(model.RentalId, model.Start, model.Nights,out int availableUnit);

            if(!unitAvailability)
            {
                throw new ApplicationException("Not available");
            }

            var key = new ResourceIdViewModel { Id =_bookingService.GetBookingCount() + 1 };

            _bookingService.AddBooking(key.Id, model.RentalId, model.Start, model.Nights,availableUnit);
            return key;
        }
    }
}
