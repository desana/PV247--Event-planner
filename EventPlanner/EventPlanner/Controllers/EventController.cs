﻿using System.Threading.Tasks;
using EventPlanner.Models;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling MVC for empty projects, visit http://go.microsoft.com/fwlink/?LinkID=397860

namespace EventPlanner.Controllers
{
    public partial class EventController : Controller
    {
        [HttpGet]
        public IActionResult CreateEvent()
        {
            return View();
        }

        [HttpGet]
        public async Task<IActionResult> AddPlaces(int eventId)
        {
            var eventTransferModel = await _eventService.GetSingleEvent(eventId);
            var eventViewModel = _mapper.Map<EventViewModel>(eventTransferModel);
            return View(eventViewModel);
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
    }
}

