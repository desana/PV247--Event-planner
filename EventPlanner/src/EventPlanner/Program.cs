using System;
using System.Collections.Generic;
using System.IO;
using EventPlanner.Entities;
using Microsoft.AspNetCore.Hosting;

namespace EventPlanner
{
    public class Program
    {
        public static void Main(string[] args)
        {
            using (var databaseContext = new EventPlannerDbContext())
            {
                Event e = new Event()
                {
                    EventName = "Meeting",
                    EventDescription = "Company meeting winter 2016",
                    EventLink = "some.fake.link",
                    Votes = new List<Vote>
                {
                    new Vote
                    {
                        TimeAtPlace = new TimeAtPlace
                        {
                            Place = new Place { FourSquareLink = "https://foursquare.com/v/u-karla/4c1f3003b4e62d7fb244df93", Name = "U Karla"},
                            Time = DateTime.Now
                        }
                    },
                    new Vote
                    {
                        TimeAtPlace = new TimeAtPlace
                        {
                            Place = new Place { FourSquareLink = "https://foursquare.com/v/burger-inn/55a93496498e49f11b0a9532", Name = "Burger Inn"},
                            Time = DateTime.Now
                        }
                    }
                }

                };


                databaseContext
                    .Events
                    .Add(e);

                databaseContext.SaveChanges();
            }


            var host = new WebHostBuilder()
                .UseKestrel()
                .UseContentRoot(Directory.GetCurrentDirectory())
                .UseIISIntegration()
                .UseStartup<Startup>()
                .Build();

            host.Run();
        }

    }
}
