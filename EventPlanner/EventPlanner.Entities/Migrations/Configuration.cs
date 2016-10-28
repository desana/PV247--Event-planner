using System.Collections.Generic;
using EventPlanner.Entities;

namespace EventPlanner.Migrations
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;

    internal sealed class Configuration : DbMigrationsConfiguration<EventPlanner.EventPlannerDbContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = true;
        }

        protected override void Seed(EventPlannerDbContext context)
        {
            //  This method will be called after migrating to the latest version.

            context.Events.AddOrUpdate(
                e => e.EventName,
                new Event()
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

                }
                );
        }
    }
}
