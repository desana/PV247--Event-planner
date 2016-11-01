using System;
using EventPlanner.Models;
using Microsoft.AspNetCore.Mvc;

namespace EventPlanner.Controllers
{
    public class HomeController : Controller
    {
        private EventPlannerDbContext _databaseContext = new EventPlannerDbContext();
        private Models.CreateEventModel _createEventModel = new CreateEventModel();

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult About()
        {
            ViewData["Message"] = "Your application description page.";

            return View();
        }

        public IActionResult Contact()
        {
            ViewData["Message"] = "Your contact page.";

            return View();
        }

        public IActionResult Error()
        {
            return View();
        }

        public IActionResult CreateEvent()
        {
            return View(_createEventModel);
        }

        public IActionResult Create()
        {
            throw new NotImplementedException();

            var newEvent = new CreateEventModel();
            return View(newEvent);
        }
    }
}
