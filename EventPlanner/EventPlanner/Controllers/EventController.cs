using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using AutoMapper;
using EventPlanner.Services.Event;
using EventPlanner.Services.Vote;
using FoursquareVenuesService.Services;
using System.Linq;
using EventPlanner.DTO.Event;
using EventPlanner.DTO.Vote;
using System.Globalization;
using EventPlanner.Models.Event;

namespace EventPlanner.Controllers
{
    public class EventController : Controller
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
        /// Shows page on which event name and description are added.
        /// </summary>
        /// <param name="err">Error message for missing event name.</param>
        /// <returns>Page to add name and description with.</returns>
        [HttpGet]
        public IActionResult CreateEvent(string err = "")
        {
            ViewData["EventName"] = "New event";

            var model = new CreateEventViewModel
            {
                Error = err
            };

            return View(model);
        }

        /// <summary>
        /// Shows page on which the places and times are added.
        /// </summary>
        /// <param name="eventId">Id of current event.</param>
        /// <param name="foursquareId">Foursquare ID of last added place.</param>
        /// <param name="errTime">Error message for time validation.</param>
        /// <param name="errPlace">Error message for place validation.</param>
        /// <returns>Page to add places and times with.</returns>
        [HttpGet]
        public async Task<IActionResult> AddPlaces(Guid eventId, string foursquareId = "", string errTime = "", string errPlace = "")
        {
            ViewData["EventName"] = "New event";

            var eventTransferModel = await _eventService.GetSingleEvent(eventId);
            var eventViewModel = _mapper.Map<AddPlacesViewModel>(eventTransferModel);

            eventViewModel.CurrentPlaceFoursquareId = foursquareId;
            eventViewModel.PlaceErrorMessage = errPlace;
            eventViewModel.TimeErrorMessage = errTime;

            await DecorateWithOpeningHours(eventViewModel);      

            return View(eventViewModel);
        }

        /// <summary>
        /// Decorates places with opening hours.
        /// </summary>
        /// <param name="eventViewModel">Event view model to be decorated.</param>
        /// <returns>Event view model.</returns>
        private async Task DecorateWithOpeningHours(AddPlacesViewModel eventViewModel)
        {
            foreach (var place in eventViewModel.Places)
            {
                if (!string.IsNullOrEmpty(place.OpeningHours))
                {
                    continue;
                }
                var venue = await _fsService.GetVenueAsync(place.FourSquareId);
                if (venue.Hours == null)
                {
                    continue;
                }
                foreach (var timeframe in venue.Hours.Timeframes)
                {
                    place.OpeningHours += timeframe.Days + ": ";
                    foreach (var time in timeframe.Open)
                    {
                        place.OpeningHours += time.ReturnAsInvariantCulture();
                    }
                    place.OpeningHours += "\n";
                }
            }
        }

        /// <summary>
        /// Shows page at the end of event creation.
        /// </summary>
        /// <param name="eventViewModel">Model to show.</param>
        /// <returns>Page with event overview.</returns>
        [HttpPost]
        public async Task<IActionResult> ShowCreatedEvent(EventViewModel eventViewModel)
        {
            var errorMessage = await ValidateBeforeFinish(eventViewModel);
            if (errorMessage != null)
            {
                return RedirectToAction("AddPlaces",
                    new
                    {
                        eventId = eventViewModel.EventId,
                        foursquareId = (await _eventService.GetSingleEvent(eventViewModel.EventId)).Places.Last().FourSquareId,
                        errTime = errorMessage
                    });
            }
            var eventTransferModel = await _eventService.GetSingleEvent(eventViewModel.EventId);
            var completeEventViewModel = _mapper.Map<EventViewModel>(eventTransferModel);

            return View(completeEventViewModel);
        }

        /// <summary>
        /// Shows results of a vote.
        /// </summary>
        /// <param name="id">Id of an event.</param>
        /// <returns>Page with results.</returns>
        [HttpGet]
        public async Task<IActionResult> Results(Guid id)
        {
            var requestedEventName = await _eventService.GetEventName(id);
            if (requestedEventName == null)
            {
                throw new System.Web.HttpException(404, "Event does not exist.");
            }

            ViewData["EventName"] = requestedEventName;
            var resultsViewModel = await GetResultsViewModel(id);
            resultsViewModel.ChartData.EventName = requestedEventName;

            return View(resultsViewModel);
        }

        /// <summary>
        /// Creates new event and sets <see cref="CreateEventViewModel.EventName"/> and <see cref="CreateEventViewModel.EventDescription"/>.
        /// </summary>
        /// <param name="newEvent"> <see cref="EventViewModel"/> containing data from user.</param>
        /// <returns>Redirect to <see cref="AddPlaces"/> page. </returns>
        [HttpPost]
        public async Task<IActionResult> CreateNewEvent(CreateEventViewModel newEvent)
        {
            if (String.IsNullOrEmpty(newEvent.EventName))
            {
                return RedirectToAction("CreateEvent",
                    new
                    {
                        err = "Add event title."
                    });
            }

            var savedEvent = await _eventService.AddEvent(_mapper.Map<Event>(newEvent));
            return RedirectToAction("AddPlaces",
                new
                {
                    eventId = savedEvent.EventId
                });
        }

        /// <summary>
        /// Adds <c>DateTime</c> value to <see cref="TimeAtPlaceViewModel"/> of corresponding <see cref="AddPlacesViewModel"/>.
        /// </summary>
        /// <param name="targetEvent">Currently processed event.</param>
        /// <returns>Redirect to <see cref="AddPlaces"/> page with updated data.</returns>
        [HttpPost]
        public async Task<IActionResult> AddSingleTime(AddPlacesViewModel targetEvent)
        {
            var errorMessage = await ValidateTime(targetEvent);
            if (errorMessage != null)
            {
                return RedirectToAction("AddPlaces",
                    new
                    {
                        eventId = targetEvent.EventId,
                        foursquareId = targetEvent.CurrentPlaceFoursquareId,
                        errTime = errorMessage
                    });
            }

            await _eventService.AddEventTime(targetEvent.EventId, targetEvent.CurrentPlaceFoursquareId, Convert.ToDateTime(targetEvent.CurrentTime));
            return RedirectToAction("AddPlaces",
                new
                {
                    eventId = targetEvent.EventId,
                    foursquareId = targetEvent.CurrentPlaceFoursquareId
                });
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
            {
                return Json(new List<string>());
            }
            var places = await _fsService.SearchVenuesAsync(placeName, placeCity, 10);
            foreach (var place in places)
            {
                place.PhotoUrl = await _fsService.GetVenuePhotoUrlAsync(place.Id, "36x36");
                place.Text = placeName;
            }
            return Json(places);
        }


        /// <summary>
        /// Adds instance of <see cref="PlaceViewModel"/> to the database 
        /// and creates corresponding links with <see cref="TimeAtPlaceViewModel"/> and <see cref="AddPlacesViewModel"/>.
        /// </summary>
        /// <param name="targetEvent">Currently processed event.</param>
        /// <returns>Redirect to <see cref="AddPlaces"/> page with updated data.</returns>
        [HttpPost]
        public async Task<IActionResult> AddSinglePlace(AddPlacesViewModel targetEvent)
        {
            var errorMessage = await ValidatePlace(targetEvent);
            if (errorMessage != null)
            {
                return RedirectToAction("AddPlaces",
                    new
                    {
                        eventId = targetEvent.EventId,
                        foursquareId = "ssss",
                        errPlace = errorMessage
                    });
            }

            await _eventService.AddEventPlace(targetEvent.EventId, targetEvent.CurrentPlaceFoursquareId);
            return RedirectToAction("AddPlaces",
                new
                {
                    eventId = targetEvent.EventId,
                    foursquareId = targetEvent.CurrentPlaceFoursquareId
                });
        }

        /// <summary>
        /// Gets model that will be used for rendering charts and tables with votes.
        /// </summary>
        /// <param name="id">Event id.</param>
        /// <returns>Results view model.</returns>
        private async Task<ResultsViewModel> GetResultsViewModel(Guid id)
        {
            var resultViewModel = new ResultsViewModel();

            var chartModel = new ChartModel();
            // NOTE: we do not display places and times witch zero votes
            var @event = await _eventService.GetSingleEvent(id);
            resultViewModel.EventLink = @event.EventId.ToString();
            var voteSessions = await _voteService.GetVoteSessions(@event.EventId);
            resultViewModel.VoteSessions = voteSessions;
            var data = new Dictionary<string, int>();
            foreach (var place in @event.Places)
            {
                foreach (var time in place.Times)
                {
                    var value = voteSessions.SelectMany(voteSession => voteSession.Votes)
                        .Count(vote => vote.TimeAtPlaceId == time.Id && vote.Value == VoteValueEnum.Accept);
                    var placeAndTime = place.Name + " - " + time.Time.ToString("dd/MM/yyyy H:mm");
                    data.Add(placeAndTime, value);
                    resultViewModel.TimesAtPlaces.Add(time.Id, placeAndTime);
                }
            }

            var sortedData = data.OrderByDescending(k => k.Value);

            foreach (var pair in sortedData)
            {
                chartModel.Categories.Add(pair.Key);
                chartModel.Data.Add(pair.Value);
            }

            resultViewModel.ChartData = chartModel;

            return resultViewModel;
        }

        /// <summary>
        /// Validates new time.
        /// </summary>
        /// <param name="targetEvent">View model to be validated.</param>
        /// <returns>Error message if problem was found, null otherwise.</returns>
        public async Task<string> ValidateTime(AddPlacesViewModel targetEvent)
        {
            DateTime targetTime;

            if (string.IsNullOrWhiteSpace(targetEvent.CurrentTime))
            {
                return "You can not add empty time.";
            }

            try
            {
                targetTime = Convert.ToDateTime(targetEvent.CurrentTime);
            }
            catch
            {
                return "Inserted value is not valid time.";
            }

            if (!await IsCurrentTimeUnique(targetEvent))
            {
                return "This time was already added.";
            }

            if (targetTime < DateTime.Now)
            {
                return "You can not plan event to the past date";
            }

            if (!await IsVenueOpen(targetEvent))
            {
                return "Place is not open at this time";
            }

            return null;
        }


        /// <summary>
        /// Validates new place. 
        /// </summary>
        /// <param name="targetEvent">View model to be validated.</param>
        /// <returns>Error message if problem was found, null otherwise.</returns>
        public async Task<string> ValidatePlace(AddPlacesViewModel targetEvent)
        {
            if (!await _eventService.AllPlacesHaveTime(targetEvent.EventId))
            {
                return "Add at least one time to the last place before adding another place.";
            }

            if (string.IsNullOrWhiteSpace(targetEvent.CurrentPlaceFoursquareId) || targetEvent.CurrentPlaceFoursquareId.Equals("Search places..."))
            {
                return "You can not add empty place.";
            }

            if (!await DoesVenueExist(targetEvent.CurrentPlaceFoursquareId))
            {
                return "Selected place does not exist";
            }

            if (!await IsCurrentPlaceUnique(targetEvent))
            {
                return "This place was already added. Check previous places.";
            }

            return null;
        }

        /// <summary>
        /// Checks if foursquare venue exists.
        /// </summary>
        /// <param name="id">Id of the venue.</param>
        /// <returns>True if venue exists.</returns>
        private async Task<bool> DoesVenueExist(string id)
        {
            var venue = await _fsService.GetVenueAsync(id);
            return venue != null;
        }

        /// <summary>
        /// Checks if time to be inserted has unique value.
        /// </summary>
        /// <param name="targetEvent">Model of the event to be checked.</param>
        /// <returns>True if value is unique.</returns>
        private async Task<bool> IsCurrentTimeUnique(AddPlacesViewModel targetEvent)
        {
            var currentEvent = await _eventService.GetSingleEvent(targetEvent.EventId);
            var place = currentEvent
                .Places
                .ToList()
                .First(p => p.FourSquareId.Equals(targetEvent.CurrentPlaceFoursquareId));

            return !place
                 .Times
                 .Any(timeslot => timeslot.Time.Equals(Convert.ToDateTime(targetEvent.CurrentTime)));
        }

        /// <summary>
        /// Checks if place to be inserted has unique value.
        /// </summary>
        /// <param name="targetEvent">Model of the event to be checked.</param>
        /// <returns>True if value is unique.</returns>
        private async Task<bool> IsCurrentPlaceUnique(AddPlacesViewModel targetEvent)
        {
            var currentEvent = await _eventService.GetSingleEvent(targetEvent.EventId);
            return !currentEvent
                .Places
                .ToList()
                .Any(p => p.FourSquareId.Equals(targetEvent.CurrentPlaceFoursquareId));
        }

        /// <summary>
        /// Checks if venue is open at given time.
        /// </summary>
        /// <param name="targetEvent">Model of the event to be checked.</param>
        /// <returns>True if venue is open.</returns>
        private async Task<bool> IsVenueOpen(AddPlacesViewModel targetEvent)
        {
            var time = Convert.ToDateTime(targetEvent.CurrentTime, CultureInfo.InvariantCulture);
            var venue = await _fsService.GetVenueAsync(targetEvent.CurrentPlaceFoursquareId);

            if (venue.Hours == null)
            {
                return true;
            }

            var openHours = venue
                .Hours
                .GetOpenTimeAtDay(time.DayOfWeek);

            if (openHours == null)
            {
                return false;
            }

            return openHours
                .Any(openHour => openHour.IsOpenAtTime(time.TimeOfDay));
        }

        /// <summary>
        /// Checks that model has at least one place with at least one time in the database.
        /// </summary>
        /// <param name="eventViewModel">Model to check.</param>
        /// <returns><c>True if model is valid.</c></returns>
        private async Task<string> ValidateBeforeFinish(EventViewModel eventViewModel)
        {
            if (!await _eventService.AllPlacesHaveTime(eventViewModel.EventId))
            {
                return "Add at least one time to place.";
            }

            if (!await _eventService.HasAnyPlace(eventViewModel.EventId))
            {
                return "Add at least one place.";
            }

            return null;
        }
    }
}

