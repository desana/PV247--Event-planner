using System;
using System.Collections.Generic;
using System.Data.Entity;
using EventPlanner.Repositories;
using Xunit;
using Moq;

namespace EventPlanner.Entities.UnitTests
{
    public class NonQueriesTests
    {
        private Mock<EventPlannerDbContext> _mockContext;

        public NonQueriesTests()
        {
            _mockContext = new Mock<EventPlannerDbContext>();
        }

        [Fact]
        public async void AddEvent_Saves_Event_Via_Context()
        {
            var mockSet = new Mock<DbSet<Event>>();

            _mockContext.Setup(m => m.Events).Returns(mockSet.Object);

            var repository = new EventsRepository(_mockContext.Object);
            var testPlace = new Place() { PlaceId = 1, FourSquareLink = "https://foursquare.com/v/u-karla/4c1f3003b4e62d7fb244df93", Name = "U Karla" };
            var testTime = new TimeAtPlace() { Place = testPlace, Time = DateTime.Now, TimeAtPlaceId = 1 };
            var testEvent = new Event()
            {
                EventId = 1,
                TimesAtPlaces = new List<TimeAtPlace> { testTime },
                Votes = new List<Vote>() 
            };
            await repository.AddEvent(testEvent);

            mockSet.Verify(m => m.Add(It.IsAny<Event>()), Times.Once());
            _mockContext.Verify(m => m.SaveChangesAsync(), Times.Once());
        }

        [Fact]
        public async void AddVote_Saves_Vote_Via_Context()
        {
            var mockSet = new Mock<DbSet<Vote>>();

            _mockContext.Setup(m => m.Votes).Returns(mockSet.Object);

            var repository = new VotesRepository(_mockContext.Object);
            var testPlace = new Place { PlaceId = 1, FourSquareLink = "https://foursquare.com/v/u-karla/4c1f3003b4e62d7fb244df93", Name = "U Karla" };
            var testTime = new TimeAtPlace { Place = testPlace, Time = DateTime.Now, TimeAtPlaceId = 1 };
            var testVote = new Vote
            {
                VoteId = 1, TimeAtPlace = testTime, VoteLink = "", Votes = 4
            };
            await repository.AddVote(testVote);

            mockSet.Verify(m => m.Add(It.IsAny<Vote>()), Times.Once());
            _mockContext.Verify(m => m.SaveChangesAsync(), Times.Once());
        }
    }
}