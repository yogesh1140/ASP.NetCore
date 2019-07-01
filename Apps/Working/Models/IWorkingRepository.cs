using System.Collections.Generic;
using System.Threading.Tasks;
using Working.ViewModels;

namespace Working.Models
{
    public interface IWorkingRepository
    {
        IEnumerable<Trip> GetAllTrips();
        Trip GetTripByName(string tripName);
        void AddTrip(Trip trip);
        void AddStop(string tripName, Stop newStop);

        Task<bool> SaveChangesAsync();
        IEnumerable<Trip> GetTripsByUserName(string username);
    }
}