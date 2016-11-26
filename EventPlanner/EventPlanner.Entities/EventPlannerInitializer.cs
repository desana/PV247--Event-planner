using System;
using System.Collections.Generic;
using EventPlanner.Entities;
using EventPlanner.Entities.Entities;

namespace EventPlanner
{
    class EventPlannerInitializer : System.Data.Entity.CreateDatabaseIfNotExists<EventPlannerContext>
    {
        protected override void Seed(EventPlannerContext context)
        {
            context.Events.Add(GetSomeEvent());
            context.SaveChanges();
        }

        private static Event GetSomeEvent()
        {
            return new Event()
            {
                Name = "Meeting",
                Description = "Company meeting winter 2016",
                Link = "some.fake.link",
                Places = new List<Place>
                {
                    new Place
                    {
                        FourSquareLink = "https://foursquare.com/v/u-karla/4c1f3003b4e62d7fb244df93",
                        Name = "U Karla",
                        Times = new List<TimeAtPlace> {new TimeAtPlace { Time = DateTime.Now} }
                    },
                    new Place
                    {
                        FourSquareLink = "https://foursquare.com/v/burger-inn/55a93496498e49f11b0a9532",
                        Name = "Burger Inn",
                        Times = new List<TimeAtPlace> {new TimeAtPlace { Time = DateTime.Now} }
                    }
                }
            };
        }
    }
}