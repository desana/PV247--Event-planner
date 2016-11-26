using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using EventPlanner.Models;
using EventPlanner.Services.DataTransferModels.Models;
using EventPlanner.Services.Event;
using Microsoft.AspNetCore.Mvc;
using System.Linq;
using System;

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
        [HttpPost]
        public async Task<IActionResult> CreateNewEvent(EventViewModel newEvent)
        {
            if (!ModelState.IsValid)
            {
                return RedirectToAction("CreateEvent");
            }
            
            var savedEvent = await _eventService.AddEvent(_mapper.Map<EventTransferModel>(newEvent));
            return RedirectToAction("AddPlaces", new { eventId = savedEvent.Id });
        }

        /// <summary>
        /// Adds <c>DateTime</c> value to <see cref="TimeAtPlaceViewModel"/> of corresponding <see cref="EventViewModel"/>.
        /// </summary>
        /// <param name="currentevent">Currently processed event.</param>
        /// <returns>Redirect to <see cref="AddPlaces"/> page with updated data.</returns>
        public async Task<IActionResult> AddSingleTime(int eventId, string foursquareId, DateTime time)
        {
            if (!ModelState.IsValid)
            {   
                return RedirectToAction("AddPlaces", new { eventId = eventId});
            }

            var modifiedEventId = await _eventService.AddEventTime(eventId, foursquareId, time);            
            return RedirectToAction("AddPlaces", new { eventId = modifiedEventId, foursquareId = foursquareId });
        }

        /// <summary>
        /// Adds instance of <see cref="PlaceViewModel"/> to the database 
        /// and creates corresponding links with <see cref="TimeAtPlaceViewModel"/> and <see cref="EventViewModel"/>.
        /// </summary>
        /// <param name="eventId">Id of currently processed event.</param>
        /// <param name="foursquareId"> Foursquare id of place to be added.</param>
        /// <returns>Redirect to <see cref="AddPlaces"/> page with updated data.</returns>
        [HttpPost]
        public async Task<IActionResult> AddSinglePlace(EventViewModel targetEvent)
        {
            // TODO
            /* if (!ModelState.IsValid)
            {
                return RedirectToAction("AddPlaces", new { eventId = targetEvent.EventId });
            }
            */

            var currentTimeAtPlaceId = await _eventService.AddEventPlace(targetEvent.EventId, targetEvent.CurrentPlaceFoursquareId);
            return RedirectToAction("AddPlaces", new { eventId = targetEvent.EventId, place = currentTimeAtPlaceId });
        }


        /// <summary>
        /// Gets json object that will be used for rendering charts.
        /// </summary>
        /// <param name="id">Event id.</param>
        /// <returns>Json object.</returns>
        private async Task<ChartModel> GetChartModel(int id)
        {
            var chartModel = new ChartModel();
            // NOTE: we do not display places and times witch zero votes
            var votes = _mapper.Map<IEnumerable<TimeAtPlaceViewModel>>(await _eventService.GetTimeAtPlacesForEvent(id));
            var data = votes.ToDictionary(
                vote => vote.Place.Name + " - " + vote.Time.ToString("dd/MM/yyyy H:mm"),
                vote => vote.Votes).OrderBy(k => k.Key);

            foreach (var pair in data)
            {
                chartModel.Categories.Add(pair.Key);
                chartModel.Data.Add(pair.Value);
            }

            return chartModel;
        }
    }
}
