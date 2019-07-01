using IMDBDummy.Data.Entities;
using IMDBDummy.ViewModels;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IMDBDummy.Data
{
    public class IMDBRepository : IIMDBRepository
    {
        private readonly IMDBContext _context;
        private readonly ILogger<IMDBRepository> _logger;

        public IMDBRepository(IMDBContext context, ILogger<IMDBRepository> logger)
        {
            _context = context;
            _logger = logger;
        }

        public void AddEntity(object model)
        {
            _context.Add(model);
        }


        public int AddMovie(Movie newMovie)
        {
            if(_context.Movies.Where(m => m.Name == newMovie.Name && m.YearOfRelease == newMovie.YearOfRelease).FirstOrDefault() == null)
            {
                newMovie.Producer = _context.Producers.Where(p => p.Id == newMovie.Producer.Id).FirstOrDefault();
                AddEntity(newMovie);
                _context.SaveChanges();
                return _context.Movies.Where(m => m.Name == newMovie.Name && m.YearOfRelease == newMovie.YearOfRelease).Select(m => m.Id).FirstOrDefault();

            }
            return 0;
        }

        public Actor AddActor (Actor actor)
        {
            AddEntity(actor);
            _context.SaveChanges();
            return _context.Actors.Where(a => a.FirstName == actor.FirstName && a.LastName == actor.LastName && a.DOB == actor.DOB).FirstOrDefault();
            
        }
        
        public Producer AddProducer(Producer producer)
        {
            AddEntity(producer);
            _context.SaveChanges();
            return _context.Producers.Where(p => p.FirstName == producer.FirstName && p.LastName == producer.LastName && p.DOB == producer.DOB).FirstOrDefault();

        }
        public IEnumerable<Movie> GetAllMovies()
        {
            var result = _context.Movies
                .Include(m=>m.Producer)
                .Include(m=>m.MovieActors)
                .ThenInclude(m=>m.Actor)
                .OrderByDescending(m=>m.YearOfRelease)
                .ToList();
            return result;
        }
        public Movie GetMovieById(int id)
        {
            var result = _context.Movies.Where(m=>m.Id==id)
                .Include(m => m.Producer)
                .Include(m => m.MovieActors)
                .ThenInclude(m => m.Actor)
                .FirstOrDefault();
            return result;


        }
        public bool SaveAll()
        {
            return _context.SaveChanges() > 0;
        }

        public IEnumerable<Producer> GetAllProducers()
        {
            return _context.Producers.OrderBy(p=>p.FirstName).ToList();
        }

        public IEnumerable<Actor> GetAllActors()
        {
            return _context.Actors.OrderBy(a=>a.FirstName).ToList();
        }

        public void UpdateMovie(Movie existingMovie, Movie newMovie)
        {
            
            _context.TryUpdateManyToMany(existingMovie.MovieActors, newMovie.MovieActors, ma => ma.ActorId);
            _context.SaveChanges();
            
           
        }

        public Producer GetProducerById(int id)
        {
           return _context.Producers.Where(p => p.Id == id).FirstOrDefault();
        }

        public Actor GetActorById(int id)
        {
            return _context.Actors.Where(a => a.Id == id).FirstOrDefault();
        }
        public bool IsActorExist(Actor actor)
        {
            return (_context.Actors.Where(act => act.FirstName.ToLower() == actor.FirstName.ToLower() && act.LastName.ToLower() == actor.LastName.ToLower() && act.DOB == actor.DOB).FirstOrDefault() != null);
        }
        public bool IsProducerExist(Producer producer)
        {
            return (_context.Producers.Where(pr => pr.FirstName.ToLower() == producer.FirstName.ToLower() && pr.LastName.ToLower() == producer.LastName.ToLower() && pr.DOB == producer.DOB).FirstOrDefault() != null);
        }

        public void UpdateMovie(Movie movie)
        {
            _context.Movies.Update(movie);
            _context.SaveChanges();
        }

        public void DeleteMovie(Movie movie)
        {
            _context.Remove(movie);
            _context.SaveChanges();

        }
    }
}
