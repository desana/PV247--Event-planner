using System.Collections.Generic;
using System.ComponentModel.Design;
using EventPlanner.Entities;
using EventPlanner.Models;
using EventPlanner.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

// For more information on enabling MVC for empty projects, visit http://go.microsoft.com/fwlink/?LinkID=397860

namespace EventPlanner.Controllers
{
    public class EventController : Controller
    {
        private EventPlannerDbContext _databaseContext = new EventPlannerDbContext();
        private Models.CreateEventViewModel _createEventViewModel = new CreateEventViewModel();
        private Services.EventService _eventService = new EventService();


        // GET: /<controller>/
        public IActionResult CreateEvent()
        {
            return View();
        }

        public IActionResult CreateNewEvent()
        {
            var newEvent = new CreateEventViewModel();
            return View(newEvent);
        }
    }
}

