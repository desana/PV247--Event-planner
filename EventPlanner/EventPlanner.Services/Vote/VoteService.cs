using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EventPlanner.DTO.Vote;
using EventPlanner.Entities.Repositories;
using EventPlanner.Services.Event;

namespace EventPlanner.Services.Vote
{
    public class VoteService : IVoteService
    {
        private readonly IVoteRepository _voteRepository;
        private readonly IEventService _eventService;

        public VoteService(IVoteRepository voteRepository, IEventService eventService)
        {
            _voteRepository = voteRepository;
            _eventService = eventService;
        }

        /// <summary>
        /// Saves vote session to the database.
        /// </summary>
        /// <param name="session">Model of the session to be saved.</param>
        /// <returns>Saved session.</returns>
        public async Task<VoteSession> SaveVoteSession(VoteSession session)
        {
            if (session == null)
            {
                throw new ArgumentNullException(nameof(session));
            }
            session.LastModified = DateTime.Now;

            return await _voteRepository.SaveSession(session);
        }

        /// <summary>
        /// Gets vote session according to <paramref name="sessionId"/>.
        /// </summary>
        /// <param name="sessionId">Id of session to get.</param>
        /// <returns>Session model with <paramref name="sessionId"/>.</returns>
        public async Task<VoteSession> GetVoteSession(Guid sessionId)
        {
            var session = await _voteRepository.GetVoteSession(sessionId);
            if (session == null)
            {
                return null;
            }

            var @event = await _eventService.GetSingleEvent(session.EventId);
            if (@event == null)
            {
                return null;
            }

            DecorateSessionForEvent(session, @event);
            return session;
        }

        /// <summary>
        /// Creates new session. Sets event ID and session ID.
        /// </summary>
        /// <param name="event">Id of the event.</param>
        /// <returns>Newly added vote session.</returns>
        public VoteSession InitializeVoteSession(DTO.Event.Event @event)
        {
            var newSession =
                new VoteSession
                {
                    EventId = @event.EventId,
                    VoteSessionId = Guid.NewGuid(),
                };

            DecorateSessionForEvent(newSession, @event);
            return newSession;
        }

        /// <summary>
        /// Gets all vote sessions for event.
        /// </summary>
        /// <param name="eventId">Event for which the session will be returned.</param>
        /// <returns>List of vote sessions.</returns>
        public async Task<IList<VoteSession>> GetVoteSessions(Guid eventId)
        {
            return await _voteRepository.GetVoteSessions(eventId);
        }

        /// <summary>
        /// Returns all votes for timeslot.
        /// </summary>
        /// <param name="timeAtPlaceId">If of the timeslot.</param>
        /// <returns>All votes for timeslot.</returns>
        public async Task<IList<DTO.Vote.Vote>> GetVotes(int timeAtPlaceId)
        {
            return await _voteRepository.GetVotes(new[] { timeAtPlaceId });
        }

        /// <summary>
        /// Returns votes grouped by <see cref="DTO.Event.TimeAtPlace"/>.
        /// </summary>
        public async Task<IDictionary<int, IList<DTO.Vote.Vote>>> GetVotes(ICollection<int> timeAtPlaceIds)
        {
            var votes = await _voteRepository.GetVotes(timeAtPlaceIds);

            return votes
                .GroupBy(v => v.TimeAtPlaceId)
                .ToDictionary(g => g.Key, g => (IList<DTO.Vote.Vote>)g.ToList());
        }

        private void DecorateSessionForEvent(VoteSession session, DTO.Event.Event @event)
        {
            var missingTimes = @event
                .Places
                .SelectMany(p => p.Times)
                .Where(t => session.Votes
                .All(v => v.TimeAtPlaceId != t.Id));

            foreach (var timeAtPlace in missingTimes)
            {
                session.Votes.Add(
                    new DTO.Vote.Vote
                    {
                        VoteId = Guid.NewGuid(),
                        TimeAtPlaceId = timeAtPlace.Id,
                        Value = VoteValueEnum.Decline,
                    });
            }
        }
    }
}
