using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using EventPlanner.Configuration;
using EventPlanner.Repositories;
using Microsoft.Extensions.Options;

namespace EventPlanner.Entities.Repositories
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
                .Where(ev => ev.Id == id)
                .Include(ev => ev.TimesAtPlaces)
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
                .Where(ev => ev.Name == name)
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
                .FirstOrDefault(ev => ev.Id == id);

            if (foundEvent == null)
                return false;

            _context.Events.Remove(foundEvent);
            await _context.SaveChangesAsync();

            return true;
        }

        /// <summary>
        /// Adds <see cref="TimeAtPlace"/> to the event.
        /// </summary>
        /// <param name="eventId">Id of the event.</param>
        /// <param name="timeAtPlaceId"><see cref="TimeAtPlace"/> to add.</param>
        /// <returns><c>True</c> if operation was succesfull.</returns>
        public async Task<bool> AddTimeAtPlace(int eventId, int timeAtPlaceId)
        {
            var foundEvent = _context
                .Events
                .FirstOrDefault(ev => ev.Id == eventId);

            if (foundEvent == null)
                return false;

            var timeAtPlaceToAdd = _context
                .TimesAtPlaces
                .FirstOrDefault(tp => tp.Id == timeAtPlaceId);

            foundEvent.TimesAtPlaces.Add(timeAtPlaceToAdd);
            await _context.SaveChangesAsync();

            return true;
        }

        public async Task<IEnumerable<TimeAtPlace>> GetTimeAtPlacesForEvent(int id)
        {
            var timesAtPlaces = await _context.TimesAtPlaces
               .Where(tp => tp.Event.Id == id).ToListAsync();
            return timesAtPlaces;
        }

        public async Task<IEnumerable<TimeAtPlace>> GetAllVotesForEvent(int id)
        {
            var votes = await _context.TimesAtPlaces
                .Where(v => v.Event.Id == id).ToListAsync();
            return votes;
        }
    }
}