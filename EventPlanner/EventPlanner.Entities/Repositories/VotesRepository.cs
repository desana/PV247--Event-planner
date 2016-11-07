using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using EventPlanner.Entities;

namespace EventPlanner.Repositories
{
    public class VotesRepository : IVotesRepository
    {
        private readonly EventPlannerContext _context;

        public VotesRepository(EventPlannerContext context)
        {
            _context = context;
        }

        /// <summary>
        /// Returns single vote from the database.
        /// </summary>
        /// <param name="id">Id of requested vote.</param>
        public async Task<Vote> GetSingleVote(int id)
        {
            var vote = await _context
                .Votes
                .Where(v => v.VoteId == id)
                .SingleOrDefaultAsync();

            return vote;
        }

        /// <summary>
        /// Returns all votes from the database.
        /// </summary>
        public async Task<IEnumerable<Vote>> GetAllVotes()
        {
            var allVotes = await _context.Votes.ToArrayAsync();
            return allVotes;
        }

        /// <summary>
        /// Adds a vote to the database.
        /// </summary>
        /// <param name="vote">Vote to be added.</param>
        public async Task<Vote> AddVote(Vote vote)
        {
            if (vote == null)
                throw new ArgumentNullException(nameof(vote));

            var addedVote = _context.Votes.Add(vote);
            await _context.SaveChangesAsync();
            return addedVote;
        }
    }
}
