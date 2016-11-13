using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using TheWorld.Models;
using TheWorld.ViewModels;

namespace TheWorld.Controllers.Api
{
    [Route("api/trips")]
    public class TripsController : Controller
    {
        private IWorldRepository _repository;

        public TripsController(IWorldRepository repository)
        {
            _repository = repository;
        }

        [HttpGet("")]
        public ActionResult Get()
        {
            try
            {
                IEnumerable<Trip> data = _repository.GetAllTrips();
                return Ok(Mapper.Map<IEnumerable<TripViewModel>>(data));
            }
            catch (Exception ex)
            {
                return BadRequest("An error occurred while getting trips.");
            }
        }

        [HttpPost("")]
        public ActionResult Post([FromBody]TripViewModel theTrip)
        {
            if (ModelState.IsValid)
            {
                Trip trip = Mapper.Map<Trip>(theTrip);

                Trip result = _repository.AddNewTrip(trip);

                TripViewModel tripViewModel = Mapper.Map<TripViewModel>(trip);
                return Created($"api/trips/{theTrip.Name}", tripViewModel);
            }
            return BadRequest(ModelState);
        }
    }

}
