﻿using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CityInfo.API.Entities
{
    public class CityInfoContext: DbContext
    {
        public DbSet<City> Cities { get; set; }
        public DbSet<PointOfInterest> PointsOfInterest { get; set; }

        public CityInfoContext(DbContextOptions<CityInfoContext> options): base(options)
        {
            // Database.EnsureCreated();

            // Executes EF Migration 
            Database.Migrate();
           

        }
        //protected override void OnConfiguring(DbContextOptionsBuilder optionBuilder) {
        //    optionBuilder.UseSqlServer("connectionstring");
        //    base.OnConfiguring(optionBuilder);
        //}
    }
}
