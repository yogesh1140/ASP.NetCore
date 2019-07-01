using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Logging.Console;
using SamuraiApp.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SamuraiApp.Data
{
    public class SamuraiContext : DbContext
    {
        //public static readonly LoggerFactory myConsoleLoggerFactory = new LoggerFactory(new[] {
        //     new ConsoleLoggerProvider((category, level) =>
        //         category == DbLoggerCategory.Database.Command.Name && level == LogLevel.Information, true)});
        public SamuraiContext()
        {
            Database.Migrate();
        }
        public DbSet<Samurai> Samurais { get; set; }
        public DbSet<Quote> Quotes { get; set; }
        public DbSet<Battle> Battles { get; set; }
        public DbQuery<SamuraiStat> SamuraiBattleStats { get; set; }


        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer("Data Source=DESKTOP-NASS03S;Integrated Security =True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False;Initial Catalog=SamuraiAppDB");
            optionsBuilder.UseLoggerFactory(GetLoggerFactory());
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Many-to-many
            modelBuilder.Entity<SamuraiBattle>()
                .HasKey(s => new { s.SamuraiId, s.BattleId });


            // One-to-one
            //specify foreignkey using shadow property, nullable foreign key
            modelBuilder.Entity<Samurai>()
                .HasOne(i => i.SecretIdentity)
                .WithOne()
                .HasForeignKey<SecretIdentity>(i => i.SamuraiId);

            //or non-nullable foreigh key
            //modelBuilder.Entity<Samurai>()
            //   .HasOne(s => s.SecretIdentity)
            //   .WithOne(i => i.Samurai).IsRequired();



            // Adding Shadow property to all
            foreach (var entityType in modelBuilder.Model.GetEntityTypes())
            {
                if (entityType.ClrType.BaseType != typeof(DbView))
                {
                    modelBuilder.Entity(entityType.Name).Property<DateTime>("Created");
                    modelBuilder.Entity(entityType.Name).Property<DateTime>("LastModified");
                }
            }

            // Owned Types
            modelBuilder.Entity<Samurai>().OwnsOne(s => s.BetterName).Property(b => b.GivenName).HasColumnName("GivenName");
            modelBuilder.Entity<Samurai>().OwnsOne(s => s.BetterName).Property(b => b.SurName).HasColumnName("SurName");

            // modelBuilder.Entity<SamuraiStat>().HasKey(s => s.SamuraiId);
            modelBuilder.Query<SamuraiStat>().ToView("SamuraiBattleStat");

        }
        private ILoggerFactory GetLoggerFactory()
        {
            IServiceCollection serviceCollection = new ServiceCollection();
            serviceCollection.AddLogging(builder =>
                   builder.AddConsole()
                          .AddFilter(DbLoggerCategory.Database.Command.Name,
                                     LogLevel.Information));
            return serviceCollection.BuildServiceProvider()
                    .GetService<ILoggerFactory>();
        }



        // Interact With Shadow Property
        public override int SaveChanges()
        {
            ChangeTracker.DetectChanges();
            var timestamp = DateTime.Now;
            foreach (var entry in ChangeTracker.Entries()
                .Where(e => (e.State == EntityState.Added || e.State == EntityState.Modified)
                             && !e.Metadata.IsOwned()))
            {
                entry.Property("LastModified").CurrentValue = timestamp;
                if (entry.State == EntityState.Added)
                {
                    entry.Property("Created").CurrentValue = timestamp;
                }

                if (entry.Entity is Samurai)
                {
                    if (entry.Reference("BetterName").CurrentValue == null)
                    {
                        entry.Reference("BetterName").CurrentValue = PersonFullName.Empty();
                    }
                    entry.Reference("BetterName").TargetEntry.State = entry.State;
                }
            }
            return base.SaveChanges();
        }


        //Scalar Functions

        [DbFunction(Schema = "dbo")]
        public static string EarliestBattleFoughtBySamurai(int samuraiId)
        {
            throw new Exception();
        }


        [DbFunction(Schema = "dbo")]
        public static int DaysInBattle(DateTime start, DateTime end)
        {
            return (int)end.Subtract(start).TotalDays + 1;
        }
    }
}
