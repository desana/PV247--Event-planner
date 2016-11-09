using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using EventPlanner.Entities;

namespace EventPlanner.Repositories
{
    public interface ITimeAtPlaceRepository
    {
        /// <summary>
        /// Returns single <see cref="TimeAtPlace"/> from the database.
        /// </summary>
        /// <param name="id">Id of requested event.</param>
        Task<TimeAtPlace> GetSingleTimeAtPlace(int id);

        /// <summary>
        /// Returns all <see cref="TimeAtPlace"/>s from the database.
        /// </summary>
        Task<IEnumerable<TimeAtPlace>> GetAllTimeAtPlaces();

        /// <summary>
        /// Adds <see cref="TimeAtPlace"/> to the database.
        /// </summary>
        /// <param name="timeSlot">Event to be added.</param>
        Task<TimeAtPlace> AddTimeAtPlace(TimeAtPlace timeSlot);

        /// <summary>
        /// Removes <see cref="TimeAtPlace"/> from the database.
        /// </summary>
        /// <param name="id">Id of the event to be removed.</param>
        /// <returns><c>True</c> if event was removed.</returns>
        Task<bool> DeleteTimeAtPlace(int id);
    }
}
