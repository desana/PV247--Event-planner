using System.Collections.Generic;
using System.Threading.Tasks;
using EventPlanner.Entities;

namespace EventPlanner.Repositories
{
    public interface IEventsRepository
    {
        /// <summary>
        /// Returns single event from the database.
        /// </summary>
        /// <param name="id">Id of requested event.</param>
        Task<Event> GetSingleEvent(int id);

        /// <summary>
        /// Returns single event from the database.
        /// </summary>
        /// <param name="name">Name of requested event.</param>
        Task<Event> GetSingleEvent(string name);

        /// <summary>
        /// Returns all events from the database.
        /// </summary>
        Task<IEnumerable<Event>> GetAllEvents();

        /// <summary>
        /// Adds event to the database.
        /// </summary>
        /// <param name="plannedEvent">Event to be added.</param>
        Task<Event> AddEvent(Event plannedEvent);

        /// <summary>
        /// Removed event from the database.
        /// </summary>
        /// <param name="id">Id of the event to be removed.</param>
        /// <returns><c>True</c> if event was removed.</returns>
        Task<bool> DeleteEvent(int id);

        /// <summary>
        /// Adds <see cref="TimeAtPlace"/> to the event.
        /// </summary>
        /// <param name="eventId">Id of the event.</param>
        /// <param name="timeAtPlaceId"><see cref="TimeAtPlace"/> to add.</param>
        /// <returns><c>True</c> if operation was succesfull.</returns>
        Task<bool> AddTimeAtPlace(int eventId, int timeAtPlaceId);
    }
}