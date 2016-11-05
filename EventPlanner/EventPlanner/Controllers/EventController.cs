using System.Linq;
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
        private readonly IEventService _eventService;
        private readonly IMapper _mapper;

        public EventController(IMapper mapper, IEventService eventService)
        {
            _mapper = mapper;
            _eventService = eventService;
        }

        // GET: /<controller>/
        public IActionResult CreateEvent()
        {
            return View();
        }

        public IActionResult AddPlaces()
        {
            return View();
        }

        /// <summary>
        /// Creates new event and sets <see cref="EventViewModel.EventName"/> and <see cref="EventViewModel.EventDescription"/>.
        /// It also generates unique <see cref="EventViewModel.EventLink"/>.
        /// </summary>
        /// <param name="newEvent"> <see cref="EventViewModel"/> containing data from user.</param>
        public async Task<IActionResult> CreateNewEvent(EventViewModel newEvent)
        {
            if (!ModelState.IsValid)
            {
                return View("CreateEvent");
            }
            
            await _eventService.AddEvent(_mapper.Map<Services.DataTransferModels.Event>(newEvent));
            return RedirectToAction("AddPlaces");
        }
    }
}

