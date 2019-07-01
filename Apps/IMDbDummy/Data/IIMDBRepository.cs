using System.Collections.Generic;
using IMDBDummy.Data.Entities;
using IMDBDummy.ViewModels;

namespace IMDBDummy.Data
{
    public interface IIMDBRepository
    {
        void AddEntity(object model);
        IEnumerable<Movie> GetAllMovies();
        Movie GetMovieById(int id);
        bool SaveAll();
        int AddMovie(Movie newMovie);
        Producer AddProducer(Producer newProducer);
        IEnumerable<Producer> GetAllProducers();
        IEnumerable<Actor> GetAllActors();
        void UpdateMovie(Movie newMovie, Movie vm);
        Producer GetProducerById(int id);
        Actor GetActorById(int id);
        Actor AddActor(Actor actor);
        bool IsActorExist(Actor actor);
        bool IsProducerExist(Producer producer);
        void UpdateMovie(Movie movie);
        void DeleteMovie(Movie movie);
    }
}