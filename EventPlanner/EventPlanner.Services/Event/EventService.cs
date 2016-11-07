using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using EventPlanner.Repositories;
using EventPlanner.Services.DataTransferModels.Generators;
using EventPlanner.Services.DataTransferModels.Models;

namespace EventPlanner.Services.Event
{
    public class EventService : IEventService
    {
        private readonly IMapper _mapper;
        private readonly IEventsRepository _eventRepository;

        public EventService(IMapper mapper, IEventsRepository eventRepository)
        {
            _mapper = mapper;
            _eventRepository = eventRepository;
        }

        /// <summary>
        /// Returns all events from the database.
        /// </summary>
        public async Task<IEnumerable<EventTransferModel>> GetAllEvents()
        {
            IEnumerable<Entities.Event> dataAccessEventModel = await _eventRepository.GetAllEvents();
            return _mapper.Map<IEnumerable<Entities.Event>, IEnumerable<EventTransferModel>>(dataAccessEventModel);
        }

        /// <summary>
        /// Adds event to the database.
        /// </summary>
        /// <param name="newEvent">Event to be added.</param>
        public async Task<EventTransferModel> AddEvent(EventTransferModel newEvent)
        {
            newEvent.GenerateUniqueLink();

            Entities.Event dataAccessEventModel = _mapper.Map<Entities.Event>(newEvent);
            dataAccessEventModel = await _eventRepository.AddEvent(dataAccessEventModel);

            return _mapper.Map<EventTransferModel>(dataAccessEventModel);
        }

        /// <summary>
        /// Returns single event from the database.
        /// </summary>
        /// <param name="name">Name of requested event.</param>
        public async Task<EventTransferModel> GetSingleEvent(string name)
        {
            Entities.Event dataAccessEventModel = await _eventRepository.GetSingleEvent(name);
            return _mapper.Map<EventTransferModel>(dataAccessEventModel);
        }

        /// <summary>
        /// Returns single event from the database.
        /// </summary>
        /// <param name="id">Id of requested event.</param>
        public async Task<EventTransferModel> GetSingleEvent(int id)
        {
            Entities.Event dataAccessEventModel = await _eventRepository.GetSingleEvent(id);
            return _mapper.Map<EventTransferModel>(dataAccessEventModel);
        }

        /// <summary>
        /// Removed event from the database.
        /// </summary>
        /// <param name="id">Id of the event to be removed.</param>
        /// <returns><c>True</c> if event was removed.</returns>
        public async Task<bool> DeleteEvent(int id)
        {
            bool wasRemoved = await _eventRepository.DeleteEvent(id);
            return wasRemoved;
        }
    }
}
