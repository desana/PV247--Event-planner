using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Migrations;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using EventPlanner.Configuration;
using EventPlanner.Entities.Entities;
using Microsoft.Extensions.Options;

namespace EventPlanner.Entities.Repositories
{
    public class VoteRepository : IVoteRepository
    {
        private readonly EventPlannerContext _context;
        private readonly IMapper _mapper;

        internal VoteRepository(EventPlannerContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        /// <summary>
        /// Default repository constructor that uses connection string provided by top most level configuration
        /// </summary>
        public VoteRepository(IOptions<ConnectionOptions> options, IMapper mapper)
            : this(new EventPlannerContext(options.Value.ConnectionString), mapper)
        {
        }

        public async Task<DTO.Vote.VoteSession> SaveSession(DTO.Vote.VoteSession session)
        {
            if (session == null)
                throw new ArgumentNullException(nameof(session));

            var entity = _mapper.Map<VoteSession>(session);
            _context.VoteSessions.AddOrUpdate(entity);
            await _context.SaveChangesAsync();

            return _mapper.Map<DTO.Vote.VoteSession>(entity);
        }

        public async Task<DTO.Vote.VoteSession> GetVoteSession(Guid sessionId)
        {
            var entity = await _context.VoteSessions.Where(s => s.VoteSessionId == sessionId).FirstOrDefaultAsync();
            if (entity == null)
                return null;

            return _mapper.Map<DTO.Vote.VoteSession>(entity);
        }

        public async Task<IList<DTO.Vote.VoteSession>> GetVoteSessions(int eventId)
        {
            var entity = await _context.VoteSessions.Where(s => s.EventId == eventId).Include(k=>k.Votes).ToListAsync();
            return _mapper.Map<IList<DTO.Vote.VoteSession>>(entity);
        }

        public async Task<IList<DTO.Vote.Vote>> GetVotes(ICollection<int> timeAtPlaceIds)
        {
            var query = from vote in _context.Votes where timeAtPlaceIds.Contains(vote.TimeAtPlaceId) select vote;
            var entity = await query.ToListAsync();
            return _mapper.Map<IList<DTO.Vote.Vote>>(entity);
        }
    }
}
