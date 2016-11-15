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
    public class NonQueriesTests
    {
        private readonly Mock<EventPlannerContext> _mockContext;

        public NonQueriesTests()
        {
            _mockContext = new Mock<EventPlannerContext>();
        }

        [Fact]
        public async void AddEvent_Saves_Event_Via_Context()
        {
            var mockSet = new Mock<DbSet<Event>>();
            _mockContext.Setup(m => m.Events).Returns(mockSet.Object);
            var repository = new EventsRepository(_mockContext.Object);

            var testPlace = new Place { Id = 1, FourSquareLink = "https://foursquare.com/v/u-karla/4c1f3003b4e62d7fb244df93", Name = "U Karla" };
            var testTime = new TimeAtPlace { Place = testPlace, Time =  DateTime.Now , Id = 1, Votes = 5};
            var testEvent = new Event
            {
                Id = 1,
                Name = "Sraz",
                Description = "Sraz členů spolku",
                TimesAtPlaces = new List<TimeAtPlace> { testTime }
            };
            await repository.AddEvent(testEvent);

            mockSet.Verify(m => m.Add(It.IsAny<Event>()), Times.Once());
            _mockContext.Verify(m => m.SaveChangesAsync(), Times.Once());
        }

        [Fact]
        public async void AddEvent_If_Null_Throws_Exception()
        {
            var mockSet = new Mock<DbSet<Event>>();
            _mockContext.Setup(m => m.Events).Returns(mockSet.Object);
            var repository = new EventsRepository(_mockContext.Object);
            
            await Assert.ThrowsAsync<ArgumentNullException>(() => repository.AddEvent(null));
        }

        [Fact]
        public async void AddVote_Saves_Vote_Via_Context()
        {
            var mockSet = new Mock<DbSet<TimeAtPlace>>();
            _mockContext.Setup(m => m.TimesAtPlaces).Returns(mockSet.Object);
            var repository = new TimeAtPlaceRepository(_mockContext.Object); 

            var testPlace = new Place { Id = 1, FourSquareLink = "https://foursquare.com/v/u-karla/4c1f3003b4e62d7fb244df93", Name = "U Karla" };
            var testTime = new TimeAtPlace { Place = testPlace, Time = DateTime.Now, Id = 1, Votes = 5};
            
            //await repository.AddVote();

            //mockSet.Verify(m => m.Add(It.IsAny<Vote>()), Times.Once());
            //_mockContext.Verify(m => m.SaveChangesAsync(), Times.Once());
        }

        [Fact]
        public async void AddVote_If_Null_Throws_Exception()
        {
            //var mockSet = new Mock<DbSet<TimeAtPlace>>();
            //_mockContext.Setup(m => m.Votes).Returns(mockSet.Object);
            ////var repository = new VotesRepository(_mockContext.Object);

            //await Assert.ThrowsAsync<ArgumentNullException>(() => repository.AddVote(null));
        }

        [Fact]
        public async void DeleteEvent_Deletes_Event_Via_Context()
        {
            var testPlace = new Place { Id = 1, FourSquareLink = "https://foursquare.com/v/u-karla/4c1f3003b4e62d7fb244df93", Name = "U Karla" };
            var testTime = new TimeAtPlace { Place = testPlace, Time = DateTime.Now, Id = 1 };
            // Seed data
            var data = new List<Event>
            {
                new Event
                {
                    Id = 3,
                    Name = "Sraz",
                    Description = "Sraz členů spolku",
                    TimesAtPlaces = new List<TimeAtPlace> {testTime}
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

            _mockContext.Setup(m => m.Events).Returns(mockSet.Object);
            var repository = new EventsRepository(_mockContext.Object);

            bool result = await repository.DeleteEvent(3);

            Assert.True(result);
            mockSet.Verify(m => m.Remove(It.IsAny<Event>()), Times.Once());
            _mockContext.Verify(m => m.SaveChangesAsync(), Times.Once());
        }

        [Fact]
        public async void DeleteEvent_Delete_Nonexisting_Event_Via_Context()
        {
            // Seed data
            var data = new List<Event>().AsQueryable();

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

            _mockContext.Setup(m => m.Events).Returns(mockSet.Object);
            var repository = new EventsRepository(_mockContext.Object);

            bool result = await repository.DeleteEvent(1);

            Assert.False(result);
            mockSet.Verify(m => m.Remove(It.IsAny<Event>()), Times.Never());
            _mockContext.Verify(m => m.SaveChangesAsync(), Times.Never());
        }
    }
}