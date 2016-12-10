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

        public IEnumerable<Trip> GetTripsByUserName(string username) {
            return _context.Trips.Where(x => x.UserName == username).ToList();
        }

        public void AddStopTo(string tripName, Stop newStop, string username)
        {
            var trip = GetUserTripByName(tripName, username);
            if ( trip!=null )
            {
                trip.Stops.Add(newStop);
                _context.Stops.Add(newStop);                
            }
        }

        public IEnumerable<Trip> GetAllTrips()
        {
            return _context.Trips.ToList();
        }        

        public async Task<bool> SaveChangesAsync()
        {
            return (await _context.SaveChangesAsync()) > 0;
        }

        public Trip GetUserTripByName(string tripName, string username)
        {
            return _context.Trips
                .Include(t => t.Stops)
                .Where(x => x.Name == tripName && x.UserName == username)
                .FirstOrDefault();
        }        

        public bool RemoveStop(string tripName, string stopName, string username)
        {
            var stops = GetUserTripByName(tripName, username).Stops;
            var stopToRemove = stops.FirstOrDefault(x => x.Name == stopName);
            if (stopToRemove == null) return false;
            return stops.Remove(stopToRemove);
        }
    }
}
