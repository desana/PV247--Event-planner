using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using EventPlanner.DTO.Vote;
using EventPlanner.Models.Vote;
using EventPlanner.Services.Event;
using EventPlanner.Services.Vote;
using FoursquareVenuesService.Services;
using Microsoft.AspNetCore.Mvc;

namespace EventPlanner.Controllers
{
    public class VoteController : Controller
    {
        private readonly IEventService _eventService;
        private readonly IFoursquareService _foursquareService;
        private readonly IMapper _mapper;
        private readonly IVoteService _voteService;

        public VoteController(IEventService eventService, IFoursquareService foursquareService, IMapper mapper, IVoteService voteService)
        {
            _eventService = eventService;
            _foursquareService = foursquareService;
            _mapper = mapper;
            _voteService = voteService;
        }

        public async Task<IActionResult> Index([FromRoute(Name = "id")]int eventId)
        {
            var @event = await _eventService.GetSingleEvent(eventId);
            if (@event == null)
            {
                return NotFound();
            }

            var viewModel = _mapper.Map<VoteViewModel>(@event);
            var photos = new Dictionary<string, string>();
            foreach (var venueId in viewModel.Places.Select(i => i.Place.FourSquareId).Distinct())
            {
                photos[venueId] = await _foursquareService.GetVenuePhotoUrlAsync(venueId);
            }

            foreach (var item in viewModel.Places)
            {
                item.PlacePhotoUrl = photos[item.Place.FourSquareId];
            }

            return View(viewModel);
        }

        public async Task<IActionResult> TestVote([FromRoute(Name = "id")]int eventId, int placeAtTimeId)
        {
            var @event = await _eventService.GetSingleEvent(eventId);
            var votes = @event.Places.SelectMany(p => p.Times).Select(time => new Vote
            {
                TimeAtPlaceId = time.Id,
                Value = placeAtTimeId == time.Id ? VoteValueEnum.Accept : VoteValueEnum.Decline,
            }).ToList();

            var testVoteSession = new VoteSession
            {
                EventId = eventId,
                VoterName = "Jana Morozova",
                Votes = votes,
            };

            await _voteService.SaveVoteSession(testVoteSession);

            return Content("Voted");
        }      

    }
}
