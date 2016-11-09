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
    public class VotesRepositoryTests
    {
        private readonly VotesRepository _votesRepository;

        public VotesRepositoryTests()
        {
            var testPlace1 = new Place { PlaceId = 1, FourSquareLink = "https://foursquare.com/v/u-karla/4c1f3003b4e62d7fb244df93", Name = "U Karla" };
            var testPlace2 = new Place { PlaceId = 2, FourSquareLink = "https://foursquare.com/v/burger-inn/55a93496498e49f11b0a9532", Name = "Burger Inn" };
            var testTime1 = new TimeAtPlace { Place = testPlace1, Time = new List<DateTime> { DateTime.Now }, TimeAtPlaceId = 1 };
            var testTime2 = new TimeAtPlace { Place = testPlace2, Time = new List<DateTime> { DateTime.Now }, TimeAtPlaceId = 2 };

            // Seed data
            var data = new List<Vote>
            {
               new Vote
                {
                    VoteId = 1,
                    TimeAtPlace = testTime1
                },
               new Vote
                {
                    VoteId = 2,
                    TimeAtPlace = testTime2
                }
            }.AsQueryable();

            var mockSet = new Mock<DbSet<Vote>>();
            mockSet.As<IDbAsyncEnumerable<Vote>>()
                .Setup(m => m.GetAsyncEnumerator())
                .Returns(new TestDbAsyncEnumerator<Vote>(data.GetEnumerator()));
            mockSet.As<IQueryable<Vote>>()
                .Setup(m => m.Provider)
                .Returns(new TestDbAsyncQueryProvider<Vote>(data.Provider));
            mockSet.As<IQueryable<Vote>>().Setup(m => m.Expression).Returns(data.Expression);
            mockSet.As<IQueryable<Vote>>().Setup(m => m.ElementType).Returns(data.ElementType);
            mockSet.As<IQueryable<Vote>>().Setup(m => m.GetEnumerator()).Returns(data.GetEnumerator());

            var mockContext = new Mock<EventPlannerContext>();
            mockContext.Setup(c => c.Votes).Returns(mockSet.Object);

            _votesRepository = new VotesRepository(mockContext.Object);
        }

        [Fact]
        public async void GetAllVotes_Async()
        {
            var votes = await _votesRepository.GetAllVotes();
            var votesList = votes.ToList();

            Assert.Equal(2, votes.Count());
            Assert.Equal("U Karla", votesList[0].TimeAtPlace.Place.Name);
            Assert.Equal("Burger Inn", votesList[1].TimeAtPlace.Place.Name);
        }

        [Fact]
        public async void GetSingleVote_Async()
        {
            var singleVote = await _votesRepository.GetSingleVote(2);

            Assert.Equal("Burger Inn", singleVote.TimeAtPlace.Place.Name);
        }

        [Fact]
        public async void GetSingleVote_Nonexisting()
        {
            var singleVote = await _votesRepository.GetSingleVote(10);

            Assert.Null(singleVote);
        }
    }
}