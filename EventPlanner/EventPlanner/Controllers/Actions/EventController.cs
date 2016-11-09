
using System.Threading.Tasks;
using AutoMapper;
using EventPlanner.Models;
using EventPlanner.Services.DataTransferModels.Models;
using EventPlanner.Services.Event;
using Microsoft.AspNetCore.Mvc;

namespace EventPlanner.Controllers
{
    public partial class EventController
    {
        private readonly IEventService _eventService;
        private readonly IMapper _mapper;

        public EventController(IMapper mapper, IEventService eventService)
        {
            _mapper = mapper;
            _eventService = eventService;
        }

        /// <summary>
        /// Creates new event and sets <see cref="EventViewModel.EventName"/> and <see cref="EventViewModel.EventDescription"/>.
        /// It also generates unique <see cref="EventViewModel.EventLink"/>.
        /// </summary>
        /// <param name="newEvent"> <see cref="EventViewModel"/> containing data from user.</param>
        /// <returns>Redirect to <see cref="AddPlaces"/> page. </returns>
        public async Task<IActionResult> CreateNewEvent(EventViewModel newEvent)
        {
            if (!ModelState.IsValid)
            {
                return View("CreateEvent");
            }

            var savedEvent = await _eventService.AddEvent(_mapper.Map<EventTransferModel>(newEvent));
            TempData["event"] = _mapper.Map<EventViewModel>(savedEvent);

            return RedirectToAction("AddPlaces");
        }

        /// <summary>
        /// Adds <c>DateTime</c> value to <see cref="TimeAtPlaceViewModel"/> of corresponding <see cref="EventViewModel"/>.
        /// </summary>
        /// <param name="currentevent">Currently processed event.</param>
        /// <returns>Redirect to <see cref="AddPlaces"/> page with updated data.</returns>
        public async Task<IActionResult> AddCurrentTime(EventViewModel currentevent)
        {
            if (!ModelState.IsValid)
            {
                return View("AddPlaces");
            }

            var savedEvent = await _eventService
               .AddEventTime(_mapper.Map<EventTransferModel>(currentevent), currentevent.CurrentPlace);

            TempData["event"] = _mapper.Map<EventViewModel>(savedEvent);
            return RedirectToAction("AddPlaces");
        }

        /// <summary>
        /// Adds instance of <see cref="PlaceViewModel"/> to the database 
        /// and creates corresponding links with <see cref="TimeAtPlaceViewModel"/> and <see cref="EventViewModel"/>.
        /// </summary>
        /// <param name="currentevent">Currently processed event.</param>
        /// <returns>Redirect to <see cref="AddPlaces"/> page with updated data.</returns>
        public async Task<IActionResult> AddCurrentPlace(EventViewModel currentevent)
        {
            if (!ModelState.IsValid)
            {
                return View("AddPlaces");
            }

            var savedEvent = await _eventService
                .AddEventPlace(_mapper.Map<EventTransferModel>(currentevent), currentevent.CurrentPlaceFoursquareId);

            TempData["event"] = _mapper.Map<EventViewModel>(savedEvent);
            return RedirectToAction("AddPlaces");
        }
     }
}
