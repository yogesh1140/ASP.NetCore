using AutoMapper;
using IMDBDummy.Data;
using IMDBDummy.Data.Entities;
using IMDBDummy.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IMDBDummy.Controllers
{
    [Route("api/[Controller]")]
    public class ActorsController : Controller
    {
        private readonly ILogger<ActorsController> _logger;
        private readonly IIMDBRepository _repository;
        private readonly IMapper _mapper;

        public ActorsController(ILogger<ActorsController> logger, IIMDBRepository repository, IMapper mapper)
        {
            _logger = logger;
            _repository = repository;
            _mapper = mapper;

        }
        [HttpGet]
        public IActionResult Get()
        {
            try
            {

                var result = _repository.GetAllActors();
                return Ok(_mapper.Map<IEnumerable<Actor>, IEnumerable<ActorViewModel>>(result));
                //return Ok(result);

            }
            catch (Exception ex)
            {

                _logger.LogError($"Failed to fetch producers: {ex}");
                return BadRequest("Failed to fetch producers");
            }
        }
        [HttpPost]
        public IActionResult Post([FromBody] ActorViewModel actor)
        {

            try
            {
                if (ModelState.IsValid)
                {
                    var newActor = _mapper.Map<ActorViewModel, Actor>(actor);
                    if (!_repository.IsActorExist(newActor))
                    {
                        var result = _repository.AddActor(newActor);
                        return Ok(_mapper.Map<Actor, ActorViewModel>(result));
                    }
                    else return BadRequest("Actor already exist!");


                }
                else return BadRequest(ModelState);
                
                //return Ok(result);

            }
            catch (Exception ex)
            {

                _logger.LogError($"Failed to add actor to database: {ex}");
                return BadRequest("Failed to add actor to databse");
            }
        }
    }
}
