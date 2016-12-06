using System;
using System.Collections.Generic;
using System.Data;
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

        public async Task<VoteSession> SaveVoteSession(VoteSession session)
        {
            if (session == null)
                throw new ArgumentNullException(nameof(session));

            session.LastModified = DateTime.Now;

            return await _voteRepository.SaveSession(session);       
        }

        public async Task<VoteSession> GetVoteSession(Guid sessionId)
        {
            var session = await _voteRepository.GetVoteSession(sessionId);
            if (session == null)
                return null;

            var @event = await _eventService.GetSingleEvent(session.EventId);
            if (@event == null)
                return null;

            DecorateSessionForEvent(session, @event);
            return session;
        }

        public VoteSession InitializeVoteSession(DTO.Event.Event @event)
        {
            var newSession = new VoteSession
            {
                EventId = @event.EventId,
                VoteSessionId = Guid.NewGuid(),
            };

            DecorateSessionForEvent(newSession, @event);
            return newSession;
        }

        public void DecorateSessionForEvent(VoteSession session, DTO.Event.Event @event)
        {
            var missingTimes = @event.Places.SelectMany(p => p.Times)
                .Where(t => session.Votes
                .All(v => v.TimeAtPlaceId != t.Id));

            foreach (var timeAtPlace in missingTimes)
            {
                session.Votes.Add(new DTO.Vote.Vote
                {
                    VoteId = new Guid(),
                    TimeAtPlaceId = timeAtPlace.Id,
                    Value = VoteValueEnum.Decline,
                });
            }
        }

        public async Task<IList<VoteSession>> GetVoteSessions(Guid eventId)
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
