using SamuraiApp.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SamuraiApp.Data
{
    public static class SamuraiContextExtension
    {
        public static void EnsureDatabaseSeed(this SamuraiContext context)
        {
            List<Samurai> samaurais = new List<Samurai>() {
                new Samurai { Name = "Kikuchiyo" },
                new Samurai { Name = "Kambei Shimada" },
                new Samurai { Name = "Shichirōji " },
                new Samurai { Name = "Katsushirō Okamoto" },
                new Samurai { Name = "Heihachi Hayashida" },
                new Samurai { Name = "Kyūzō" },
                new Samurai { Name = "Gorōbei Katayama" }
                };

            List<Battle> battles = new List<Battle>() {

                     new Battle { Name = "Battle of Okehazama", StartDate = new DateTime(1560, 05, 01), EndDate = new DateTime(1560, 06, 15) },
                     new Battle { Name = "Battle of Shiroyama", StartDate = new DateTime(1877, 9, 24), EndDate = new DateTime(1877, 9, 24) },
                     new Battle { Name = "Siege of Osaka", StartDate = new DateTime(1614, 1, 1), EndDate = new DateTime(1615, 12, 31) },
                     new Battle { Name = "Boshin War", StartDate = new DateTime(1868, 1, 1), EndDate = new DateTime(1869, 1, 1) }
            };
            if (!context.Samurais.Any())
            {
                // context.Battles.RemoveRange(battles);
                //context.Samurais.RemoveRange(samaurais);

                //context.Samurais.AddRange(samaurais);
                //context.Battles.AddRange(battles);
            }

            context.SaveChanges();


        }
    }
}
