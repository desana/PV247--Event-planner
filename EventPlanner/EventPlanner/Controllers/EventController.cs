using System;
using System.Threading.Tasks;
using EventPlanner.Models;
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

namespace EventPlanner.Controllers
{
    public partial class EventController : Controller
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

        [HttpGet]
        public async Task<IActionResult> AddPlaces(Guid eventId, string foursquareId = "", string errTime = "", string errPlace = "")
        {
            ViewData["EventName"] = "New event";

            var eventTransferModel = await _eventService.GetSingleEvent(eventId);
            var eventViewModel = _mapper.Map<AddPlacesViewModel>(eventTransferModel);

            eventViewModel.CurrentPlaceFoursquareId = foursquareId;
            eventViewModel.PlaceErrorMessage = errPlace;
            eventViewModel.TimeErrorMessage = errTime;

            return View(eventViewModel);
        }
        
        [HttpPost]
        public async Task<IActionResult> ShowCreatedEvent(EventViewModel eventViewModel)
        {
            var eventTransferModel = await _eventService.GetSingleEvent(eventViewModel.EventId);
            var completeEventViewModel = _mapper.Map<EventViewModel>(eventTransferModel);

            return View(completeEventViewModel);
        }

        [HttpGet]
        public async Task<IActionResult> Results(Guid id)
        {
            var requestedEventName = await _eventService.GetEventName(id);

            if (requestedEventName == null)
            {
                //TODO show 404 or something
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
                return RedirectToAction("CreateEvent", new {
                    err = "Add event title."
                });
            }

            var savedEvent = await _eventService.AddEvent(_mapper.Map<Event>(newEvent));
            return RedirectToAction("AddPlaces", new {
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
                return RedirectToAction("AddPlaces", new {
                    eventId = targetEvent.EventId,
                    foursquareId = targetEvent.CurrentPlaceFoursquareId,
                    errTime =  errorMessage
                });
            }

            var currentTimeAtPlaceId = await _eventService.AddEventTime(targetEvent.EventId, targetEvent.CurrentPlaceFoursquareId, Convert.ToDateTime(targetEvent.CurrentTime));
            return RedirectToAction("AddPlaces", new {
                eventId = targetEvent.EventId,
                foursquareId = targetEvent.CurrentPlaceFoursquareId });
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
                return RedirectToAction("AddPlaces", new {
                    eventId = targetEvent.EventId,
                    // TODO add last valid place ?!
                    errPlace = errorMessage
                });
            }

            await _eventService.AddEventPlace(targetEvent.EventId, targetEvent.CurrentPlaceFoursquareId);
            return RedirectToAction("AddPlaces", new {
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

        #region Validation methods

        /// <summary>
        /// Validates new time.
        /// </summary>
        /// <param name="targetEvent">View model to be validated.</param>
        /// <returns>Error message if problem was found, null otherwise.</returns>
        public async Task<string> ValidateTime(AddPlacesViewModel targetEvent)
        {
            DateTime targetTime;

            if (String.IsNullOrWhiteSpace(targetEvent.CurrentTime))
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

            if (!(await IsCurrentTimeUnique(targetEvent)))
            {
                return "This time was already added.";
            }

            if (targetTime < DateTime.Now)
            {
                return "You can not plan event to the past date";
            }

            if (!(await IsVenueOpen(targetEvent)))
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
            if (String.IsNullOrWhiteSpace(targetEvent.CurrentPlaceFoursquareId) || targetEvent.CurrentPlaceFoursquareId.Equals("Search places..."))
            {
                return "You can not add empty place.";
            }

            if (!(await DoesVenueExist(targetEvent.CurrentPlaceFoursquareId)))
            {
                return "Selected place does not exist";
            }

            if (!(await IsCurrentPlaceUnique(targetEvent)))
            {
                return "This place was already added. Please check previous places.";
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
                .GetOpenTimeForDay(time.DayOfWeek);
            
            if (openHours == null)
            {
                return false;
            }

            return openHours
                .Any(openHour => openHour.IsOpenAtTime(time.TimeOfDay));            
        }
        
        #endregion
    }
}

