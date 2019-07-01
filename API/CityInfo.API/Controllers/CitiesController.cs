﻿using AutoMapper;
using CityInfo.API.Models;
using CityInfo.API.Services;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CityInfo.API.Controllers
{
    [Route("api/cities")]
    public class CitiesController : Controller
    {
        private ICityInfoRepository _repository { get; set; }
        public CitiesController(ICityInfoRepository repository)
        {
            _repository = repository;
        }
        [HttpGet]
        public IActionResult GetCities()
        {
            var cityEntities = _repository.GetCities();
            var results = Mapper.Map<IEnumerable<CityWithoutPointsOfInterestDto>>(cityEntities);

            return Ok(results);
        }
        [HttpGet("{id}")]
        public IActionResult GetCity(int id, [FromQuery] bool includePointsOfInterest = false)
        {
            var city = _repository.GetCity(id, includePointsOfInterest);
            if (city == null)
                return NotFound();

            if (!includePointsOfInterest)
            {
                var result = Mapper.Map<CityWithoutPointsOfInterestDto>(city);
                return Ok(result);
            }
            else
            {
                var results = Mapper.Map<CityDto>(city);
                return Ok(results);
            }


        }
    }
}
