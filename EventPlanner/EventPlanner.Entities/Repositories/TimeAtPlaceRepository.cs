using System.Collections.Generic;
using System.Threading.Tasks;
using EventPlanner.Entities;
using Microsoft.Extensions.Options;

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

        public Task<IEnumerable<TimeAtPlace>> GetAllTimeAtPlaces()
        {
            throw new System.NotImplementedException();
        }

        public Task<TimeAtPlace> AddTimeAtPlace(TimeAtPlace timeSlot)
        {
            throw new System.NotImplementedException();
        }

        public Task<bool> DeleteTimeAtPlace(int id)
        {
            throw new System.NotImplementedException();
        }
    }
}