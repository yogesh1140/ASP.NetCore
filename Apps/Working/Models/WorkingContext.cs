using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Working.Models
{
    public class WorkingContext : IdentityDbContext<WorkingUser>
    {
        public IConfigurationRoot _config { get; set; }
        public WorkingContext(IConfigurationRoot config, DbContextOptions options):base(options)
        {
            _config = config;
        }

        public DbSet<Trip> Trips { get; set; }
        public DbSet<Stop> Stops { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionBuilder)
        {
            base.OnConfiguring(optionBuilder);
            optionBuilder.EnableSensitiveDataLogging();
        }
    }
}
