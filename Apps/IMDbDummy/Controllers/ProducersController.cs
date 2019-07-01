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
    public class ProducersController: Controller
    {
        private readonly ILogger<ProducersController> _logger;
        private readonly IIMDBRepository _repository;
        private readonly IMapper _mapper;

        public ProducersController(ILogger<ProducersController> logger, IIMDBRepository repository, IMapper mapper)
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
              
                var result = _repository.GetAllProducers();
                return Ok(_mapper.Map<IEnumerable<Producer>,  IEnumerable<ProducerViewModel >> (result));
                //return Ok(result);

            }
            catch (Exception ex)
            {

                _logger.LogError($"Failed to fetch producers: {ex}");
                return BadRequest("Failed to fetch producers");
            }
        }
        [HttpPost]
        public IActionResult Post([FromBody] ProducerViewModel producer)
        {

            try
            {
                if (ModelState.IsValid)
                {
                    var newProducer = _mapper.Map<ProducerViewModel, Producer>(producer);
                    if (!_repository.IsProducerExist(newProducer))
                    {
                        var result = _repository.AddProducer(newProducer);
                        return Ok(_mapper.Map<Producer, ProducerViewModel>(result));

                    }
                    else
                    {
                        return BadRequest("Producer Already Exist");
                    }

                }
                else
                {
                    return BadRequest(ModelState);
                }
                
                //return Ok(result);

            }
            catch (Exception ex)
            {

                _logger.LogError($"Failed to add producer to database: {ex}");
                return BadRequest("Failed to add producer to databse");
            }
        }
    }
}
