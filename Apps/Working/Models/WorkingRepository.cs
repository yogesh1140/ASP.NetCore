using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Working.ViewModels;

namespace Working.Models
{
    public class WorkingRepository : IWorkingRepository
    {
        public WorkingContext _context { get; set; }

        private ILogger<WorkingRepository> _logger;

        public WorkingRepository(WorkingContext context,ILogger<WorkingRepository> logger)
        {
            _context = context;
            _logger = logger;
        }
        public IEnumerable<Trip> GetAllTrips()
        {
            _logger.LogInformation("Getting All Trips from the Database");
            return _context.Trips.ToList();
        }
        public void AddTrip(Trip trip)
        {
            _context.Add(trip);
        }
        public async Task<bool> SaveChangesAsync()
        {
            return (await _context.SaveChangesAsync()) > 0;
        }

        public Trip GetTripByName(string tripName)
        {
           return _context.Trips
                .Include(t=>t.Stops)
                .Where(t => t.Name == tripName)
                .FirstOrDefault();
        }

        public void AddStop(string tripName, Stop newStop)
        {
            var trip = GetTripByName(tripName);
            if (trip!=null)
            {
                trip.Stops.Add(newStop);
                _context.Stops.Add(newStop);
            }
        }

        public IEnumerable<Trip>GetTripsByUserName(string username) { 

        
            return _context.Trips
           .Include(t => t.Stops)
           .Where(t => t.UserName == username)
           .ToList();
        }
    }
    
}
