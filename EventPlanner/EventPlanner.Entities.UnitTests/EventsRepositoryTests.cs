using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using AutoMapper;
using EventPlanner.Entities.Configuration;
using EventPlanner.Entities.Entities;
using EventPlanner.Entities.Repositories;
using Xunit;
using Moq;

namespace EventPlanner.Entities.UnitTests
{
    public class EventsRepositoryTests
    {
        private readonly EventsRepository _eventsRepository;

        public EventsRepositoryTests()
        {
            var testPlace1 = new Place
            {
                Id = 1,
                FourSquareLink = "https://foursquare.com/v/u-karla/4c1f3003b4e62d7fb244df93",
                Name = "U Karla",
                Times = new List<TimeAtPlace>
                {
                    new TimeAtPlace { Time = DateTime.Now , Id = 1 },
                    new TimeAtPlace { Time = DateTime.Now , Id = 2 }
                }
            };
            var testPlace2 = new Place
            {
                Id = 2,
                FourSquareLink = "https://foursquare.com/v/burger-inn/55a93496498e49f11b0a9532",
                Name = "Burger Inn", 
                Times = new List<TimeAtPlace>
                {
                    new TimeAtPlace { Time = DateTime.Now, Id = 3 }
                }
            };

            // Seed data
            var data = new List<Event>
            {
               new Event
                {
                    EventId = Guid.NewGuid(),
                    Name = "Sraz",
                    Description = "Sraz členů spolku",
                    Places = new List<Place> { testPlace1 }
                },
               new Event
                {
                    EventId = Guid.Parse("90a79b82-d4f9-4bff-bbb7-1cd50a35cdc0"),
                    Name = "Sraz 2",
                    Description = "Sraz členů jiného spolku",
                    Places = new List<Place> { testPlace2 }
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

            var mapper = new MapperConfiguration(EntitiesMapperConfiguration.InitialializeMappings).CreateMapper();

            _eventsRepository = new EventsRepository(mockContext.Object, mapper);
        }

        [Fact]
        public async void GetAllEvents_Async()
        {
            var events = await _eventsRepository.GetAllEvents();
            var eventsList = events.ToList();

            Assert.Equal(2, events.Count);
            Assert.Equal("Sraz", eventsList[0].Name);
            Assert.Equal("Sraz 2", eventsList[1].Name);
            Assert.Equal("Sraz členů spolku", eventsList[0].Description);
            Assert.Equal("Sraz členů jiného spolku", eventsList[1].Description);
        }

        [Fact]
        public async void GetSingleEvent_Async()
        {
            var singleEvent = await _eventsRepository.GetSingleEvent(Guid.Parse("90a79b82-d4f9-4bff-bbb7-1cd50a35cdc0"));

            Assert.Equal("Sraz 2", singleEvent.Name);
            Assert.Equal("Burger Inn", singleEvent.Places.Single().Name);
            Assert.Equal(1, singleEvent.Places.Single().Times.Count);
        }

        [Fact]
        public async void GetSingleEvent_Nonexisting()
        {
            var singleEvent = await _eventsRepository.GetSingleEvent(Guid.NewGuid());

            Assert.Null(singleEvent);
        }

        [Fact]
        public async void GetSingleEvent2_Nonexisting()
        {
            var singleEvent = await _eventsRepository.GetSingleEvent(Guid.NewGuid());

            Assert.Null(singleEvent);
        }
    }
}