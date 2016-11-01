using System;
using EventPlanner.Services.DataTransferModels;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using EventPlanner.Repositories;

namespace EventPlanner.Services
{
    public class EventService
    {
        private IMapper _mapper;
        private IEventsRepository _eventRepository;

        public async Task<EventItem[]> GetAllEvents()
        {
            // _logger.LogInformation("Starting Game service method");

            IEnumerable<Entities.Event> dataAccessEventModel = await _eventRepository.GetAllEvents();
            return _mapper
                .Map<IEnumerable<Entities.Event>, IEnumerable<EventItem>>(dataAccessEventModel)
                .ToArray();
        }


        public async Task<DataTransferModels.Event> AddEvent(DataTransferModels.EventItem @event, string developerCodeName)
        {
            Entities.Event dataAccessEventModel = _mapper.Map<Entities.Event>(@event);
            dataAccessEventModel = await _eventRepository.AddEvent(dataAccessEventModel);

            return _mapper.Map<DataTransferModels.Event>(dataAccessEventModel);
        }

    }
}
