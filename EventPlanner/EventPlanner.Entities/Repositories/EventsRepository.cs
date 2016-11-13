using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Threading.Tasks;
using EventPlanner.Entities;
using System.Linq;
using EventPlanner.Configuration;
using Microsoft.Extensions.Options;

namespace EventPlanner.Repositories
{
    public class EventsRepository : IEventsRepository
    {
        private readonly EventPlannerContext _context;

        public EventsRepository(EventPlannerContext context)
        {
            _context = context;
        }

        /// <summary>
        /// Default repository constructor that uses connection string provided by top most level configuration
        /// </summary>
        public EventsRepository(IOptions<ConnectionOptions> options)
            : this(new EventPlannerContext(options.Value.ConnectionString))
        {
        }

        /// <summary>
        /// Returns single event from the database.
        /// </summary>
        /// <param name="id">Id of requested event.</param>
        public async Task<Event> GetSingleEvent(int id)
        {
            var plannedEvent = await _context
                .Events
                .Where(ev => ev.EventId == id)
                .Include(ev => ev.Places)
                .Include(ev => ev.TimesAtPlaces)
                .Include(ev => ev.Votes)
                .SingleOrDefaultAsync();

            return plannedEvent;
        }

        /// <summary>
        /// Returns single event from the database.
        /// </summary>
        /// <param name="name">Name of requested event.</param>
        public async Task<Event> GetSingleEvent(string name)
        {
            var plannedEvent = await _context
                .Events
                .Where(ev => ev.EventName == name)
                .SingleOrDefaultAsync();

            return plannedEvent;
        }

        /// <summary>
        /// Returns all events from the database.
        /// </summary>
        public async Task<IEnumerable<Event>> GetAllEvents()
        {
            var allEvents = await _context.Events.ToArrayAsync();
            return allEvents;
        }

        /// <summary>
        /// Adds event to the database.
        /// </summary>
        /// <param name="plannedEvent">Event to be added.</param>
        public async Task<Event> AddEvent(Event plannedEvent)
        {
            if (plannedEvent == null)
                throw new ArgumentNullException(nameof(plannedEvent));

            var addedEvent = _context.Events.Add(plannedEvent);
            await _context.SaveChangesAsync();
            return addedEvent;
        }

        /// <summary>
        /// Removed event from the database.
        /// </summary>
        /// <param name="id">Id of the event to be removed.</param>
        /// <returns><c>True</c> if event was removed.</returns>
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

        public async Task<IEnumerable<TimeAtPlace>> GetTimeAtPlacesForEvent(int id)
        {
            var timesAtPlaces = await _context.TimesAtPlaces
               .Where(tp => tp.Event.EventId == id).ToListAsync();
            return timesAtPlaces;
        }

        public async Task<IEnumerable<Vote>> GetAllVotesForEvent(int id)
        {
            var votes = await _context.Votes
                .Where(v => v.Event.EventId == id).ToListAsync();
            return votes;
        }
    }
}