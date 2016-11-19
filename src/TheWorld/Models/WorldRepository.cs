using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TheWorld.Models
{
    public class WorldRepository : IWorldRepository
    {
        private WorldContext _context;

        public WorldRepository(WorldContext context)
        {
            _context = context;
        }

        public void AddTrip(Trip trip)
        {
            trip.DateCreated = DateTime.Now;
            _context.Add(trip);
        }

        public Trip GetTripByName(string tripName)
        {
            return _context.Trips
                .Include(t=>t.Stops)
                .Where(x => x.Name == tripName)
                .FirstOrDefault();
        }

        public IEnumerable<Trip> GetAllTrips()
        {
            return _context.Trips.ToList();
        }        

        public async Task<bool> SaveChangesAsync()
        {
            return (await _context.SaveChangesAsync()) > 0;
        }        
    }
}
