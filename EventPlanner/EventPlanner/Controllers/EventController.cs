using System.Threading.Tasks;
using AutoMapper;
using EventPlanner.Models;
using EventPlanner.Services;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling MVC for empty projects, visit http://go.microsoft.com/fwlink/?LinkID=397860

namespace EventPlanner.Controllers
{
    public class EventController : Controller
    {
        private readonly EventService _eventService = new EventService();
        private IMapper _mapper;

        // GET: /<controller>/
        public IActionResult CreateEvent()
        {
            return View();
        }
        
        public async Task<IActionResult> CreateNewEvent(CreateEventViewModel newEvent)
        {
            if (!ModelState.IsValid)
            {
                return View("CreateEvent");
            }

            await _eventService.AddEvent(_mapper.Map<Services.DataTransferModels.EventItem>(newEvent));
            return RedirectToAction("AddPlaces");
        }

        public IActionResult AddPlaces()
        {
            return View();
        }
    }
}

