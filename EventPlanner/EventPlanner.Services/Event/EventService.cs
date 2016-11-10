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
        private readonly ITimeAtPlaceRepository _timePlaceRepository;

        public EventService(IMapper mapper, IEventsRepository eventRepository, ITimeAtPlaceRepository timePlaceRepository)
        {
            _mapper = mapper;
            _eventRepository = eventRepository;
            _timePlaceRepository = timePlaceRepository;
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

        /// <summary>
        /// Adds place to an event. 
        /// Adds records to <see cref="PlaceTransferModel"/> and <see cref="TimeAtPlaceTransferModel"/> accordingly.
        /// </summary>
        /// <param name="targetEvent"><see cref="EventTransferModel"/> which will be updated.</param>
        /// <param name="foursquareId">Foursquare ID of the new <see cref="PlaceTransferModel"/>.</param>
        /// <returns>Id of the newly created place.</returns>
        public async Task<int> AddEventPlace(EventTransferModel targetEvent, int foursquareId)
        {
            var atPlaceTransferModel= new TimeAtPlaceTransferModel()
            {
                Place = new PlaceTransferModel()
                {
                    PlaceId = foursquareId
                }
            };

            var timeAtPlaceEntity = await _timePlaceRepository
                .AddTimeAtPlace(_mapper.Map<Entities.TimeAtPlace>(atPlaceTransferModel));
               
            var timeAtPlaceId = timeAtPlaceEntity.TimeAtPlaceId;

            // This is ugly

            var wasAdded = await _eventRepository.AddTimeAtPlace(targetEvent.EventId, timeAtPlaceId);
            
            return timeAtPlaceId;
        }

        /// <summary>
        /// Adds time to <see cref="TimeAtPlaceTransferModel"/>.
        /// </summary>
        /// <param name="targetEvent"><see cref="EventTransferModel"/> which will be updated.</param>
        /// <param name="targetPlace"><see cref="PlaceTransferModel"/> to which the time belongs to.</param>
        public async Task<bool> AddEventTime(EventTransferModel targetEvent, int targetPlace)
        {
            throw new System.NotImplementedException();
        }
    }
}
