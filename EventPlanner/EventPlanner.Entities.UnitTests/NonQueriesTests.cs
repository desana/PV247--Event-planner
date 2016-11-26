using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using AutoMapper;
using EventPlanner.DTO.Event;
using EventPlanner.Entities.Configuration;
using EventPlanner.Entities.Repositories;
using Xunit;
using Moq;

namespace EventPlanner.Entities.UnitTests
{
    public class NonQueriesTests
    {
        private readonly Mock<EventPlannerContext> _mockContext;
        private readonly IMapper _mapper;

        public NonQueriesTests()
        {
            _mockContext = new Mock<EventPlannerContext>();
            _mapper = new MapperConfiguration(cfg =>
            {
                EntitiesMapperConfiguration.InitialializeMappings(cfg);

            }).CreateMapper();
        }

        [Fact]
        public async void AddEvent_Saves_Event_Via_Context()
        {
            var mockSet = new Mock<DbSet<Entities.Event>>();
            _mockContext.Setup(m => m.Events).Returns(mockSet.Object);
            var repository = new EventsRepository(_mockContext.Object, _mapper);

            var testEvent = new Event
            {
                Id = 1,
                Name = "Sraz",
                Description = "Sraz členů spolku",
                Places = new List<Place>
                {
                    new Place
                    {
                        Id = 1,
                        FourSquareLink = "https://foursquare.com/v/u-karla/4c1f3003b4e62d7fb244df93",
                        Name = "U Karla",
                        Times = new List<TimeAtPlace>
                        {
                            new TimeAtPlace { Time = DateTime.Now, Id = 1},
                        }
                    }
                }
            };
            await repository.AddEvent(testEvent);

            mockSet.Verify(m => m.Add(It.IsAny<Entities.Event>()), Times.Once());
            _mockContext.Verify(m => m.SaveChangesAsync(), Times.Once());
        }

        [Fact]
        public async void AddEvent_If_Null_Throws_Exception()
        {
            var mockSet = new Mock<DbSet<Entities.Event>>();
            _mockContext.Setup(m => m.Events).Returns(mockSet.Object);
            var repository = new EventsRepository(_mockContext.Object, _mapper);
            
            await Assert.ThrowsAsync<ArgumentNullException>(() => repository.AddEvent(null));
        }


        [Fact]
        public async void DeleteEvent_Deletes_Event_Via_Context()
        {
            // Seed data
            var data = new List<Entities.Event>
            {
                new Entities.Event
                {
                    Id = 3,
                    Name = "Sraz",
                    Description = "Sraz členů spolku",
                    Places = new List<Entities.Place>
                    {
                        new Entities.Place
                        {
                            Id = 1,
                            FourSquareLink = "https://foursquare.com/v/u-karla/4c1f3003b4e62d7fb244df93",
                            Name = "U Karla",
                            Times = new List<Entities.TimeAtPlace>
                            {
                                new Entities.TimeAtPlace { Time = DateTime.Now, Id = 1 }
                            }
                        }
                    }
                }
            }.AsQueryable();

            var mockSet = new Mock<DbSet<Entities.Event>>();
            mockSet.As<IDbAsyncEnumerable<Entities.Event>>()
               .Setup(m => m.GetAsyncEnumerator())
               .Returns(new TestDbAsyncEnumerator<Entities.Event>(data.GetEnumerator()));
            mockSet.As<IQueryable<Entities.Event>>()
                .Setup(m => m.Provider)
                .Returns(new TestDbAsyncQueryProvider<Entities.Event>(data.Provider));
            mockSet.As<IQueryable<Entities.Event>>().Setup(m => m.Expression).Returns(data.Expression);
            mockSet.As<IQueryable<Entities.Event>>().Setup(m => m.ElementType).Returns(data.ElementType);
            mockSet.As<IQueryable<Entities.Event>>().Setup(m => m.GetEnumerator()).Returns(data.GetEnumerator());

            _mockContext.Setup(m => m.Events).Returns(mockSet.Object);
            var repository = new EventsRepository(_mockContext.Object, _mapper);

            bool result = await repository.DeleteEvent(3);

            Assert.True(result);
            mockSet.Verify(m => m.Remove(It.IsAny<Entities.Event>()), Times.Once());
            _mockContext.Verify(m => m.SaveChangesAsync(), Times.Once());
        }

        [Fact]
        public async void DeleteEvent_Delete_Nonexisting_Event_Via_Context()
        {
            // Seed data
            var data = new List<Entities.Event>().AsQueryable();

            var mockSet = new Mock<DbSet<Entities.Event>>();
            mockSet.As<IDbAsyncEnumerable<Entities.Event>>()
               .Setup(m => m.GetAsyncEnumerator())
               .Returns(new TestDbAsyncEnumerator<Entities.Event>(data.GetEnumerator()));
            mockSet.As<IQueryable<Entities.Event>>()
                .Setup(m => m.Provider)
                .Returns(new TestDbAsyncQueryProvider<Entities.Event>(data.Provider));
            mockSet.As<IQueryable<Entities.Event>>().Setup(m => m.Expression).Returns(data.Expression);
            mockSet.As<IQueryable<Entities.Event>>().Setup(m => m.ElementType).Returns(data.ElementType);
            mockSet.As<IQueryable<Entities.Event>>().Setup(m => m.GetEnumerator()).Returns(data.GetEnumerator());

            _mockContext.Setup(m => m.Events).Returns(mockSet.Object);
            var repository = new EventsRepository(_mockContext.Object, _mapper);

            bool result = await repository.DeleteEvent(1);

            Assert.False(result);
            mockSet.Verify(m => m.Remove(It.IsAny<Entities.Event>()), Times.Never());
            _mockContext.Verify(m => m.SaveChangesAsync(), Times.Never());
        }
    }
}