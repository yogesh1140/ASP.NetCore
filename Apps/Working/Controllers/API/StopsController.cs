﻿using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Working.Models;
using Working.Services;
using Working.ViewModels;

namespace Working.Controllers.API
{
    [Route("api/trips/{tripName}/stops")]
    public class StopsController : Controller
    {
        private IWorkingRepository _repository;
        private ILogger<StopsController> _logger;
        private GeoCoordsService _coordService;

        public StopsController(IWorkingRepository repository, ILogger<StopsController> logger, GeoCoordsService coordService)
        {
            _repository = repository;
            _logger = logger;
            _coordService = coordService;
        }
        [HttpGet("")]
        public IActionResult Get(string tripName)
        {
            try
            {
                var trip = _repository.GetTripByName(tripName);
                return Ok(Mapper.Map<IEnumerable< StopViewModel>> (trip.Stops.OrderBy(s => s.Order).ToList()));
            }
            catch (Exception ex)
            {

                _logger.LogError("Failed to get stops : {0}", ex);
            }
            return BadRequest("Failed to get stops");

        }
        [HttpPost("")]
        public async Task<IActionResult> Post(string tripName, [FromBody]StopViewModel vm)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var newStop = Mapper.Map<Stop>(vm);

                    var result = await _coordService.GetCoordsAsync(newStop.Name);

                    if (!result.Success)
                    {
                        _logger.LogError(result.Message);
                    }
                    else
                    {
                        newStop.Latitude = result.Latitude;
                        newStop.Longitude = result.Longitude;
                        _repository.AddStop(tripName, newStop);

                        if (await _repository.SaveChangesAsync())
                        {
                            return Created($"/api/trips/{tripName}/stops/{newStop.Name}", Mapper.Map<StopViewModel>(newStop));
                        }
                    }


                   
                 
                }
            }
            catch (Exception ex)
            {
                _logger.LogError("Failed to save new Stop: {0}", ex);
            }
            return BadRequest("Failed to save new stop");
        }
    }
}
