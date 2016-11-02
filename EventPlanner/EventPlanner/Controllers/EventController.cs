using System.Threading.Tasks;
using AutoMapper;
using EventPlanner.Models;
using EventPlanner.Services;
using EventPlanner.Services.Event;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling MVC for empty projects, visit http://go.microsoft.com/fwlink/?LinkID=397860

namespace EventPlanner.Controllers
{
    public class EventController : Controller
    {
        private readonly EventService _eventService = new EventService();
        private IMapper _mapper;
        private string currentEventName;

        // GET: /<controller>/
        public IActionResult CreateEvent()
        {
            return View();
        }
        
        public async Task<IActionResult> CreateNewEvent(EventViewModel newEvent)
        {
            if (!ModelState.IsValid)
            {
                return View("CreateEvent");
            }

            currentEventName = newEvent.EventName;
            await _eventService.AddEvent(_mapper.Map<Services.DataTransferModels.Event>(newEvent));
            return RedirectToAction("AddPlaces");
        }

        public IActionResult AddPlaces()
        {
            return View(_eventService.GetSingleEvent(currentEventName));
        }
    }
}

