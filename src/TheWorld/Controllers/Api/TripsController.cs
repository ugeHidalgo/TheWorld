using Microsoft.AspNetCore.Mvc;
using TheWorld.Models;

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
            var data = _repository.GetAllTrips();
            return Ok(data);
        }

        [HttpPost("")]
        public ActionResult Post([FromBody]Trip trip)
        {
            return Ok(_repository.AddNewTrip(trip));
        }
    }

}
