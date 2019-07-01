using AngularWorking.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AngularWorking.Data
{
    public class AngularDbContext: IdentityDbContext<AngularUser>
    {
        public IConfiguration _configuration { get; set; }
        public AngularDbContext(IConfiguration config, DbContextOptions options) : base(options)
        {
            _configuration = config;
        }

        //public DbSet<Trip> Trips { get; set; }
        //public DbSet<Stop> Stops { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionBuilder)
        {
            base.OnConfiguring(optionBuilder);
            optionBuilder.UseSqlServer(_configuration["ConnectionStrings:AngularConnection"]);
        }

    }
   
}
