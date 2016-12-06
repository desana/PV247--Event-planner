using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using EventPlanner.DTO.Vote;

namespace EventPlanner.Services.Vote
{
    public interface IVoteService
    {
        Task<VoteSession> SaveVoteSession(VoteSession session);
        Task<VoteSession> GetVoteSession(Guid sessionId);
        Task<IList<VoteSession>> GetVoteSessions(Guid eventId);
        VoteSession InitializeVoteSession(DTO.Event.Event @event);
        Task<IList<DTO.Vote.Vote>> GetVotes(int timeAtPlaceId);

        /// <summary>
        /// Returns votes grouped by <see cref="EventPlanner.DTO.Event.TimeAtPlace"/>.
        /// </summary>
        Task<IDictionary<int, IList<DTO.Vote.Vote>>> GetVotes(ICollection<int> timeAtPlaceIds);
    }
}