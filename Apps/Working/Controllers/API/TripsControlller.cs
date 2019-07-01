using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Working.Models;
using Working.ViewModels;

namespace Working.Controllers.API
{
    [Route("api/trips")]
    [Authorize]
    public class TripsControlller: Controller
    {
        private IWorkingRepository _repository;
        private ILogger<TripsControlller> _logger;

        public TripsControlller(IWorkingRepository repository, ILogger<TripsControlller> logger)
        {
            _repository = repository;
            _logger = logger;
        }
        [HttpGet("")]
        public IActionResult Get()
        {
            try
            {
                var result = _repository.GetTripsByUserName(this.User.Identity.Name);
                return Ok(Mapper.Map<IEnumerable<TripViewModel>>(result));
            }
            catch (Exception ex)
            {
                _logger.LogError($"Failed to get ll trips: {ex}");
                return BadRequest("Error occured");
            }
            //if (true) return BadRequest("something went wrong");
            
        }
        [HttpPost("")]
        public async Task<IActionResult> Post([FromBody]TripViewModel trip)
        {
            if (ModelState.IsValid)
            {
                Trip newTrip = Mapper.Map<Trip>(trip);
                newTrip.UserName = this.User.Identity.Name;
                _repository.AddTrip(newTrip);
                if(await _repository.SaveChangesAsync())
                {
                    return Created($"api/trips/{trip.Name}", Mapper.Map<TripViewModel>(newTrip));
                }
                //else
                //{
                //    return BadRequest("Failed to save changes to database");
                //}                
            }
            return BadRequest("Failed to save trip");
        }
    }
}
