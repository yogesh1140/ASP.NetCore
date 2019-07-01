using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using IMDBDummy.Data.Entities;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace IMDBDummy.Data
{
    public class IMDBContext :DbContext
    {
        public DbSet<Actor> Actors { get; set; }
        public DbSet<Movie> Movies { get; set; }
        public DbSet<Producer> Producers { get; set; }

        public IMDBContext(DbContextOptions<IMDBContext> options):base(options)
        {

        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<MovieActor>()
                .HasKey(m => new { m.ActorId, m.MovieId });
        }
        //protected override void OnConfiguring(DbContextOptionsBuilder optionBuilder)
        //{
        //    base.OnConfiguring(optionBuilder);
        //    optionBuilder.EnableSensitiveDataLogging =true;
        //}
        

    }
}
