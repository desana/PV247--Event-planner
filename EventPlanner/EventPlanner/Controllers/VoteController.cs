using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using EventPlanner.Models.Vote;
using EventPlanner.Services.Event;
using FoursquareVenuesService.Services;
using Microsoft.AspNetCore.Mvc;

namespace EventPlanner.Controllers
{
    public class VoteController : Controller
    {
        private readonly IEventService _eventService;
        private readonly IFoursquareService _foursquareService;
        private readonly IMapper _mapper;

        public VoteController(IEventService eventService, IFoursquareService foursquareService, IMapper mapper)
        {
            _eventService = eventService;
            _foursquareService = foursquareService;
            _mapper = mapper;
        }

        public async Task<IActionResult> Index([FromRoute(Name = "id")]int eventId)
        {
            var @event = await _eventService.GetSingleEvent(eventId);
            if (@event == null)
            {
                return NotFound();
            }

            var viewModel = _mapper.Map<VoteCollectionViewModel>(@event);
            var photos = new Dictionary<string, string>();
            foreach (var venueId in viewModel.VoteItems.Select(i => i.Place.FourSquareId).Distinct())
            {
                photos[venueId] = await _foursquareService.GetVenuePhotoUrlAsync(venueId);
            }

            foreach (var item in viewModel.VoteItems)
            {
                item.PlacePhotoUrl = photos[item.Place.FourSquareId];
            }

            return View(viewModel);
        }
    }
}
