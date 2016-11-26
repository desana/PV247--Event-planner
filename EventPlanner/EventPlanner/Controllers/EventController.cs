using System.Threading.Tasks;
using EventPlanner.Models;
using Microsoft.AspNetCore.Mvc;

namespace EventPlanner.Controllers
{
    public partial class EventController : Controller
    {
        [HttpGet]
        public IActionResult CreateEvent()
        {
            return View();
        }

        [HttpGet]
        public async Task<IActionResult> AddPlaces(int eventId, string place = "")
        {
            var eventTransferModel = await _eventService.GetSingleEvent(eventId);
            var eventViewModel = _mapper.Map<EventViewModel>(eventTransferModel);
            eventViewModel.CurrentPlaceFoursquareId = place;

            return View(eventViewModel);
        }

        [HttpGet]
        public async Task<IActionResult> Results(int id)
        {
            var requestedEventName = await _eventService.GetEventName(id);

            if (requestedEventName == null)
            {
                //TODO show 404 or something
            }

            ViewData["EventName"] = requestedEventName;
            var chartModel = await GetChartModel(id);
            chartModel.EventName = requestedEventName;

            return View(chartModel);
        }
    }
}

