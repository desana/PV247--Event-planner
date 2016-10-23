using System.Collections.Generic;
using System.Threading.Tasks;
using EventPlanner.Entities;

namespace EventPlanner.Repositories
{
    public interface IVotesRepository
    {
        Task<Vote> GetSingleVote(int id);

        Task<IEnumerable<Vote>> GetAllVotes();

        Task<Vote> AddVote(Vote vote);
    }
}