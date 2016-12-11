using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace EventPlanner.Entities.Repositories
{
    public interface IVoteRepository
    {
        /// <summary>
        /// Saves current session to the database.
        /// </summary>
        /// <param name="session">Session to be saved.</param>
        /// <returns>Saved session.</returns>
        Task<DTO.Vote.VoteSession> SaveSession(DTO.Vote.VoteSession session);

        /// <summary>
        /// Returns vote session.
        /// </summary>
        /// <param name="sessionId">Id of session to be returned.</param>
        /// <returns>Session with <paramref name="sessionId"/>"/></returns>
        Task<DTO.Vote.VoteSession> GetVoteSession(Guid sessionId);

        /// <summary>
        /// Returns all vote sessions for event.
        /// </summary>
        /// <param name="eventId">Event which sessions will be returned.</param>
        /// <returns>Sessions for <paramref name="eventId"/></returns>
        Task<IList<DTO.Vote.VoteSession>> GetVoteSessions(Guid eventId);

        /// <summary>
        /// Returns all votes for timeslots.
        /// </summary>
        /// <param name="timeAtPlaceIds">Timeslots.</param>
        /// <returns>All votes for timeslots.</returns>
        Task<IList<DTO.Vote.Vote>> GetVotes(ICollection<int> timeAtPlaceIds);
    }
}