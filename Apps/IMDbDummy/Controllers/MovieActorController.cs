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
    [Route("/api/movies/{id}/actors")]

    public class MovieActorController: Controller
    {
        private readonly IIMDBRepository _repository;
        private readonly ILogger<MovieActorController> _logger;
        private readonly IMapper _mapper;

        public MovieActorController(IIMDBRepository repository, ILogger<MovieActorController> logger, IMapper mapper)
        {
            _repository = repository;
            _logger = logger;
            _mapper = mapper;

        }
        [HttpGet]
        public IActionResult Get(int id)
        {
            var order = _repository.GetMovieById(id);
            if (order != null)
                return Ok(_mapper.Map<IEnumerable<MovieActor>, IEnumerable<MovieActorViewModel>>(order.MovieActors));
            else return NotFound();
        }
    }
}
