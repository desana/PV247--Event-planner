using System.Collections.Generic;
using EventPlanner.Entities;

namespace EventPlanner.Migrations
{
 using System.Data.Entity.Migrations;

    internal sealed class Configuration : DbMigrationsConfiguration<EventPlannerContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = true;
        }

        protected override void Seed(EventPlannerContext context)
        {
            //  This method will be called after migrating to the latest version.

            context.Events.AddOrUpdate(
                e => e.Name,
                new Event()
                {
                    Name = "Meeting",
                    Description = "Company meeting winter 2016",
                    Link = "some.fake.link"
                }
                );
        }
    }
}
