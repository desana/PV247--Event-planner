using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using EventPlanner.DTO.Vote;

namespace EventPlanner.Services.Vote
{
    public interface IVoteService
    {
        /// <summary>
        /// Saves vote session to the databsase.
        /// </summary>
        /// <param name="session">Model of the session to be saved.</param>
        /// <returns>Saved session.</returns>
        Task<VoteSession> SaveVoteSession(VoteSession session);

        /// <summary>
        /// Gets vote session according to <paramref name="sessionId"/>.
        /// </summary>
        /// <param name="sessionId">Id of session to get.</param>
        /// <returns>Session model with <paramref name="sessionId"/>.</returns>
        Task<VoteSession> GetVoteSession(Guid sessionId);

        /// <summary>
        /// Gets all vote sessions for event.
        /// </summary>
        /// <param name="eventId">Event for which the session will be returned.</param>
        /// <returns>List of vote sessions.</returns>
        Task<IList<VoteSession>> GetVoteSessions(Guid eventId);

        /// <summary>
        /// Creates new session. Sets event ID and session ID.
        /// </summary>
        /// <param name="event">Id of the event.</param>
        /// <returns>Newly added vote session.</returns>
        VoteSession InitializeVoteSession(DTO.Event.Event @event);

        /// <summary>
        /// Returns all votes for timeslot.
        /// </summary>
        /// <param name="timeAtPlaceId">If of the timeslot.</param>
        /// <returns>All votes for timeslot.</returns>
        Task<IList<DTO.Vote.Vote>> GetVotes(int timeAtPlaceId);

        /// <summary>
        /// Returns votes grouped by <see cref="DTO.Event.TimeAtPlace"/>.
        /// </summary>
        Task<IDictionary<int, IList<DTO.Vote.Vote>>> GetVotes(ICollection<int> timeAtPlaceIds);
    }
}