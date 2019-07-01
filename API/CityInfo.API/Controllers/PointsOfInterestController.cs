using AutoMapper;
using CityInfo.API.Models;
using CityInfo.API.Services;
using FluentValidation.Results;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CityInfo.API.Controllers
{
    [Route("api/cities")]
    public class PointsOfInterestController : Controller
    {
        private ILogger<PointsOfInterestController> _logger { get; set; }
        private ICityInfoRepository _repository { get; set; }
        private IMailService _mailService;

        public PointsOfInterestController(ILogger<PointsOfInterestController> logger, IMailService mailService, ICityInfoRepository repository)
        {
            _logger = logger;
            _mailService = mailService;
            _repository = repository;
        }
        [HttpGet("{cityId}/pointsofinterest")]
        public IActionResult GetPointsOfInterest(int cityId)
        {

            try
            {
                var cityExist = _repository.CityExists(cityId);
                if (!cityExist)
                {
                    _logger.LogInformation($"City with id {cityId} wasn't found when accessing points of interest.");
                    return NotFound();

                }
                var pointsOfInterest = _repository.GetPointsOfInterestForCity(cityId);

                var result = Mapper.Map<IEnumerable<PointOfInterestDto>>(pointsOfInterest);


                return Ok(result);
            }
            catch (Exception ex)
            {
                
                    _logger.LogCritical($"Exception while getting points of interest for city with id{cityId}", ex);
                return StatusCode(500, "A problem happened while handling you request");
            }
        }
        [HttpGet("{cityId}/pointsofinterest/{id}", Name = "GetPointOfInterest")]
        public IActionResult GetPointOfInterest(int cityId, int id)
        {
            var cityExist = _repository.CityExists(cityId);
            if (!cityExist)
            {
                _logger.LogInformation($"City with id {cityId} wasn't found when accessing points of interest.");
                return NotFound();

            }
            var pointOfInterest = _repository.GetPointOfInterestForCity(cityId, id);
            if (pointOfInterest == null)
            {
                _logger.LogInformation($"City with id {cityId} wasn't found when accessing points of interest.");
                return NotFound();

            }
            var result = Mapper.Map<PointOfInterestDto>(pointOfInterest);
            return Ok(result);
        }
        [HttpPost("{cityId}/pointsofinterest")]
        public IActionResult CreatePointOfInterest(int cityId, [FromBody] PointOfInterestForCreationDto pointOfInterest)
        {

            var pointOfInterestValidator = new PointOfInterestForCreationValidator();
            ValidationResult validationResult = pointOfInterestValidator.Validate(pointOfInterest);

            if (!validationResult.IsValid)
                return BadRequest(validationResult.Errors.Select(vr => new { propertyName = vr.PropertyName, errorMessage = vr.ErrorMessage }));

            var cityExist = _repository.CityExists(cityId);
            if (!cityExist)
            {
                _logger.LogInformation($"City with id {cityId} wasn't found when accessing points of interest.");
                return NotFound();

            }

            var finalPointOfInterest = Mapper.Map<Entities.PointOfInterest>(pointOfInterest);
            _repository.AddPointOfInterestForCity(cityId, finalPointOfInterest);
            if (!_repository.Save())
                return StatusCode(500, "A problem happened while handling you request");

            var createdPointOfInteretToReturn = Mapper.Map<PointOfInterestDto>(finalPointOfInterest);
            return CreatedAtRoute("GetPointOfInterest", new { cityId = cityId, id = createdPointOfInteretToReturn.Id }, createdPointOfInteretToReturn);


        }
        [HttpPut("{cityId}/pointsofinterest/{id}")]
        public IActionResult UpdatePointOfInterest(int cityId, int id, [FromBody] PointOfInterestForUpdateDto pointOfInterest)
        {
            if (pointOfInterest == null) return BadRequest();
            else if (!ModelState.IsValid) return BadRequest(ModelState);

            else if (pointOfInterest.Name == pointOfInterest.Description)
            {
                ModelState.AddModelError("Description", "The provide description should be different from the name");
                return BadRequest(ModelState);
            }
            var cityExist = _repository.CityExists(cityId);
            if (!cityExist)
            {
                _logger.LogInformation($"City with id {cityId} wasn't found when accessing points of interest.");
                return NotFound();

            }

            var pointOfInterestEntity = _repository.GetPointOfInterestForCity(cityId, id);
            if (pointOfInterestEntity == null)
                return NotFound();


            // Updated fields with mapping created
            Mapper.Map(pointOfInterest, pointOfInterestEntity);
            if (!_repository.Save())
                return StatusCode(500, "A problem happened while handling you request");

            return NoContent();

        }

        [HttpPatch("{cityId}/pointsofinterest/{id}")]
        public IActionResult PartiallyUpdatePointOfInterest(int cityId, int id, [FromBody] JsonPatchDocument<PointOfInterestForUpdateDto> patchDoc)
        {
            if (patchDoc == null)
                return BadRequest();
            var cityExist = _repository.CityExists(cityId);
            if (!cityExist)
            {
                _logger.LogInformation($"City with id {cityId} wasn't found when accessing points of interest.");
                return NotFound();

            }

            var pointOfInterestEntity = _repository.GetPointOfInterestForCity(cityId, id);
            if (pointOfInterestEntity == null)
                return NotFound();

            var pointOfInterestToPatch = Mapper.Map<PointOfInterestForUpdateDto>(pointOfInterestEntity);

            patchDoc.ApplyTo(pointOfInterestToPatch, ModelState);

            TryValidateModel(pointOfInterestToPatch);

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            Mapper.Map(pointOfInterestToPatch, pointOfInterestEntity);
            if (!_repository.Save())
                return StatusCode(500, "A problem happened while handling you request");

            return NoContent();

        }
        [HttpDelete("{cityId}/pointsofinterest/{id}")]
        public IActionResult DeletePointOfInterest(int cityId, int id)
        {

            var cityExist = _repository.CityExists(cityId);
            if (!cityExist)
            {
                _logger.LogInformation($"City with id {cityId} wasn't found when accessing points of interest.");
                return NotFound();

            }
            var pointOfInterestEntity = _repository.GetPointOfInterestForCity(cityId, id);
            if (pointOfInterestEntity == null)
                return NotFound();
            _repository.DeletePointOfInterest(pointOfInterestEntity);

            if (!_repository.Save())
                return StatusCode(500, "A problem happened while handling you request");

            _mailService.Send("Point of interest deleted.", $"Point if interest {pointOfInterestEntity.Name} with id {pointOfInterestEntity.Id} was deleted.");
            return NoContent();
        }
    }
}
