using System;
using System.Collections.Generic;
using System.Linq;

namespace TheWorld.Models
{
    public class WorldRepository : IWorldRepository
    {
        private WorldContext _context;

        public WorldRepository(WorldContext context)
        {
            _context = context;
        }

        public Trip AddNewTrip(Trip trip)
        {
            trip.DateCreated = DateTime.Now;
            _context.Trips.Add(trip);
            _context.SaveChanges();
            return _context.Trips.FirstOrDefault(x => x.Id == trip.Id);            
        }

        public IEnumerable<Trip> GetAllTrips()
        {
            return _context.Trips.ToList();
        }
    }
}
