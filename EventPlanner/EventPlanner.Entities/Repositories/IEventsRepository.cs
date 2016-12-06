using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace EventPlanner.Entities.Repositories
{
    public interface IEventsRepository
    {
        /// <summary>
        /// Returns single event from the database.
        /// </summary>
        /// <param name="id">Id of requested event.</param>
        Task<DTO.Event.Event> GetSingleEvent(Guid id);

        /// <summary>
        /// Returns all events from the database.
        /// </summary>
        Task<IList<DTO.Event.EventListItem>> GetAllEvents();

        /// <summary>
        /// Adds event to the database.
        /// </summary>
        /// <param name="plannedEvent">Event to be added.</param>
        Task<DTO.Event.Event> AddEvent(DTO.Event.Event plannedEvent);

        /// <summary>
        /// Removed event from the database.
        /// </summary>
        /// <param name="id">Id of the event to be removed.</param>
        /// <returns><c>True</c> if event was removed.</returns>
        Task<bool> DeleteEvent(Guid id);

        Task<DTO.Event.Event> SaveEvent(DTO.Event.Event @event);
    }
}