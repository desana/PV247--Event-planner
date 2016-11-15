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

        /// <summary>
        /// Adds place to an event. 
        /// Adds records to <see cref="PlaceTransferModel"/> and <see cref="TimeAtPlaceTransferModel"/> accordingly.
        /// </summary>
        /// <param name="targetEvent"><see cref="EventTransferModel"/> which will be updated.</param>
        /// <param name="foursquareId">Foursquare ID of the new <see cref="PlaceTransferModel"/>.</param>
        /// <returns>Id of the newly created place.</returns>
        Task<int> AddEventPlace(EventTransferModel targetEvent, int foursquareId);

        /// <summary>
        /// Adds time to <see cref="TimeAtPlaceTransferModel"/>.
        /// </summary>
        /// <param name="targetEvent"><see cref="EventTransferModel"/> which will be updated.</param>
        /// <param name="targetPlace"><see cref="PlaceTransferModel"/> to which the time belongs to.</param>
        Task<bool> AddEventTime(EventTransferModel targetEvent, int targetPlace);

        /// <summary>
        /// Get the name of the event.
        /// </summary>
        /// <param name="id">Id of event.</param>
        /// <returns>Event name.</returns>
        Task<string> GetEventName(int id);

        /// <summary>
        /// Gets all timeAtPlaces for given event from the database.
        /// </summary>
        /// <param name="id">Id of the event.</param>
        /// <returns>All timeAtPlaces for the event.</returns>
        Task<IEnumerable<TimeAtPlaceTransferModel>> GetTimeAtPlacesForEvent(int id);

        Task<object> GetVotesForEvent(int id);
    }
}
