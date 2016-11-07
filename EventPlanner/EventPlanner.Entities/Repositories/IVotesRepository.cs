using System.Collections.Generic;
using System.Threading.Tasks;
using EventPlanner.Entities;

namespace EventPlanner.Repositories
{
    public interface IVotesRepository
    {
        /// <summary>
        /// Returns single vote from the database.
        /// </summary>
        /// <param name="id">Id of requested vote.</param>
        Task<Vote> GetSingleVote(int id);

        /// <summary>
        /// Returns all votes from the database.
        /// </summary>
        Task<IEnumerable<Vote>> GetAllVotes();

        /// <summary>
        /// Adds a vote to the database.
        /// </summary>
        /// <param name="vote">Vote to be added.</param>
        Task<Vote> AddVote(Vote vote);
    }
}