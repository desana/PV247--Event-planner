using System.Collections.Generic;
using System.Threading.Tasks;
using EventPlanner.Entities;

namespace EventPlanner.Repositories
{
    public interface IEventsRepository
    {
        Task<Event> GetSingleEvent(int id);

        Task<IEnumerable<Event>> GetAllEvent();

        Task<Event> AddEvent(Event plannedEvent);

        Task<bool> DeleteEvent(int id);
    }
}