using System.Collections.Generic;
using System.Threading.Tasks;
using EventPlanner.Entities;

namespace EventPlanner.Repositories
{
    public interface IEventsRepository
    {
        Task<PlannedEvent> GetSingleEvent(int id);

        Task<IEnumerable<PlannedEvent>> GetAllEvent();

        Task<Vote> AddEvent(PlannedEvent plannedEvent);
    }
}