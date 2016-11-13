using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using AutoMapper;
using EventPlanner.Models;
using EventPlanner.Services.DataTransferModels.Models;
using EventPlanner.Services.Event;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling MVC for empty projects, visit http://go.microsoft.com/fwlink/?LinkID=397860

namespace EventPlanner.Controllers
{
    public partial class EventController : Controller
    {
        // GET: /<controller>/
        public IActionResult CreateEvent()
        {
            return View();
        }

        [HttpGet]
        public async Task<IActionResult> Results(int id)
        {   
            var requestedEventName = await _eventService.GetEventName(id);

            if (requestedEventName == null)
            {
                //TODO show 404 or something
            }

            ViewData["EventName"] = requestedEventName;
            var chartModel = await GetChartModel(id);
            chartModel.EventName = requestedEventName;

            return View(chartModel);
        }

        public IActionResult AddPlaces()
        {
            return View(TempData["event"] as EventViewModel);
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
            
            var savedEvent = await _eventService.AddEvent(_mapper.Map<Services.DataTransferModels.Models.EventTransferModel>(newEvent));
            TempData["event"] = _mapper.Map<EventViewModel>(savedEvent);

            return RedirectToAction("AddPlaces");
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
            var votes = _mapper.Map<IEnumerable<VoteViewModel>>(await _eventService.GetVotesForEvent(id));
            var data = votes.ToDictionary(
                vote => vote.TimeAtPlace.Place.Name + " - " + vote.TimeAtPlace.Time.ToString("dd/MM/yyyy H:mm"), 
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

