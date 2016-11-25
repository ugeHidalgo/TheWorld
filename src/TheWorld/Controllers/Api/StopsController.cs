using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TheWorld.Models;
using TheWorld.ViewModels;

namespace TheWorld.Controllers.Api
{
    [Route("/api/trips/{tripName}/stops")]
    public class StopsController : Controller
    {
        private ILogger _logger;
        private IWorldRepository _repository;

        public StopsController(IWorldRepository repository, ILogger<StopsController> logger)
        {
            _repository = repository;
            _logger = logger;
        }

        [HttpGet("")]
        public ActionResult Get(string tripName)
        {
            try
            {
                Trip trip = _repository.GetTripByName(tripName);
                return Ok(Mapper.Map<IEnumerable<Stop>>(trip.Stops.OrderBy(s => s.Order)));
            }
            catch (System.Exception ex)
            {
                _logger.LogError($"Failed to get stops for trip {tripName}: {ex}");
            }
            return BadRequest($"Failed to get stops for trip {tripName}.");
        }


        [HttpPost("")]
        public async Task<ActionResult> Post(string tripName, [FromBody]StopViewModel stop)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var newStop = Mapper.Map<Stop>(stop);
                    _repository.AddStopTo(tripName, newStop);
                    if (await _repository.SaveChangesAsync())
                    {
                        return Created($"api/trips/{tripName}/stops/{newStop.Name}", 
                            Mapper.Map<StopViewModel>(newStop));
                    }
                }
            }
            catch (System.Exception ex)
            {
                _logger.LogError($"Failed to save new stopo: {ex}");
            }
            return BadRequest($"Failed to post new stop.");
        }
    }
}
