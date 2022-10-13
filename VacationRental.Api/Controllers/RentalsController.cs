using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using VacationRental.Api.Models;
using VacationRental.Api.Services;

namespace VacationRental.Api.Controllers
{
    [Route("api/v1/rentals")]
    [ApiController]
    public class RentalsController : ControllerBase
    {
        private readonly IRentalService _rentalService;

        public RentalsController(IRentalService service)
        {
            _rentalService = service;
        }

        [HttpGet]
        [Route("{rentalId:int}")]
        public RentalViewModel Get(int rentalId)
        {
            var rental = _rentalService.GetRental(rentalId);
            if (rental==null)
                throw new ApplicationException("Rental not found");

            return rental;
        }

        [HttpPost]
        public  ResourceIdViewModel Post(RentalBindingModel model)
        {
            var key = new ResourceIdViewModel { Id = _rentalService.GetRentalCount() + 1 };

            _rentalService.Add(key.Id, model.Units, model.PreparationTimeInDays);
            return key;
        }
        
    }
}
