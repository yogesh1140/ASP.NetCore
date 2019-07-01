using IMDBDummy.Data.Entities;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IMDBDummy.Data
{
    public class IMDBSeeder
    {
        private readonly IMDBContext _context;
        private readonly IHostingEnvironment _hosting;
        public IMDBSeeder(IMDBContext context, IHostingEnvironment hosting)
        {
            _context = context;
            _hosting = hosting;
        }
        public async Task Seed()
        {
            //_context.Database.EnsureDeleted();
            _context.Database.EnsureCreated();
            //var user =await _userManager.FindByEmailAsync("jadhayogesh222@gmail.com");
            //if (user == null)
            //{
            //    user = new IMDBUser() {
            //        FirstName="Yogesh",
            //        LastName="Jadhav",
            //        UserName="yogeshj",
            //        Email="jadhavyogesh222@gmail.com"
            //    };
            //    var result = await _userManager.CreateAsync(user,"P@ssw0rd!");
            //    if (result == IdentityResult.Success)
            //    {
            //        throw new InvalidOperationException("Failed to create default user");
            //    }
            //    else
            //    {

            //    }

            //}

            if (!_context.Movies.Any())
            {                
                var actors = new[] {
                      new Actor
                      {
                          
                          FirstName = "Emilia",
                          LastName ="Clarke",
                          Sex ="Female",
                          DOB = new DateTime(1986,10,23) ,
                          Bio = "British actress Emilia Clarke was born in London and grew up in Berkshire, England. Her father is a theatre sound engineer and her mother is a businesswoman. Her father was working on a theatre production of \"Show Boat\" and her mother took her along to the performance. "
                      },
                      new Actor
                      {
                          
                          FirstName = "Alexandra",
                          Sex ="Female",
                          LastName="Daddario",
                          DOB = new DateTime(1986, 3, 16) ,
                          Bio = "Alexandra Anna Daddario was born on March 16, 1986 in New York City, New York, to Christina, a lawyer, and Richard Daddario, a prosecutor. Her brother is actor Matthew Daddario, and her grandfather was congressman Emilio Daddario (Emilio Q. Daddario), of Connecticut. She has Italian, Irish, Hungarian/Slovak, German, and English ancestry"

                      },

                      new Actor
                      {
                          
                          FirstName = "Ryan",
                          Sex ="Male",
                          LastName ="Reynolds",
                          DOB = new DateTime(1976, 10, 23),
                          Bio = "Ryan Rodney Reynolds was born on October 23, 1976 in Vancouver, British Columbia, Canada, the youngest of four children. His father, James Chester Reynolds, was a food wholesaler, and his mother, Tammy, worked as a retail-store saleswoman."

                      },
                      new Actor
                      {
                          
                          FirstName = "Jennifer",
                          LastName="Lawerence",
                          Sex ="Female",
                          DOB = new DateTime(1990, 8, 15),
                          Bio = "Was the highest-paid actress in the world in 2015 and 2016. With her films grossing over $5.5 billion worldwide, Jennifer Lawrence is often cited as the most successful actor of her generation. She is also thus far the only person born in the 1990s to have won an acting Oscar. "

                      },
                      new Actor
                      {
                          
                          FirstName = "Chris",
                          LastName ="Pratt",
                          DOB = new DateTime(1979, 6, 21),
                          Sex ="Male",

                          Bio = "Christopher Michael Pratt is an American film and television actor. He came to prominence from his television roles, including Bright Abbott in Everwood (2002), Ché in The O.C. (2003), and Andy Dwyer and Parks and Recreation (2009), and notable film roles in Moneyball (2011), The Five-Year Engagement (2012), Zero Dark Thirty (2012), etc"

                      },
                      new Actor
                      {
                          
                          FirstName = "Keanu",
                          Sex ="Male",
                          LastName ="Reeves",
                          DOB = new DateTime(1964, 2, 11),
                          Bio = "Keanu Charles Reeves, whose first name means \"cool breeze over the mountains\" in Hawaiian, was born September 2, 1964 in Beirut, Lebanon. He is the son of Patricia Taylor, a showgirl and costume designer, and Samuel Nowlin Reeves, a geologist. Keanu's father was born in Hawaii, of British, Portuguese, Native Hawaiian, and Chinese ancestry\""

                      }
                };

            
            _context.Actors.AddRange(actors);



                var producers = new[] {

                      new Producer{
                        
                        FirstName= "Simon",
                          Sex ="Male",
                        LastName ="Kinberg",
                        DOB= new DateTime(1973,8,2),
                        Bio= "Simon Kinberg was born on August 2, 1973 in London, England. He is a producer and writer, known for The Martian (2015), Logan (2017) and Fantastic Four (2015). He was previously married to Mali Heled"
                          },
                      new Producer{
                        
                        FirstName= "Bruce",
                          Sex ="Male",
                        LastName ="Berman",
                        DOB= new DateTime(1952,1,1),
                        Bio= "Bruce Berman is Chairman and CEO of Village Roadshow Pictures. The company has a successful joint partnership with Warner Bros. Pictures to co-produce a wide range of motion pictures, with all films distributed worldwide by Warner Bros. and in select territories by Village Roadshow Pictures."

                      }

                };
            _context.Producers.AddRange(producers);


                var movies = new[] {
                    new Movie {
                        
                        Name ="Deadpool 2",
                        YearOfRelease= 2018,
                        PosterUrl= "/images/movies/deadpool2.jpg",
                        Plot="Wisecracking mercenary Deadpool meets Russell, an angry teenage mutant who lives at an orphanage. When Russell becomes the target of Cable -- a genetically enhanced soldier from the future -- Deadpool realizes that he'll need some help saving the boy from such a superior enemy. He soon joins forces with Bedlam, Shatterstar, Domino and other powerful mutants to protect young Russell from Cable and his advanced weaponry.",
                        Producer = producers[0]

                        
                    },
                    new Movie {
                        
                        Name ="The Matrix",
                        YearOfRelease= 1999,
                        Plot="Thomas, a computer programmer, is led to fight an underground war against powerful computers who now rule the world with a system called 'The Matrix'.",
                        PosterUrl= "/images/movies/thematrix.jpg",
                        Producer = producers[1]
                }
                };
            _context.Movies.AddRange(movies);


                var movieActor = new[] {
                    new MovieActor{Movie= movies[0],Actor = actors[2]},
                    new MovieActor{Movie = movies[1], Actor = actors[5]}
                };
                _context.AddRange(movieActor);

        }
        _context.SaveChanges();
        }
}
}

