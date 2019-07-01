using AutoMapper;
using IMDBDummy.Data.Entities;
using IMDBDummy.ViewModels;

namespace IMDBDummy.Data
{
    public class IMDBMappingProfile:Profile
    {
        public IMDBMappingProfile()
        {
            CreateMap<Movie, MovieViewModel>()
               // .ForMember(o => o.OrderId, ex => ex.MapFrom(o => o.Id))
                .ReverseMap();
            CreateMap<Producer, ProducerViewModel>()
                .ReverseMap();
            CreateMap<Actor, ActorViewModel>()
                .ReverseMap();
            CreateMap<MovieActor, MovieActorViewModel>()
                .ReverseMap();
        }
    }
}
