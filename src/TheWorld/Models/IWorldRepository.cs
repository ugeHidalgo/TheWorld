using System.Collections.Generic;
using System.Threading.Tasks;

namespace TheWorld.Models
{
    public interface IWorldRepository
    {
        Trip GetTripByName(string tripName);
        Trip GetUserTripByName(string tripName, string username);

        IEnumerable<Trip> GetAllTrips();
        IEnumerable<Trip> GetTripsByUserName(string username);

        void AddTrip(Trip trip);
        void AddStopTo(string tripName, Stop stop, string username);
        Task<bool> SaveChangesAsync();                
    }
}