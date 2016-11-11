﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using AutoMapper;
using EventPlanner.Entities;
using EventPlanner.Models;
using EventPlanner.Services.DataTransferModels.Models;
using EventPlanner.Services.Event;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling MVC for empty projects, visit http://go.microsoft.com/fwlink/?LinkID=397860

namespace EventPlanner.Controllers
{
    public class EventController : Controller
    {
        private readonly IEventService _eventService;
        private readonly IMapper _mapper;

        public EventController(IMapper mapper, IEventService eventService)
        {
            _mapper = mapper;
            _eventService = eventService;
        }

        // GET: /<controller>/
        public IActionResult CreateEvent()
        {
            return View();
        }

        [HttpGet]
        public async Task<IActionResult> Results(int id)
        {   
            var requestedEvent = _mapper.Map<EventViewModel>(await _eventService.GetSingleEvent(id));

            if (requestedEvent == null)
            {
                //TODO
            }

            ViewData["EventName"] = requestedEvent.EventName;

            return View(requestedEvent);
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
    }
}

