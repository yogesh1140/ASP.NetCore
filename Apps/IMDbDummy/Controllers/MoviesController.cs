using AutoMapper;
using IMDBDummy.Data;
using IMDBDummy.Data.Entities;
using IMDBDummy.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.IO;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;

namespace IMDBDummy.Controllers
{
    [Route("api/[Controller]")]
    public class MoviesController : Controller
    {
        private readonly ILogger<MoviesController> _logger;
        private readonly IIMDBRepository _repository;
        private readonly IMapper _mapper;
        private readonly IHostingEnvironment _env;

        public MoviesController(ILogger<MoviesController> logger, IIMDBRepository repository, IMapper mapper, IHostingEnvironment env)
        {
            _logger = logger;
            _repository = repository;
            _mapper = mapper;
            _env = env;

        }
        [HttpGet]
        public IActionResult Get()
        {
            try
            {
                var result = _repository.GetAllMovies();
                return Ok(_mapper.Map<IEnumerable<Movie>, IEnumerable<MovieViewModel>>(result));
                //return Ok(result);


            }
            catch (Exception ex)
            {

                _logger.LogError($"Failed to retrive all movies: {ex}");
                return BadRequest("Failed to retrive all movies");
            }

        }

        [HttpGet("{id:int}")]
        public IActionResult Get(int id)
        {
            try
            {
                var result = _repository.GetMovieById(id);
                //return Ok(_mapper.Map<Movie, MovieViewModel>(result));
                if(result!=null)
                return Ok(_mapper.Map<Movie, MovieViewModel>(result));
                else
                    return NotFound("Movie does not exist");
                //return Ok(result);

            }
            catch (Exception ex)
            {

                _logger.LogError($"Failed to get Movie: {ex}");
                return BadRequest("Failed to get Movie");
            }

        }
        [HttpPost]
        public IActionResult Post([FromBody] MovieViewModel movie)
        {

            try
            {
                if (ModelState.IsValid)
                {
                    var newMovie = _mapper.Map<MovieViewModel, Movie>(movie);
                    if (movie.PosterUrl.StartsWith("/upload"))
                    {

                        string webRootPath = _env.WebRootPath;
                        string oldPath = webRootPath + movie.PosterUrl;
                        if (System.IO.File.Exists(oldPath))
                        {
                            string ext = oldPath.Substring(oldPath.LastIndexOf('.'));
                            string newPath = Path.Combine(webRootPath, "images", "movies") + "/" + movie.Name + ext;
                            System.IO.File.Move(oldPath, newPath);
                            newMovie.PosterUrl = "/images/movies/" + movie.Name + ext;
                        }

                    }
                    var id = _repository.AddMovie(newMovie);
                    if (id != 0)
                    {
                        var result = _repository.GetMovieById(id);
                        return Created($"api/movies/{id}", _mapper.Map<Movie, MovieViewModel>(result));
                    }
                    else return BadRequest("Movie already exists in database");
                }
                else return BadRequest(ModelState);

                //return Ok(result);

            }
            catch (Exception ex)
            {

                _logger.LogError($"Failed to add movie to database: {ex}");
                return BadRequest("Failed to add Movie to database");
            }
        }
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            try
            {
                var result = _repository.GetMovieById(id);
                if (result != null)
                {
                    _repository.DeleteMovie(result);
                    return Ok("Movie deleted");

                }
                else
                {
                    return NotFound("Movie does not exist");

                }
                

            }
            catch (Exception ex)
            {

                _logger.LogError($"Failed to add movie to database: {ex}");
                return BadRequest("Failed to add Movie to database");
            }
        }

        [HttpPut]
        public IActionResult Put([FromBody] MovieViewModel movie, [FromQuery] bool actorsOnly = true)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var newMovie = _mapper.Map<MovieViewModel, Movie>(movie);

                    var result = _repository.GetMovieById(newMovie.Id);
                    if (result != null)
                    {
                        if (actorsOnly)
                        {

                            foreach (var ma in newMovie.MovieActors)
                            {
                                ma.Actor = _repository.GetActorById(ma.ActorId);
                                ma.MovieId = newMovie.Id;
                            }
                            _repository.UpdateMovie(result, newMovie);
                            return Created($"api/movies/{result.Id}", _mapper.Map<Movie, MovieViewModel>(_repository.GetMovieById(result.Id)));

                        }
                        else
                        {
                            result.Name = movie.Name;
                            result.YearOfRelease = movie.YearOfRelease;
                            result.Plot = movie.Plot;
                            result.PosterUrl = movie.PosterUrl;
                            if (movie.PosterUrl.StartsWith("/upload"))
                            {

                                string webRootPath = _env.WebRootPath;
                                string oldPath = webRootPath + movie.PosterUrl;
                                if (System.IO.File.Exists(oldPath))
                                {
                                    string ext = oldPath.Substring(oldPath.LastIndexOf('.'));
                                    string newPath = Path.Combine(webRootPath, "images", "movies") + "/" + movie.Name + ext;
                                    System.IO.File.Move(oldPath, newPath);
                                    result.PosterUrl = "/images/movies/" + movie.Name + ext;
                                }

                            }
                            else { }

                            result.Producer = _repository.GetProducerById(newMovie.Producer.Id);
                            _repository.UpdateMovie(result);
                            return Created($"api/movies/{result.Id}", _mapper.Map<Movie, MovieViewModel>(_repository.GetMovieById(result.Id)));

                        }
                    }
                    else
                    {
                        return NotFound("Movie does not exist");
                    }

                }
                else return BadRequest(ModelState);

                //return Ok(result);

            }
            catch (Exception ex)
            {

                _logger.LogError($"Failed to update movie to database: {ex}");
                return BadRequest("Failed to update Movie to database");
            }
        }


    }
}
