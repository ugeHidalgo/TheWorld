using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TheWorld.Models;
using TheWorld.Services;
using TheWorld.ViewModels;

namespace TheWorld.Controllers.Api
{
    [Authorize]
    //[Route("/api/trips/{tripName}/stops")]
    public class StopsController : Controller
    {
        private GeoCoordsService _coordsService;
        private ILogger _logger;
        private IWorldRepository _repository;

        public StopsController(IWorldRepository repository,
            ILogger<StopsController> logger,
            GeoCoordsService coordsService)
        {
            _repository = repository;
            _logger = logger;
            _coordsService = coordsService;
        }

        [HttpGet("/api/trips/{tripName}/stops")]
        public ActionResult Get(string tripName)
        {
            try
            {
                Trip trip = _repository.GetUserTripByName(tripName, User.Identity.Name);
                return Ok(Mapper.Map<IEnumerable<Stop>>(trip.Stops.OrderBy(s => s.Order)));
            }
            catch (System.Exception ex)
            {
                _logger.LogError($"Failed to get stops for trip {tripName}: {ex}");
            }
            return BadRequest($"Failed to get stops for trip {tripName}.");
        }

        [HttpPost("/api/trips/{tripName}/stops")]
        public async Task<ActionResult> Post(string tripName, [FromBody]StopViewModel stop)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var newStop = Mapper.Map<Stop>(stop);

                    var result = await _coordsService.GetCoordAsync(newStop.Name);
                    if (!result.Success)
                    {
                        _logger.LogError(result.Message);
                    }
                    else
                    {
                        newStop.Latitude = result.Latitude;
                        newStop.Longitude = result.Longitude;

                        _repository.AddStopTo(tripName, newStop, User.Identity.Name);

                        if (await _repository.SaveChangesAsync())
                        {
                            return Created($"api/trips/{tripName}/stops/{newStop.Name}",
                                Mapper.Map<StopViewModel>(newStop));
                        }
                    }
                }
            }
            catch (System.Exception ex)
            {
                _logger.LogError($"Failed to save new stop: {ex}");
            }
            return BadRequest($"Failed to post new stop.");
        }

        [HttpDelete("/api/trips/{tripName}/{stopName}")]
        public async Task<ActionResult> RemoveStop(string tripName, string stopName)
        {
            try
            {
                bool result = _repository.RemoveStop(tripName, stopName, User.Identity.Name);
                if (result)
                {
                    if (await _repository.SaveChangesAsync())
                    {
                        return Ok();
                    }
                }               
            }
            catch (System.Exception ex)
            {
                _logger.LogError($"Failed to remove stop {stopName} for trip {tripName}: {ex}");
            }
            return BadRequest($"Failed to remove stop {stopName} for trip {tripName}.");
        }
    }
}
