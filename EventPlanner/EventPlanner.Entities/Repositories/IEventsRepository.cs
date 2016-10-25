using System.Collections.Generic;
using System.Threading.Tasks;
using EventPlanner.Entities;

namespace EventPlanner.Repositories
{
    public interface IEventsRepository
    {
        Task<Event> GetSingleEvent(int id);

        Task<IEnumerable<Event>> GetAllEvents();

        Task<Event> AddEvent(Event plannedEvent);

        Task<bool> DeleteEvent(int id);
    }
}