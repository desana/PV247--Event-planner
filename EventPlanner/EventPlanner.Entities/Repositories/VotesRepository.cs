using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EventPlanner.Entities;

namespace EventPlanner.Repositories
{
    public class VotesRepository : IVotesRepository
    {
        private readonly EventPlannerDbContext _context;

        public VotesRepository(EventPlannerDbContext context)
        {
            _context = context;
        }


        public async Task<Vote> GetSingleVote(int id)
        {
            var vote = await _context
                .Votes
                .Where(v => v.VoteId == id)
                .SingleOrDefaultAsync();

            return vote;
        }

        public async Task<IEnumerable<Vote>> GetAllVotes()
        {
            var allVotes = await _context.Votes.ToArrayAsync();
            return allVotes;
        }

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
