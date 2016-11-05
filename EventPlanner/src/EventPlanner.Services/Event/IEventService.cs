using System.Collections.Generic;
using System.Threading.Tasks;

namespace EventPlanner.Services.Event
{
    public interface IEventService
    {
        Task<IEnumerable<DataTransferModels.Event>> GetAllEvents();

        Task<DataTransferModels.Event> AddEvent(DataTransferModels.Event newEvent);

        Task<DataTransferModels.Event> GetSingleEvent(string eventName);

        Task<DataTransferModels.Event> GetSingleEvent(int id);

        Task<bool> DeleteEvent(int id);
    }
}
