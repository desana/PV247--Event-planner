using System;
using System.Collections.Generic;
using System.Threading.Tasks;


namespace EventPlanner.Services.Event
{
    public interface IEventService
    {
        /// <summary>
        /// Returns all events from the database.
        /// </summary>
        Task<IList<DTO.Event.EventListItem>> GetAllEvents();

        /// <summary>
        /// Adds event to the database.
        /// </summary>
        /// <param name="newEvent">Event to be added.</param>
        Task<DTO.Event.Event> AddEvent(DTO.Event.Event newEvent);

        /// <summary>
        /// Returns single event from the database.
        /// </summary>
        /// <param name="id">Id of requested event.</param>
        Task<DTO.Event.Event> GetSingleEvent(Guid id);

        /// <summary>
        /// Removed event from the database.
        /// </summary>
        /// <param name="id">Id of the event to be removed.</param>
        /// <returns><c>True</c> if event was removed.</returns>
        Task<bool> DeleteEvent(Guid id);

        /// <summary>
        /// Adds place to an event. 
        /// Adds records to <see cref="DTO.Event.Place"/> and <see cref="DTO.Event.TimeAtPlace"/> accordingly.
        /// </summary>
        /// <param name="eventId">Id of <see cref="Event"/> which will be updated.</param>
        /// <param name="foursquareId">Foursquare ID of the new <see cref="DTO.Event.Place"/>.</param>
        /// <returns>Id of the newly created place.</returns>
        Task<DTO.Event.Event> AddEventPlace(Guid eventId, string foursquareId);

        /// <summary>
        /// Adds time to <see cref="DTO.Event.TimeAtPlace"/>.
        /// </summary>
        /// <param name="targetEvent">ID of the<see cref="Event"/> which will be updated.</param>
        /// <param name="foursquareId">The foursquare Id of place to which the time belongs to.</param>
        /// <param name="time">New timeslot.</param>
        Task<DTO.Event.Event> AddEventTime(Guid targetEvent, string foursquareId, DateTime time);

        /// <summary>
        /// Get the name of the event.
        /// </summary>
        /// <param name="id">Id of event.</param>
        /// <returns>Event name.</returns>
        Task<string> GetEventName(Guid id);

        /// <summary>
        /// Saves event.
        /// </summary>
        /// <param name="event">Event to save.</param>
        /// <returns>Saved event.</returns>
        Task<DTO.Event.Event> SaveEvent(DTO.Event.Event @event);
    }
}
