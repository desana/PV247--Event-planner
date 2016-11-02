using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Threading.Tasks;
using EventPlanner.Entities;
using System.Linq;

namespace EventPlanner.Repositories
{
    public class EventsRepository : IEventsRepository
    {
        private readonly EventPlannerDbContext _context;

        public EventsRepository(EventPlannerDbContext context)
        {
            _context = context;
        }

        public async Task<Event> GetSingleEvent(int id)
        {
            var plannedEvent = await _context
                .Events
                .Where(ev => ev.EventId == id)
                .SingleOrDefaultAsync();

            return plannedEvent;
        }


        public async Task<Event> GetSingleEvent(string name)
        {
            var plannedEvent = await _context
                .Events
                .Where(ev => ev.EventName == name)
                .SingleOrDefaultAsync();

            return plannedEvent;
        }

        public async Task<IEnumerable<Event>> GetAllEvents()
        {
            var allEvents = await _context.Events.ToArrayAsync();
            return allEvents;
        }

        public async Task<Event> AddEvent(Event plannedEvent)
        {
            if (plannedEvent == null)
                throw new ArgumentNullException(nameof(plannedEvent));

            var addedEvent = _context.Events.Add(plannedEvent);
            await _context.SaveChangesAsync();
            return addedEvent;
        }

        public async Task<bool> DeleteEvent(int id)
        {
            var foundEvent = _context
                .Events
                .FirstOrDefault(ev => ev.EventId == id);

            if (foundEvent == null)
                return false;

            _context.Events.Remove(foundEvent);
            await _context.SaveChangesAsync();

            return true;
        }
    }
}