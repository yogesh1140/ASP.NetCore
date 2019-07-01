using Microsoft.EntityFrameworkCore;
using SamuraiApp.Data;
using SamuraiApp.Domain;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;

namespace SomeUI
{
    class Program
    {
        private static SamuraiContext _context = new SamuraiContext();
        static void Main(string[] args)
        {
            using (var newContext = new SamuraiContext())
            {
                newContext.EnsureDatabaseSeed();
            }
            #region Module 3 methods
            //InsertSamurai();
            //InsertMultipleSamurais();
            //SimpleSamuraiQuery();
            //MoreQueries();
            //RetrieveAndUpdateSamurai();
            //RetrieveAndUpdateMultipleSamurais();
            //MultipleDatabaseOperations();
            //QueryAndUpdateSamurai_Disconnected();
            //InsertBattle();
            //QueryAndUpdateBattle_Disconnected();
            //AddSomeMoreSamurais();
            //DeleteWhileTracked();
            //DeleteWhileNotTracked();
            //DeleteMany();
            //DeleteUsingId(3);
            #endregion

            //______Methods added in Module 4 (Working with related data)______
            //InsertNewPkFkGraph();
            //InsertNewPkFkGraphMultipleChildren();
            //AddChildToExistingObjectWhileTracked();
            //AddChildToExistingObjectWhileNotTracked(1);
            //EagerLoadSamuraiWithQuotes();
            //var dynamicList = ProjectDynamic();
            //ProjectSomeProperties();
            //ProjectSamuraisWithQuotes();
            //FilteringWithRelatedData();
            //ModifyingRelatedDataWhenTracked();
            //ModifyingRelatedDataWhenNotTracked();


        }
        #region Many-to-Many
        private static void RemoveBattleFromSamuraiWhenDisconnected()
        {
            //Goal:Remove join between Shichirōji(Id=3) and Battle of Okehazama (Id=1)
            Samurai samurai;
            using (var separateOperation = new SamuraiContext())
            {
                samurai = separateOperation.Samurais.Include(s => s.SamuraiBattles)
                                                    .ThenInclude(sb => sb.Battle)
                                           .SingleOrDefault(s => s.Id == 3);
            }
            var sbToRemove = samurai.SamuraiBattles.SingleOrDefault(sb => sb.BattleId == 1);
            samurai.SamuraiBattles.Remove(sbToRemove);
            //_context.Attach(samurai);
            //_context.ChangeTracker.DetectChanges();
            _context.Remove(sbToRemove);
            _context.SaveChanges();
        }
        private static void RemoveBattleFromSamurai()
        {
            //Goal:Remove join between Shichirōji(Id=3) and Battle of Okehazama (Id=1)
            var samurai = _context.Samurais.Include(s => s.SamuraiBattles)
                                           .ThenInclude(sb => sb.Battle)
                                  .SingleOrDefault(s => s.Id == 3);
            var sbToRemove = samurai.SamuraiBattles.SingleOrDefault(sb => sb.BattleId == 1);
            samurai.SamuraiBattles.Remove(sbToRemove); //remove via List<T>
                                                       //_context.Remove(sbToRemove); //remove using DbContext
            _context.ChangeTracker.DetectChanges(); //here for debugging
            _context.SaveChanges();
        }
        private static void RemoveJoinBetweenSamuraiAndBattleSimple()
        {
            var join = new SamuraiBattle { BattleId = 1, SamuraiId = 8 };
            _context.Remove(join);
            _context.SaveChanges();
        }
        private static void GetBattlesForSamuraiInMemory()
        {
            var battle = _context.Battles.Find(1);
            _context.Entry(battle).Collection(b => b.SamuraiBattles).Query().Include(sb => sb.Samurai).Load();

        }
        private static void GetSamuraiWithBattles()
        {
            var samuraiWithBattles = _context.Samurais
                .Include(s => s.SamuraiBattles)
                .ThenInclude(sb => sb.Battle).FirstOrDefault(s => s.Id == 1);
            var battle = samuraiWithBattles.SamuraiBattles.First().Battle;
            var allTheBattles = new List<Battle>();
            foreach (var samuraiBattle in samuraiWithBattles.SamuraiBattles)
            {
                allTheBattles.Add(samuraiBattle.Battle);
            }
        }
        private static void AddNewSamuraiViaDisconnectedBattleObject()
        {
            Battle battle;
            using (var separateOperation = new SamuraiContext())
            {
                battle = separateOperation.Battles.Find(1);
            }
            var newSamurai = new Samurai { Name = "SampsonSan" };
            battle.SamuraiBattles.Add(new SamuraiBattle { Samurai = newSamurai });
            _context.Battles.Attach(battle);
            _context.ChangeTracker.DetectChanges();
            _context.SaveChanges();
        }

        private static void EnlistSamuraiIntoABattleUntracked()
        {
            Battle battle;
            using (var separateOperation = new SamuraiContext())
            {
                battle = separateOperation.Battles.Find(1);
            }
            battle.SamuraiBattles.Add(new SamuraiBattle { SamuraiId = 2 });
            _context.Battles.Attach(battle);
            _context.ChangeTracker.DetectChanges(); //here to show you debugging info
            _context.SaveChanges();

        }
        private static void EnlistSamuraiIntoABattle()
        {
            var battle = _context.Battles.Find(1);
            battle.SamuraiBattles
                .Add(new SamuraiBattle { SamuraiId = 3 });
            _context.SaveChanges();
        }

        private static void JoinBattleAndSamurai()
        {
            //Kikuchiyo id is 1, Siege of Osaka id is 3
            var sbJoin = new SamuraiBattle { SamuraiId = 1, BattleId = 3 };
            _context.Add(sbJoin);
            _context.SaveChanges();
        }

        private static void PrePopulateSamuraisAndBattles()
        {
            _context.AddRange(
             new Samurai { Name = "Kikuchiyo" },
             new Samurai { Name = "Kambei Shimada" },
             new Samurai { Name = "Shichirōji " },
             new Samurai { Name = "Katsushirō Okamoto" },
             new Samurai { Name = "Heihachi Hayashida" },
             new Samurai { Name = "Kyūzō" },
             new Samurai { Name = "Gorōbei Katayama" }
           );

            _context.Battles.AddRange(
             new Battle { Name = "Battle of Okehazama", StartDate = new DateTime(1560, 05, 01), EndDate = new DateTime(1560, 06, 15) },
             new Battle { Name = "Battle of Shiroyama", StartDate = new DateTime(1877, 9, 24), EndDate = new DateTime(1877, 9, 24) },
             new Battle { Name = "Siege of Osaka", StartDate = new DateTime(1614, 1, 1), EndDate = new DateTime(1615, 12, 31) },
             new Battle { Name = "Boshin War", StartDate = new DateTime(1868, 1, 1), EndDate = new DateTime(1869, 1, 1) }
           );
            _context.SaveChanges();
        }

        #endregion

        #region mapping and interacting with one-to-one
        private static void ReplaceSecretIdentityNotInMemory()
        {
            var samurai = _context.Samurais.FirstOrDefault(s => s.SecretIdentity != null);
            samurai.SecretIdentity = new SecretIdentity { RealName = "Bobbie Draper" };
            _context.SaveChanges();
        }
        private static void ReplaceASecretIdentityNotTracked()
        {
            Samurai samurai;
            using (var separateOperation = new SamuraiContext())
            {
                samurai = separateOperation.Samurais.Include(s => s.SecretIdentity)
                                           .FirstOrDefault(s => s.Id == 1);
            }
            samurai.SecretIdentity = new SecretIdentity { RealName = "Sampson" };
            _context.Samurais.Attach(samurai);
            //this will fail...EF Core tries to insert a duplicate samuraiID FK
            _context.SaveChanges();
        }
        private static void ReplaceASecretIdentity()
        {
            var samurai = _context.Samurais.Include(s => s.SecretIdentity)
                                  .FirstOrDefault(s => s.Id == 1);
            samurai.SecretIdentity = new SecretIdentity { RealName = "Sampson" };
            _context.SaveChanges();
        }
        private static void EditASecretIdentity()
        {
            var samurai = _context.Samurais.Include(s => s.SecretIdentity)
                                  .FirstOrDefault(s => s.Id == 1);
            samurai.SecretIdentity.RealName = "T'Challa";
            _context.SaveChanges();
        }
        private static void AddSecretIdentityToExistingSamurai()
        {
            Samurai samurai;
            using (var separateOperation = new SamuraiContext())
            {
                samurai = _context.Samurais.Find(2);
            }
            samurai.SecretIdentity = new SecretIdentity { RealName = "Julia" };
            _context.Samurais.Attach(samurai);
            _context.SaveChanges();
        }
        private static void AddSecretIdentityUsingSamuraiId()
        {
            //Note: SamuraiId 1 does not have a secret identity yet!
            var identity = new SecretIdentity { SamuraiId = 1, };
            _context.Add(identity);
            _context.SaveChanges();
        }
        private static void AddNewSamuraiWithSecretIdentity()
        {
            var samurai = new Samurai { Name = "Jina Ujichika" };
            samurai.SecretIdentity = new SecretIdentity { RealName = "Julie" };
            _context.Samurais.Add(samurai);
            _context.SaveChanges();
        }
               
        #endregion

        #region shadow property
        private static void RetrieveSamuraisCreatedInPastWeek()
        {
            var oneWeekAgo = DateTime.Now.AddDays(-7);
            //var newSamurais = _context.Samurais
            //                          .Where(s => EF.Property<DateTime>(s, "Created") >= oneWeekAgo)
            //                          .ToList();
            var samuraisCreated = _context.Samurais
                                        .Where(s => EF.Property<DateTime>(s, "Created") >= oneWeekAgo)
                                        .Select(s => new { s.Id, s.Name, Created = EF.Property<DateTime>(s, "Created") })
                                        .ToList();
        }

        private static void CreateSamurai()
        {
            var samurai = new Samurai { Name = "Ronin" };
            _context.Samurais.Add(samurai);
            _context.Entry(samurai).Property("Created").CurrentValue = DateTime.Now;
            _context.Entry(samurai).Property("LastModified").CurrentValue = DateTime.Now;
            _context.SaveChanges();
        }

        private static void CreateThenEditSamuraiWithQuote()
        {
            var samurai = new Samurai { Name = "Ronin" };
            var quote = new Quote { Text = "Aren't I MARVELous?" };
            samurai.Quotes.Add(quote);
            _context.Samurais.Add(samurai);
            _context.SaveChanges();
            quote.Text += " See what I did there?";
            _context.SaveChanges();
        }

        #endregion

        #region Working with Shadow Property
        private static void CreateSamuraiWithBetterName()
        {
            var samurai = new Samurai
            {
                Name = "Jack le Black",
                BetterName = PersonFullName.Create("Jack", "Black")
            };
            _context.Samurais.Add(samurai);
            _context.SaveChanges();
        }

        private static void CreateAndFixUpNullBetterName()
        {
            _context.Samurais.Add(new Samurai { Name = "Chrisjen" });
            _context.SaveChanges();
            _context = new SamuraiContext();
            var persistedSamurai = _context.Samurais.FirstOrDefault(s => s.Name == "Chrisjen");
            if (persistedSamurai is null) { return; }
            if (persistedSamurai.BetterName.IsEmpty())
            {
                persistedSamurai.BetterName = null;
            }
        }

        private static void ReplaceBetterName()
        {
            var samurai = _context.Samurais.FirstOrDefault(s => s.Name == "Chrisjen");
            _context.Entry(samurai).Reference(s => s.BetterName).TargetEntry.State = EntityState.Detached;
            samurai.BetterName = PersonFullName.Create("Shohreh", "Aghdashloo");
            _context.Samurais.Update(samurai);
            _context.SaveChanges();
        }

        //private static void RetrieveAndUpdateBetterName()
        //{
        //    var samurai = _context.Samurais.FirstOrDefault(s => s.BetterName.SurName == "Black");
        //    samurai.BetterName.GivenName = "Jill";
        //    _context.SaveChanges();
        //}

        private static void GetAllSamurais()
        {
            var allsamurais = _context.Samurais.ToList();
        }

        
        #endregion


        #region Scalar Functions

        private static void RetrieveYearUsingDbBuiltInFunction()
        {
            var battles = _context.Battles
                 .Select(b => new { b.Name, b.StartDate.Year }).ToList();
        }

        private static void RetrieveScalarResult()
        {
            var samurais = _context.Samurais
                .Select(s => new
                {
                    s.Name,
                    EarliestBattle = SamuraiContext.EarliestBattleFoughtBySamurai(s.Id)
                })
                .ToList();
        }
        private static void FilterWithScalarResult()
        {
            var samurais = _context.Samurais
                    .Where(s => EF.Functions.Like(SamuraiContext.EarliestBattleFoughtBySamurai(s.Id), "%Battle%"))
                    .Select(s => new
                    {
                        s.Name,
                        EarliestBattle = SamuraiContext.EarliestBattleFoughtBySamurai(s.Id)
                    })
                   .ToList();
        }
        private static void SortWithScalar()
        {
            var samurais = _context.Samurais
                 .OrderBy(s => SamuraiContext.EarliestBattleFoughtBySamurai(s.Id))
                 .Select(s => new { s.Name, EarliestBattle = SamuraiContext.EarliestBattleFoughtBySamurai(s.Id) })
                 .ToList();
        }
        private static void SortWithoutReturningScalar()
        {
            var samurais = _context.Samurais
                 .OrderBy(s => SamuraiContext.EarliestBattleFoughtBySamurai(s.Id))
                 .ToList();
        }
        private static void RetrieveBattleDays()
        {
            var battles = _context.Battles.Select(b => new { b.Name, Days = SamuraiContext.DaysInBattle(b.StartDate, b.EndDate) }).ToList();
        }

        private static void RetrieveBattleDaysWithoutDbFunction()
        {
            var battles = _context.Battles.Select(
                b => new
                {
                    b.Name,
                    Days = DateDiffDaysPlusOne(b.StartDate, b.EndDate)
                }
                ).ToList();
        }
        private static int DateDiffDaysPlusOne(DateTime start, DateTime end)
        {
            return (int)end.Subtract(start).TotalDays + 1;
        }
        #endregion

        #region Views
        private static void GetStats()
        {
            var stats = _context.SamuraiBattleStats.AsNoTracking().ToList();
        }
        private static void Filter()
        {
            var stats = _context.SamuraiBattleStats.Where(s => s.SamuraiId == 2).AsNoTracking().ToList();
        }
        private static void Project()
        {
            var stats = _context.SamuraiBattleStats.AsNoTracking().Select(s => new { s.Name, s.NumberOfBattles }).ToList();
        }

        #endregion

        private static void ModifyingRelatedDataWhenTracked()
        {
            var samurai = _context.Samurais.Include(s => s.Quotes).FirstOrDefault();
            samurai.Quotes[0].Text += " Did you hear that?";
            _context.SaveChanges();
        }



        #region Getting Started Methods
        private static void ModifyingRelatedDataWhenNotTracked()
        {
            var samurai = _context.Samurais.Include(s => s.Quotes).FirstOrDefault();
            var quote = samurai.Quotes[0];
            quote.Text += " Did you hear that?";
            using (var newContext = new SamuraiContext())
            {
                //newContext.Quotes.Update(quote);
                newContext.Entry(quote).State = EntityState.Modified;
                newContext.SaveChanges();
            }
        }


        private static void FilteringWithRelatedData()
        {
            var samurais = _context.Samurais
                                   .Where(s => s.Quotes.Any(q => q.Text.Contains("happy")))
                                   .ToList();
        }

        private static void ProjectSamuraisWithQuotes()
        {
            var somePropertiesWithQuotes = _context.Samurais
                .Select(s => new { s.Id, s.Name, s.Quotes.Count })
                .ToList();

        }

        public struct IdAndName
        {
            public IdAndName(int id, string name)
            {
                Id = id;
                Name = name;
            }
            public int Id;
            public string Name;
        }
        private static void ProjectSomeProperties()
        {
            var someProperties = _context.Samurais.Select(s => new { s.Id, s.Name }).ToList();
            var idsAndNames = _context.Samurais.Select(s => new IdAndName(s.Id, s.Name)).ToList();
        }

        private static List<dynamic> ProjectDynamic()
        {
            var someProperties = _context.Samurais.Select(s => new { s.Id, s.Name }).ToList();
            return someProperties.ToList<dynamic>();
        }

        private static void EagerLoadSamuraiWithQuotes()
        {
            var samuraiWithQuotes = _context.Samurais.Where(s => s.Name.Contains("Kyūzō"))
                                                     .Include(s => s.Quotes)
                                                     .Include(s => s.SecretIdentity)
                                                     .FirstOrDefault();
        }

        private static void AddChildToExistingObjectWhileNotTracked(int samuraiId)
        {
            var quote = new Quote
            {
                Text = "Now that I saved you, will you feed me dinner?",
                SamuraiId = samuraiId
            };
            using (var newContext = new SamuraiContext())
            {
                newContext.Quotes.Add(quote);
                newContext.SaveChanges();
            }
        }

        private static void AddChildToExistingObjectWhileTracked()
        {
            var samurai = _context.Samurais.First();
            samurai.Quotes.Add(new Quote
            {
                Text = "I bet you're happy that I've saved you!"
            });
            _context.SaveChanges();
        }

        private static void InsertNewPkFkGraphMultipleChildren()
        {
            var samurai = new Samurai
            {
                Name = "Kyūzō",
                Quotes = new List<Quote> {
                  new Quote {Text = "Watch out for my sharp sword!"},
                  new Quote {Text="I told you to watch out for the sharp sword! Oh well!" }
                }
            };
            _context.Samurais.Add(samurai);
            _context.SaveChanges();
        }

        private static void InsertNewPkFkGraph()
        {
            var samurai = new Samurai
            {
                Name = "Kambei Shimada",
                Quotes = new List<Quote>
                               {
                                 new Quote {Text = "I've come to save you"}
                               }
            };
            _context.Samurais.Add(samurai);
            _context.SaveChanges();
        }


        #region Module 3 methods
        private static void DeleteUsingId(int samuraiId)
        {
            var samurai = _context.Samurais.Find(samuraiId);
            _context.Remove(samurai);
            _context.SaveChanges();
            //alternate: call a stored procedure!
            //_context.Database.ExecuteSqlCommand("exec DeleteById {0}", samuraiId);
        }

        private static void AddSomeMoreSamurais()
        {
            _context.AddRange(
               new Samurai { Name = "Kambei Shimada" },
               new Samurai { Name = "Shichirōji " },
               new Samurai { Name = "Katsushirō Okamoto" },
               new Samurai { Name = "Heihachi Hayashida" },
               new Samurai { Name = "Kyūzō" },
               new Samurai { Name = "Gorōbei Katayama" }
             );
            _context.SaveChanges();
        }

        private static void DeleteWhileTracked()
        {
            var samurai = _context.Samurais.FirstOrDefault(s => s.Name == "Kambei Shimada");
            _context.Samurais.Remove(samurai);
            //some alternates:
            // _context.Remove(samurai);
            // _context.Samurais.Remove(_context.Samurais.Find(1));
            _context.SaveChanges();
        }

        private static void DeleteMany()
        {
            var samurais = _context.Samurais.Where(s => s.Name.Contains("ō"));
            _context.Samurais.RemoveRange(samurais);
            //alternate: _context.RemoveRange(samurais);
            _context.SaveChanges();
        }

        private static void DeleteWhileNotTracked()
        {
            var samurai = _context.Samurais.FirstOrDefault(s => s.Name == "Heihachi Hayashida");
            using (var contextNewAppInstance = new SamuraiContext())
            {
                contextNewAppInstance.Samurais.Remove(samurai);
                //contextNewAppInstance.Entry(samurai).State=EntityState.Deleted;
                contextNewAppInstance.SaveChanges();
            }
        }

        private static void SimpleSamuraiQuery()
        {
            using (var context = new SamuraiContext())
            {
                var samurais = context.Samurais.ToList();
                //var query = context.Samurais;
                //var samuraisAgain = query.ToList();
                foreach (var samurai in context.Samurais)
                {
                    Console.WriteLine(samurai.Name);
                }
            }
        }
        private static void MoreQueries()
        {
            var samurais_NonParameterizedQuery = _context.Samurais.Where(s => s.Name == "Sampson").ToList();
            var name = "Sampson";
            var samurais_ParameterizedQuery = _context.Samurais.Where(s => s.Name == name).ToList();
            var samurai_Object = _context.Samurais.FirstOrDefault(s => s.Name == name);
            var samurais_ObjectFindByKeyValue = _context.Samurais.Find(2);
            var samuraisJ = _context.Samurais.Where(s => EF.Functions.Like(s.Name, "J%")).ToList();
            var search = "J%";
            var samuraisJParameter = _context.Samurais.Where(s => EF.Functions.Like(s.Name, search)).ToList();

        }


        private static void RetrieveAndUpdateSamurai()
        {
            var samurai = _context.Samurais.FirstOrDefault();
            samurai.Name += "San";
            _context.SaveChanges();
        }
        private static void RetrieveAndUpdateMultipleSamurais()
        {
            var samurais = _context.Samurais.ToList();
            samurais.ForEach(s => s.Name += "San");
            _context.SaveChanges();
        }
        private static void MultipleDatabaseOperations()
        {
            var samurai = _context.Samurais.FirstOrDefault();
            samurai.Name += "Hiro";
            _context.Samurais.Add(new Samurai { Name = "Kikuchiyo" });
            _context.SaveChanges();
        }


        private static void QueryAndUpdateSamurai_Disconnected()
        {
            var samurai = _context.Samurais.FirstOrDefault(s => s.Name == "Kikuchiyo");
            samurai.Name += "San";
            using (var newContextInstance = new SamuraiContext())
            {
                newContextInstance.Samurais.Update(samurai);
                newContextInstance.SaveChanges();
            }
        }

        private static void InsertSamurai()
        {
            var samurai = new Samurai { Name = "Julie" };
            using (var context = new SamuraiContext())
            {
                context.Samurais.Add(samurai);
                context.SaveChanges();
            }
        }

        private static void InsertMultipleSamurais()
        {
            var samurai = new Samurai { Name = "Julie" };
            var samuraiSammy = new Samurai { Name = "Sampson" };
            using (var context = new SamuraiContext())
            {
                context.Samurais.AddRange(samurai, samuraiSammy);
                context.SaveChanges();
            }
        }

        private static void InsertBattle()
        {
            _context.Battles.Add(new Battle
            {
                Name = "Battle of Okehazama",
                StartDate = new DateTime(1560, 05, 01),
                EndDate = new DateTime(1560, 06, 15)
            });
            _context.SaveChanges();
        }
        private static void QueryAndUpdateBattle_Disconnected()
        {
            var battle = _context.Battles.FirstOrDefault();
            battle.EndDate = new DateTime(1560, 06, 30);
            using (var newContextInstance = new SamuraiContext())
            {
                newContextInstance.Battles.Update(battle);
                newContextInstance.SaveChanges();
            }
        }
        #endregion
        #endregion
    }
}
