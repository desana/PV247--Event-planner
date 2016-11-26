using System.Collections.Generic;
using System.Threading.Tasks;
using EventPlanner.Entities;
using Microsoft.Extensions.Options;
using System.Data.Entity;

namespace EventPlanner.Repositories
{
    public class TimeAtPlaceRepository : ITimeAtPlaceRepository
    {
        private readonly EventPlannerContext _context;

        public TimeAtPlaceRepository(EventPlannerContext context)
        {
            _context = context;
        }

        /// <summary>
        /// Default repository constructor that uses connection string provided by top most level configuration
        /// </summary>
        public TimeAtPlaceRepository(IOptions<Configuration.ConnectionOptions> options)
            : this(new EventPlannerContext(options.Value.ConnectionString))
        {
        }

        public Task<TimeAtPlace> GetSingleTimeAtPlace(int id)
        {
            throw new System.NotImplementedException();
        }

        public async Task<IEnumerable<TimeAtPlace>> GetAllTimeAtPlaces()
        {
            return await _context
                .TimesAtPlaces
                .ToArrayAsync();
        }

        public async Task<TimeAtPlace> AddTimeAtPlace(TimeAtPlace timeSlot)
        {
            _context
                .Places
                .Add(timeSlot.Place);

            var addedTimeAtPlace =  _context
                .TimesAtPlaces
                .Add(timeSlot);

            await _context.SaveChangesAsync();
            return addedTimeAtPlace;
        }

        public Task<bool> DeleteTimeAtPlace(int id)
        {
            throw new System.NotImplementedException();
        }
    }
}