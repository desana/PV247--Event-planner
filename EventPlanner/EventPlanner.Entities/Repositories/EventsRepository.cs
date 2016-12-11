using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using EventPlanner.Configuration;
using EventPlanner.Entities.Entities;
using Microsoft.Extensions.Options;

namespace EventPlanner.Entities.Repositories
{
    public class EventsRepository : IEventsRepository
    {
        private readonly EventPlannerContext _context;
        private readonly IMapper _mapper;

        internal EventsRepository(EventPlannerContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        /// <summary>
        /// Default repository constructor that uses connection string provided by top most level configuration
        /// </summary>
        public EventsRepository(IOptions<ConnectionOptions> options, IMapper mapper)
            : this(new EventPlannerContext(options.Value.ConnectionString), mapper)
        {
        }

        /// <summary>
        /// Returns single event from the database.
        /// </summary>
        /// <param name="id">Id of requested event.</param>
        public async Task<DTO.Event.Event> GetSingleEvent(Guid id)
        {
            var plannedEvent = await _context
                .Events
                .Where(ev => ev.EventId == id)
                .Include(ev => ev.Places)
                .Include(ev => ev.Places.Select(p => p.Times))
                .SingleOrDefaultAsync();

            return _mapper.Map<DTO.Event.Event>(plannedEvent);
        }

        /// <summary>
        /// Returns all events from the database.
        /// </summary>
        public async Task<IList<DTO.Event.EventListItem>> GetAllEvents()
        {
            var allEvents = await _context.Events.ToListAsync();
            return _mapper.Map<IList<DTO.Event.EventListItem>>(allEvents);
        }

        /// <summary>
        /// Adds event to the database.
        /// </summary>
        /// <param name="plannedEvent">Event to be added.</param>
        public async Task<DTO.Event.Event> AddEvent(DTO.Event.Event plannedEvent)
        {
            if (plannedEvent == null)
                throw new ArgumentNullException(nameof(plannedEvent));

            var addedEvent = _context.Events.Add(_mapper.Map<Event>(plannedEvent));
            await _context.SaveChangesAsync();
            return _mapper.Map<DTO.Event.Event>(addedEvent);
        }

        /// <summary>
        /// Removed event from the database.
        /// </summary>
        /// <param name="id">Id of the event to be removed.</param>
        /// <returns><c>True</c> if event was removed.</returns>
        public async Task<bool> DeleteEvent(Guid id)
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

        /// <summary>
        /// Saves event to the database.
        /// </summary>
        /// <param name="event">Event to be saved.</param>
        /// <returns>Saved event.</returns>
        public async Task<DTO.Event.Event> SaveEvent(DTO.Event.Event @event)
        {
            if (@event == null)
                throw new ArgumentNullException(nameof(@event));

            var entity = _mapper.Map<Event>(@event);
            var existing = await _context.Events.Where(e => e.EventId == @event.EventId)
                .Include(ev => ev.Places)
                .Include(ev => ev.Places.Select(p => p.Times))
                .FirstOrDefaultAsync();

            if (existing == null)
                return null;

            _context.Entry(existing).CurrentValues.SetValues(entity);
            TrackPlacesChanges(existing, entity);

            await _context.SaveChangesAsync();

            return _mapper.Map<DTO.Event.Event>(entity);
        }

        private void TrackPlacesChanges(Event existingEvent, Event newEvent)
        {
            // Remove
            var placesToRemove = existingEvent.Places
                .Where(existingEventPlace => newEvent.Places.All(p => p.Id != existingEventPlace.Id))
                .ToList();

            placesToRemove.ForEach(p => _context.Places.Remove(p));

            foreach (var newEventPlace in newEvent.Places)
            {
                var existingPlace = existingEvent.Places.FirstOrDefault(p => p.Id == newEventPlace.Id);
                if (existingPlace == null)
                {
                    // Insert
                    existingEvent.Places.Add(newEventPlace);
                }
                else
                {
                    // Update
                    newEventPlace.EventId = existingPlace.EventId;
                    _context.Entry(existingPlace).CurrentValues.SetValues(newEventPlace);
                    TrackTimeChanges(existingPlace, newEventPlace);
                }
            }
        }

        private void TrackTimeChanges(Place existingPlace, Place newPlace)
        {
            // Remove
            var timesToRemove = existingPlace.Times
                .Where(existingPlaceTime => newPlace.Times.All(t => t.Id != existingPlaceTime.Id))
                .ToList();

            timesToRemove.ForEach(t => _context.TimesAtPlaces.Remove(t));

            foreach (var newPlaceTime in newPlace.Times)
            {
                var existingTime = existingPlace.Times.FirstOrDefault(t => t.Id == newPlaceTime.Id);
                if (existingTime == null)
                {
                    // Insert
                    existingPlace.Times.Add(newPlaceTime);
                }
                else
                {
                    // Update
                    newPlaceTime.PlaceId = existingTime.PlaceId;
                    _context.Entry(existingTime).CurrentValues.SetValues(newPlaceTime);
                }
            }
        }
    }
}