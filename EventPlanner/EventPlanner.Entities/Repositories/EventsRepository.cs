using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Migrations;
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
        public async Task<DTO.Event.Event> GetSingleEvent(int id)
        {
            var plannedEvent = await _context
                .Events
                .Where(ev => ev.Id == id)
                .Include(ev => ev.Places)
                .Include(ev => ev.Places.Select(p => p.Times))
                .SingleOrDefaultAsync();

            return _mapper.Map<DTO.Event.Event>(plannedEvent);
        }

        /// <summary>
        /// Returns single event from the database.
        /// </summary>
        /// <param name="name">Name of requested event.</param>
        public async Task<DTO.Event.Event> GetSingleEvent(string name)
        {
            var plannedEvent = await _context
                .Events
                .Where(ev => ev.Name == name)
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

        public async Task<DTO.Event.Event> SaveEvent(DTO.Event.Event @event)
        {
            if (@event == null)
                throw new ArgumentNullException(nameof(@event));

            var entity = _mapper.Map<Event>(@event);
            _context.Events.AddOrUpdate(entity);
            await _context.SaveChangesAsync();

            return _mapper.Map<DTO.Event.Event>(entity);
        }
    }
}