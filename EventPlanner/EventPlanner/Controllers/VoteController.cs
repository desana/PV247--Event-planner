using System;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using EventPlanner.DTO.Vote;
using EventPlanner.Models.Vote;
using EventPlanner.Services.Event;
using EventPlanner.Services.Vote;
using FoursquareVenuesService.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace EventPlanner.Controllers
{
    public class VoteController : Controller
    {
        private readonly IEventService _eventService;
        private readonly IFoursquareService _foursquareService;
        private readonly IMapper _mapper;
        private readonly IVoteService _voteService;
        private readonly ILogger<VoteController> _logger;

        public VoteController(
            IEventService eventService, 
            IFoursquareService foursquareService, 
            IMapper mapper, 
            IVoteService voteService,
            ILogger<VoteController> logger)
        {
            _eventService = eventService;
            _foursquareService = foursquareService;
            _mapper = mapper;
            _voteService = voteService;
            _logger = logger;
        }

        public async Task<IActionResult> Index([FromRoute(Name = "id")]int eventId, [FromQuery(Name = "session")]Guid? voteSessionId)
        {
            var @event = await _eventService.GetSingleEvent(eventId);
            if (@event == null)
            {
                return NotFound();
            }

            var viewModel = _mapper.Map<VoteViewModel>(@event);
            await InitPlacePhotos(viewModel);

            var session = voteSessionId.HasValue ? await _voteService.GetVoteSession(voteSessionId.Value) : null;
            viewModel.VoteSession = session ?? _voteService.InitializeVoteSession(@event);

            return View(viewModel);
        }

        private async Task InitPlacePhotos(VoteViewModel viewModel)
        {
            foreach (var votePlaceViewModel in viewModel.Places)
            {
                try
                {
                    var photoUrl =
                        await _foursquareService.GetVenuePhotoUrlAsync(votePlaceViewModel.Place.FourSquareId, "200x200");
                    votePlaceViewModel.PlacePhotoUrl = photoUrl;
                }
                catch (Exception ex)
                {
                    _logger.LogWarning(1, ex, ex.Message);
                }
            }
        }

        public async Task<IActionResult> Vote([FromRoute(Name = "id")]int eventId, Guid? voteSessionId, int timeAtPlaceId, string value)
        {
            var @event = await _eventService.GetSingleEvent(eventId);
            if (@event == null)
                return NotFound();

            var session = voteSessionId.HasValue ? await _voteService.GetVoteSession(voteSessionId.Value) : null;
            session = session ?? _voteService.InitializeVoteSession(@event);

            var vote = session.Votes.FirstOrDefault(v => v.TimeAtPlaceId == timeAtPlaceId);
            if (vote == null)
                return NotFound();

            VoteValueEnum enumValue = VoteValueEnum.Decline;
            Enum.TryParse(value, true, out enumValue);
            vote.Value = enumValue;

            session = await _voteService.SaveVoteSession(session);

            return RedirectToAction("Index", new {id = eventId, session = session.VoteSessionId});
        }

        public async Task<IActionResult> ChangeName([FromRoute(Name = "id")]int eventId, Guid? voteSessionId, string name)
        {
            var @event = await _eventService.GetSingleEvent(eventId);
            if (@event == null)
                return NotFound();

            var session = voteSessionId.HasValue ? await _voteService.GetVoteSession(voteSessionId.Value) : null;
            session = session ?? _voteService.InitializeVoteSession(@event);

            session.VoterName = name;

            session = await _voteService.SaveVoteSession(session);

            return RedirectToAction("Index", new { id = eventId, session = session.VoteSessionId });
        }
    }
}
