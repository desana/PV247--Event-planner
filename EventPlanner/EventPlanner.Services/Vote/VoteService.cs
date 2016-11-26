using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EventPlanner.DTO.Vote;
using EventPlanner.Entities.Repositories;

namespace EventPlanner.Services.Vote
{
    public class VoteService : IVoteService
    {
        private readonly IVoteRepository _voteRepository;

        public VoteService(IVoteRepository voteRepository)
        {
            _voteRepository = voteRepository;
        }

        public async Task<VoteSession> SaveVoteSession(VoteSession session)
        {
            if (session == null)
                throw new ArgumentNullException(nameof(session));

            session.LastModified = DateTime.Now;

            return await _voteRepository.SaveSession(session);       
        }

        public async Task<VoteSession> GetVoteSession(Guid sessionId)
        {
            return await _voteRepository.GetVoteSession(sessionId);
        }

        public async Task<IList<VoteSession>> GetVoteSessions(int eventId)
        {
            return await _voteRepository.GetVoteSessions(eventId);
        }

        public async Task<IList<DTO.Vote.Vote>> GetVotes(int timeAtPlaceId)
        {
            return await _voteRepository.GetVotes(new [] { timeAtPlaceId });
        }

        /// <summary>
        /// Returns votes grouped by <see cref="EventPlanner.DTO.Event.TimeAtPlace"/>.
        /// </summary>
        public async Task<IDictionary<int, IList<DTO.Vote.Vote>>> GetVotes(ICollection<int> timeAtPlaceIds)
        {
            var votes = await _voteRepository.GetVotes(timeAtPlaceIds);

            return votes.GroupBy(v => v.TimeAtPlaceId).ToDictionary(g => g.Key, g => (IList<DTO.Vote.Vote>)g.ToList());
        }
    }
}
