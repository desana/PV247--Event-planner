using System;
using System.Collections.Generic;
using System.Data.Entity.Migrations;

namespace EventPlanner.Entities.Migrations
{
    internal sealed class Configuration : DbMigrationsConfiguration<EventPlannerContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
        }

        protected override void Seed(EventPlannerContext context)
        {
            //  This method will be called after migrating to the latest version.

            var kafec = new Place
            {
                FourSquareId = "503327b8e4b020d49ee37f72",
                Name = "Kafec",
            };

            var franc = new Place
            {
                FourSquareId = "4edb4f7029c2b9122985cec4",
                Name = "Bistro franc",
            };

            context.Places.AddOrUpdate(p => p.FourSquareId, kafec, franc);

            context.Events.AddOrUpdate(e => e.Name, 
                new Event
                {
                    Name = "Meeting",
                    Description = "Company meeting winter 2016",
                    Link = "some.fake.link",
                    TimesAtPlaces = new List<TimeAtPlace>
                    {
                        new TimeAtPlace
                        {
                            Time = DateTime.Parse("2016-11-21T19:23:00"),
                            Place = kafec,
                        },
                        new TimeAtPlace
                        {
                            Time = DateTime.Parse("2016-11-21T19:22:00"),
                            Place = kafec,
                        },
                        new TimeAtPlace
                        {
                            Time = DateTime.Parse("2016-11-21T19:23:00"),
                            Place = franc,
                        }
                    }
                });
        }
    }
}
