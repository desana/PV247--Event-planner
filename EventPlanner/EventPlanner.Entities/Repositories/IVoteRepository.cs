using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace EventPlanner.Entities.Repositories
{
    public interface IVoteRepository
    {
        Task<DTO.Vote.VoteSession> SaveSession(DTO.Vote.VoteSession session);
        Task<DTO.Vote.VoteSession> GetVoteSession(Guid sessionId);
        Task<IList<DTO.Vote.VoteSession>> GetVoteSessions(int eventId);
        Task<IList<DTO.Vote.Vote>> GetVotes(ICollection<int> timeAtPlaceIds);
    }
}