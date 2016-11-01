using EventPlanner.Services.DataTransferModels;
using System.Collections.Generic;

namespace EventPlanner.Services
{
    public class EventService
    {
        private IMapper _mapper;

        public async Task<EventItem[]> GetAllEvents()
        {
            // _logger.LogInformation("Starting Game service method");

            IEnumerable<Event> dataAccessGamesModel = await _storeRepository.GetAllEvents();
            return _mapper
                .Map<IEnumerable<Event>, IEnumerable<DataTransferModels.EventItem>>(dataAccessEventModel)
                .ToArray();
        }

    }
}
