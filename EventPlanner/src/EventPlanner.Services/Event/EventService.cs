using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using EventPlanner.Repositories;

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

        public async Task<IEnumerable<DataTransferModels.Event>> GetAllEvents()
        {
            IEnumerable<Entities.Event> dataAccessEventModel = await _eventRepository.GetAllEvents();
            return _mapper.Map<IEnumerable<Entities.Event>, IEnumerable<DataTransferModels.Event>>(dataAccessEventModel);
        }

        public async Task<DataTransferModels.Event> AddEvent(DataTransferModels.Event newEvent)
        {
            Entities.Event dataAccessEventModel = _mapper.Map<Entities.Event>(newEvent);
            dataAccessEventModel = await _eventRepository.AddEvent(dataAccessEventModel);

            return _mapper.Map<DataTransferModels.Event>(dataAccessEventModel);
        }

        public async Task<DataTransferModels.Event> GetSingleEvent(string eventName)
        {
            Entities.Event dataAccessEventModel = await _eventRepository.GetSingleEvent(eventName);
            return _mapper.Map<DataTransferModels.Event>(dataAccessEventModel);
        }
        
        public async Task<DataTransferModels.Event> GetSingleEvent(int id)
        {
            Entities.Event dataAccessEventModel = await _eventRepository.GetSingleEvent(id);
            return _mapper.Map<DataTransferModels.Event>(dataAccessEventModel);
        }
        
        public async Task<bool> DeleteEvent(int id)
        {
            bool wasRemoved = await _eventRepository.DeleteEvent(id);
            return wasRemoved;
        }
    }
}
