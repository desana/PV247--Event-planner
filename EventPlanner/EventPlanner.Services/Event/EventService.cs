using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EventPlanner.DTO.Event;
using EventPlanner.Entities.Repositories;
using FoursquareVenuesService.Services;

namespace EventPlanner.Services.Event
{
    public class EventService : IEventService
    {
        private readonly IEventsRepository _eventRepository;
        private readonly IFoursquareService _foursquareService;

        public EventService(IEventsRepository eventRepository, IFoursquareService foursquareService)
        {
            _eventRepository = eventRepository;
            _foursquareService = foursquareService;
        }

        /// <summary>
        /// Returns all events from the database.
        /// </summary>
        public async Task<IList<EventListItem>> GetAllEvents()
        {
            return await _eventRepository.GetAllEvents();
        }

        /// <summary>
        /// Adds event to the database.
        /// </summary>
        /// <param name="newEvent">Event to be added.</param>
        public async Task<DTO.Event.Event> AddEvent(DTO.Event.Event newEvent)
        {
            return await _eventRepository.AddEvent(newEvent);
        }

        /// <summary>
        /// Saves event.
        /// </summary>
        /// <param name="event">Event to save.</param>
        /// <returns>Saved event.</returns>
        public async Task<DTO.Event.Event> SaveEvent(DTO.Event.Event @event)
        {
            return await _eventRepository.SaveEvent(@event);
        }

        /// <summary>
        /// Returns single event from the database.
        /// </summary>
        /// <param name="id">Id of requested event.</param>
        public async Task<DTO.Event.Event> GetSingleEvent(Guid id)
        {
            return await _eventRepository.GetSingleEvent(id);
        }

        /// <summary>
        /// Removed event from the database.
        /// </summary>
        /// <param name="id">Id of the event to be removed.</param>
        /// <returns><c>True</c> if event was removed.</returns>
        public async Task<bool> DeleteEvent(Guid id)
        {
            bool wasRemoved = await _eventRepository.DeleteEvent(id);
            return wasRemoved;
        }

        /// <summary>
        /// Adds place to an event. 
        /// Adds records to <see cref="Place"/> and <see cref="TimeAtPlace"/> accordingly.
        /// </summary>
        /// <param name="eventId">Id of <see cref="DTO.Event.Event">event</see> that will be updated.</param>
        /// <param name="foursquareId">Foursquare ID of the new <see cref="Place"/>.</param>
        /// <returns>Id of the newly created place.</returns>
        public async Task<DTO.Event.Event> AddEventPlace(Guid eventId, string foursquareId)
        {
            var @event = await _eventRepository.GetSingleEvent(eventId);
            if (@event == null)
            {
                return null;
            }

            var fsVenue = await _foursquareService.GetVenueAsync(foursquareId);
            @event.Places.Add(
                new Place
                {
                    FourSquareId = foursquareId,
                    Name = fsVenue.Name
                });

            @event = await _eventRepository.SaveEvent(@event);

            return  @event;
        }

        /// <summary>
        /// Adds time to <see cref="DTO.Event.TimeAtPlace"/>.
        /// </summary>
        /// <param name="targetEvent">ID of the<see cref="Event"/> which will be updated.</param>
        /// <param name="foursquareId">The foursquare Id of place to which the time belongs to.</param>
        /// <param name="time">New timeslot.</param>
        public async Task<DTO.Event.Event> AddEventTime(Guid eventId, string foursquareId, DateTime time)
        {
            var @event = await _eventRepository.GetSingleEvent(eventId);
            var place = @event?
                .Places
                .FirstOrDefault(p => p.FourSquareId == foursquareId);

            if (place == null)
            {
                return null;
            }                

            place.Times.Add(new TimeAtPlace
            {
                Time = time,
            });

            @event = await _eventRepository.SaveEvent(@event);

            return @event;
        }

        /// <summary>
        /// Get the name of the event.
        /// </summary>
        /// <param name="id">Id of event.</param>
        /// <returns>Event name.</returns>
        public async Task<string> GetEventName(Guid id)
        {
            var foundEvent = await _eventRepository.GetSingleEvent(id);
            return foundEvent?.Name;
        }
    }
    
}
