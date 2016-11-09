using System.Collections.Generic;
using System.Threading.Tasks;
using EventPlanner.Entities;

namespace EventPlanner.Repositories
{
    public class TimeAtPlaceRepository : ITimeAtPlaceRepository
    {
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