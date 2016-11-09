using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using EventPlanner.Repositories;
using Xunit;
using Moq;

namespace EventPlanner.Entities.UnitTests
{
    public class EventsRepositoryTests
    {
        private EventsRepository _eventsRepository;

        public EventsRepositoryTests()
        {
            var testPlace1 = new Place { PlaceId = 1, FourSquareLink = "https://foursquare.com/v/u-karla/4c1f3003b4e62d7fb244df93", Name = "U Karla" };
            var testPlace2 = new Place { PlaceId = 2, FourSquareLink = "https://foursquare.com/v/burger-inn/55a93496498e49f11b0a9532", Name = "Burger Inn" };
            var testTime1 = new TimeAtPlace { Place = testPlace1, Time = new List<DateTime> { DateTime.Now }, TimeAtPlaceId = 1 };
            var testTime2 = new TimeAtPlace { Place = testPlace1, Time = new List<DateTime> { DateTime.Now }, TimeAtPlaceId = 2 };
            var testTime3 = new TimeAtPlace { Place = testPlace2, Time = new List<DateTime> { DateTime.Now }, TimeAtPlaceId = 3 };

            // Seed data
            var data = new List<Event>
            {
               new Event
                {
                    EventId = 1,
                    EventName = "Sraz",
                    EventDescription = "Sraz členů spolku",
                    TimesAtPlaces = new List<TimeAtPlace> { testTime1, testTime2 },
                    Votes = new List<Vote>()
                },
               new Event
                {
                    EventId = 2,
                    EventName = "Sraz 2",
                    EventDescription = "Sraz členů jiného spolku",
                    TimesAtPlaces = new List<TimeAtPlace> { testTime3 },
                    Votes = new List<Vote>()
                }
            }.AsQueryable();

            var mockSet = new Mock<DbSet<Event>>();
            mockSet.As<IDbAsyncEnumerable<Event>>()
                .Setup(m => m.GetAsyncEnumerator())
                .Returns(new TestDbAsyncEnumerator<Event>(data.GetEnumerator()));
            mockSet.As<IQueryable<Event>>()
                .Setup(m => m.Provider)
                .Returns(new TestDbAsyncQueryProvider<Event>(data.Provider));
            mockSet.As<IQueryable<Event>>().Setup(m => m.Expression).Returns(data.Expression);
            mockSet.As<IQueryable<Event>>().Setup(m => m.ElementType).Returns(data.ElementType);
            mockSet.As<IQueryable<Event>>().Setup(m => m.GetEnumerator()).Returns(data.GetEnumerator());

            var mockContext = new Mock<EventPlannerContext>();
            mockContext.Setup(c => c.Events).Returns(mockSet.Object);

            _eventsRepository = new EventsRepository(mockContext.Object);
        }

        [Fact]
        public async void GetAllEvents_Async()
        {
            var events = await _eventsRepository.GetAllEvents();
            var eventsList = events.ToList();

            Assert.Equal(2, events.Count());
            Assert.Equal("Sraz", eventsList[0].EventName);
            Assert.Equal("Sraz 2", eventsList[1].EventName);
            Assert.Equal("Sraz členů spolku", eventsList[0].EventDescription);
            Assert.Equal("Sraz členů jiného spolku", eventsList[1].EventDescription);
        }

        [Fact]
        public async void GetSingleEvent_Async()
        {
            var singleEvent = await _eventsRepository.GetSingleEvent(2);

            Assert.Equal("Sraz 2", singleEvent.EventName);
            Assert.Equal(1, singleEvent.TimesAtPlaces.Count);

            var timesAtPlaces = singleEvent.TimesAtPlaces.ToList();
            Assert.NotNull(timesAtPlaces[0].Place);
            Assert.Equal("Burger Inn", timesAtPlaces[0].Place.Name);
        }

        [Fact]
        public async void GetSingleEvent_Nonexisting()
        {
            var singleEvent = await _eventsRepository.GetSingleEvent(10);

            Assert.Null(singleEvent);
        }
    }
}