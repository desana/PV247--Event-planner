using System;
using System.Collections.Generic;
using System.Data.Entity;
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

        /// <summary>
        /// Saves current session to the database.
        /// </summary>
        /// <param name="session">Session to be saved.</param>
        /// <returns>Saved session.</returns>
        public async Task<DTO.Vote.VoteSession> SaveSession(DTO.Vote.VoteSession session)
        {
            if (session == null)
                throw new ArgumentNullException(nameof(session));

            var entity = _mapper.Map<VoteSession>(session);
            var existing = await _context.VoteSessions
                .Include(s => s.Votes)
                .FirstOrDefaultAsync(s => s.VoteSessionId == entity.VoteSessionId);

            if (existing != null)
            {
                _context.Entry(existing).CurrentValues.SetValues(entity);
                TrackVoteChanges(existing, entity);
            }
            else
            {
                _context.VoteSessions.Add(entity);
            }

            await _context.SaveChangesAsync();

            return _mapper.Map<DTO.Vote.VoteSession>(entity);
        }

        /// <summary>
        /// Returns vote session.
        /// </summary>
        /// <param name="sessionId">Id of session to be returned.</param>
        /// <returns>Session with <paramref name="sessionId"/>"/></returns>
        public async Task<DTO.Vote.VoteSession> GetVoteSession(Guid sessionId)
        {
            var entity = await _context.VoteSessions
                .Where(s => s.VoteSessionId == sessionId)
                .Include(s => s.Votes)
                .FirstOrDefaultAsync();

            if (entity == null)
                return null;

            return _mapper.Map<DTO.Vote.VoteSession>(entity);
        }

        /// <summary>
        /// Returns all vote sessions for event.
        /// </summary>
        /// <param name="eventId">Event which sessions will be returned.</param>
        /// <returns>Sessions for <paramref name="eventId"/></returns>
        public async Task<IList<DTO.Vote.VoteSession>> GetVoteSessions(Guid eventId)
        {
            var entity = await _context.VoteSessions
                .Where(s => s.EventId == eventId)
                .Include(s => s.Votes)
                .ToListAsync();

            return _mapper.Map<IList<DTO.Vote.VoteSession>>(entity);
        }

        /// <summary>
        /// Returns all votes for timeslots.
        /// </summary>
        /// <param name="timeAtPlaceIds">Timeslots.</param>
        /// <returns>All votes for timeslots.</returns>
        public async Task<IList<DTO.Vote.Vote>> GetVotes(ICollection<int> timeAtPlaceIds)
        {
            var query = from vote in _context.Votes where timeAtPlaceIds.Contains(vote.TimeAtPlaceId) select vote;
            var entity = await query.ToListAsync();
            return _mapper.Map<IList<DTO.Vote.Vote>>(entity);
        }

        private void TrackVoteChanges(VoteSession existingSession, VoteSession newSession)
        {
            // Remove
            var votesToRemove = existingSession.Votes
                .Where(existingSessionVote => newSession.Votes.All(v => v.VoteId != existingSessionVote.VoteId))
                .ToList();

            votesToRemove.ForEach(p => _context.Votes.Remove(p));

            foreach (var newSessionVote in newSession.Votes)
            {
                var existingVote = existingSession.Votes.FirstOrDefault(v => v.VoteId == newSessionVote.VoteId);
                if (existingVote == null)
                {
                    // Insert
                    existingSession.Votes.Add(newSessionVote);
                }
                else
                {
                    // Update
                    _context.Entry(existingVote).CurrentValues.SetValues(newSessionVote);
                }
            }
        }
    }
}
