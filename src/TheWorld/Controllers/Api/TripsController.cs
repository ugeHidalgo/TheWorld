using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using TheWorld.Models;
using TheWorld.ViewModels;

namespace TheWorld.Controllers.Api
{
    [Authorize]
    [Route("api/trips")]
    public class TripsController : Controller
    {
        private ILogger<TripsController> _logger;
        private IWorldRepository _repository;

        public TripsController(IWorldRepository repository, ILogger<TripsController> logger)
        {
            _repository = repository;
            _logger = logger;
        }

        [HttpGet("")]
        public ActionResult Get()
        {
            try
            {
                IEnumerable<Trip> data = _repository.GetTripsByUserName(this.User.Identity.Name);
                return Ok(Mapper.Map<IEnumerable<TripViewModel>>(data));
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error when getting all trips: {ex}", ex);
                return BadRequest("An error occurred while getting trips.");
            }            
        }

        [HttpPost("")]
        public async Task<ActionResult> Post([FromBody]TripViewModel theTrip)
        {
            if (ModelState.IsValid)
            {
                Trip newtrip = Mapper.Map<Trip>(theTrip);
                newtrip.UserName = this.User.Identity.Name;

                _repository.AddTrip(newtrip);                

                if (await _repository.SaveChangesAsync())
                {
                    return Created($"api/trips/{theTrip.Name}", Mapper.Map<TripViewModel>(theTrip));
                }                
            }
            return BadRequest("Failed to save changes to database");
        }
    }

}
