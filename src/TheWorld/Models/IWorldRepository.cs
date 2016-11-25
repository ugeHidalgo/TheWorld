using System.Collections.Generic;
using System.Threading.Tasks;

namespace TheWorld.Models
{
    public interface IWorldRepository
    {
        Trip GetTripByName(string tripName);
        IEnumerable<Trip> GetAllTrips();
        void AddTrip(Trip trip);
        void AddStopTo(string tripName, Stop stop);
        Task<bool> SaveChangesAsync();

        
    }
}