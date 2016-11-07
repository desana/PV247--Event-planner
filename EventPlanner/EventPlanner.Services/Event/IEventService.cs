using System.Collections.Generic;
using System.Threading.Tasks;
using EventPlanner.Services.DataTransferModels.Models;

namespace EventPlanner.Services.Event
{
    public interface IEventService
    {
        /// <summary>
        /// Returns all events from the database.
        /// </summary>
        Task<IEnumerable<EventTransferModel>> GetAllEvents();

        /// <summary>
        /// Adds event to the database.
        /// </summary>
        /// <param name="newEvent">Event to be added.</param>
        Task<EventTransferModel> AddEvent(EventTransferModel newEvent);

        /// <summary>
        /// Returns single event from the database.
        /// </summary>
        /// <param name="name">Name of requested event.</param>
        Task<EventTransferModel> GetSingleEvent(string name);

        /// <summary>
        /// Returns single event from the database.
        /// </summary>
        /// <param name="id">Id of requested event.</param>
        Task<EventTransferModel> GetSingleEvent(int id);

        /// <summary>
        /// Removed event from the database.
        /// </summary>
        /// <param name="id">Id of the event to be removed.</param>
        /// <returns><c>True</c> if event was removed.</returns>
        Task<bool> DeleteEvent(int id);
    }
}
