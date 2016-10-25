using System;
using System.Collections.Generic;
using System.Data.Entity;
using EventPlanner.Repositories;
using Xunit;
using Moq;

namespace EventPlanner.Entities.UnitTests
{
    public class EventsRepositoryTests
    {
        private Mock<EventPlannerDbContext> _mockContext;

        public EventsRepositoryTests()
        {
            _mockContext = new Mock<EventPlannerDbContext>();
        }

    }
}