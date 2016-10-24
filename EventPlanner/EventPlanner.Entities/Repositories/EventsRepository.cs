using System.Collections.Generic;
using System.Threading.Tasks;
using EventPlanner.Entities;

namespace EventPlanner.Repositories
{
    public class EventsRepository : IEventsRepository
    {
        public Task<PlannedEvent> GetSingleEvent(int id)
        {
            throw new System.NotImplementedException();
        }

        public Task<IEnumerable<PlannedEvent>> GetAllEvent()
        {
            throw new System.NotImplementedException();
        }

        public Task<Vote> AddEvent(PlannedEvent plannedEvent)
        {
            throw new System.NotImplementedException();
        }
    }
}