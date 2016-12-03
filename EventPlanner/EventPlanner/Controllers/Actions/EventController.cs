using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using EventPlanner.Models;
using EventPlanner.Services.Event;
using Microsoft.AspNetCore.Mvc;
using System.Linq;
using System;
using EventPlanner.DTO.Event;
using EventPlanner.DTO.Vote;
using EventPlanner.Services.Vote;
using FoursquareVenuesService.Services;
using Microsoft.AspNetCore.Mvc.Rendering;
using Newtonsoft.Json;

namespace EventPlanner.Controllers
{
    public partial class EventController
    {
        private readonly IEventService _eventService;
        private readonly IVoteService _voteService;
        private readonly IMapper _mapper;
        private readonly IFoursquareService _fsService;

        public EventController(IMapper mapper, IEventService eventService, IVoteService voteService, IFoursquareService foursquareService)
        {
            _mapper = mapper;
            _eventService = eventService;
            _voteService = voteService;
            _fsService = foursquareService;
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
            
            var savedEvent = await _eventService.AddEvent(_mapper.Map<Event>(newEvent));
            return RedirectToAction("AddPlaces", new { eventId = savedEvent.Id });
        }

        /// <summary>
        /// Adds <c>DateTime</c> value to <see cref="TimeAtPlaceViewModel"/> of corresponding <see cref="EventViewModel"/>.
        /// </summary>
        /// <param name="targetEvent">Currently processed event.</param>
        /// <returns>Redirect to <see cref="AddPlaces"/> page with updated data.</returns>
        [HttpPost]
        public async Task<IActionResult> AddSingleTime(EventViewModel targetEvent)
        {
            // TODO
            //if (!ModelState.IsValid)
            //{   
            //    return RedirectToAction("AddPlaces", new { eventId = targetEvent.EventId});
            //}

            var currentTimeAtPlaceId = await _eventService.AddEventTime(targetEvent.EventId, targetEvent.CurrentPlaceFoursquareId, targetEvent.CurrentTime);
            return RedirectToAction("AddPlaces", new { eventId = targetEvent.EventId, place = targetEvent.CurrentPlaceFoursquareId });
        }

        /// <summary>
        /// Action result for ajax call to populate drop down list with places.
        /// </summary>
        /// <param name="placeName">Name of the place.</param>
        /// <param name="placeCity">City of the place.</param>
        /// <returns>Json object with places.</returns>
        [HttpPost]
        public async Task<IActionResult> GetPlaces(string placeName, string placeCity)
        {
            if (String.IsNullOrEmpty(placeName) || String.IsNullOrEmpty(placeCity))
                return Json(new List<string>());

            var places = await _fsService.SearchVenuesAsync(placeName, placeCity, 10);  //TODO how many results do we want?  
            foreach (var place in places)
            {
                place.PhotoUrl = await _fsService.GetVenuePhotoUrlAsync(place.Id, "36x36");
                place.Text = placeName;
            }        
            return Json(places);
        }


        /// <summary>
        /// Adds instance of <see cref="PlaceViewModel"/> to the database 
        /// and creates corresponding links with <see cref="TimeAtPlaceViewModel"/> and <see cref="EventViewModel"/>.
        /// </summary>
        /// <param name="targetEvent">Currently processed event.</param>
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

            var tralala = await _eventService.AddEventPlace(targetEvent.EventId, targetEvent.CurrentPlaceFoursquareId);
            return RedirectToAction("AddPlaces", new { eventId = targetEvent.EventId, place = targetEvent.CurrentPlaceFoursquareId });
        }

        /// <summary>
        /// Gets json object that will be used for rendering charts.
        /// </summary>
        /// <param name="id">Event id.</param>
        /// <returns>Chart model.</returns>
        private async Task<ChartModel> GetChartModel(int id)
        {
            var chartModel = new ChartModel();
            // NOTE: we do not display places and times witch zero votes
            var @event = await _eventService.GetSingleEvent(id);
            // TODO Use vote service to read votes
            var voteSessions = await _voteService.GetVoteSessions(@event.Id);
            var data = new Dictionary<string, int>();
            foreach (var place in @event.Places)
            {
                foreach (var time in place.Times)
                {
                    var value = voteSessions.SelectMany(voteSession => voteSession.Votes)
                        .Count(vote => vote.TimeAtPlaceId == time.Id && vote.Value == VoteValueEnum.Accept);
                    data.Add(place.Name + " - " + time.Time.ToString("dd/MM/yyyy H:mm"), value);
                }
            }

            var sortedData = data.OrderBy(k => k.Key);

            foreach (var pair in sortedData)
            {
                chartModel.Categories.Add(pair.Key);
                chartModel.Data.Add(pair.Value);
            }

            return chartModel;
        }
    }
}

