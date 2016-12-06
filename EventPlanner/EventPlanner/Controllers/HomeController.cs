using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EventPlanner.DTO.Event;
using EventPlanner.Services.Event;
using Microsoft.AspNetCore.Mvc;

namespace EventPlanner.Controllers
{
    public class HomeController : Controller
    {
        private readonly IEventService _eventService;

        public HomeController(IEventService eventService)
        {
            _eventService = eventService;
        }

        public IActionResult Index()
        {
            ViewData["EventName"] = "Home";
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

        public async Task<IActionResult> Vote()
        {
            return View(await _eventService.GetAllEvents());
        }

        public async Task<IActionResult> TestEvent()
        {
            // Test add
            var newEvent = new Event
            {
                Name = "Test new event " + DateTime.Now,
                Places = new List<Place>
                {
                    new Place
                    {
                        Name = "Test place",
                        FourSquareId = "503327b8e4b020d49ee37f72",
                        Times = new List<TimeAtPlace>
                        {
                            new TimeAtPlace { Time = DateTime.Now.AddYears(20) }
                        }
                    }
                }
            };

            var addedEvent = await _eventService.AddEvent(newEvent);

            // Test update
            var eventFromDb = await _eventService.GetSingleEvent(addedEvent.Id);
            eventFromDb.Places.Add(new Place
            {
                Name = "Test place 2",
                FourSquareId = "4edb4f7029c2b9122985cec4",
                Times = new List<TimeAtPlace>
                {
                    new TimeAtPlace { Time = DateTime.Now }
                }
            });

            eventFromDb.Places.First().Name = "Test renamed place";
            eventFromDb.Places.First().Times.Clear();
            eventFromDb.Name = "Test updated event " + DateTime.Now;

            await _eventService.SaveEvent(eventFromDb);

            return Content("Event created");
        }

        public IActionResult TestFourSquare()
        {
            return View();
        }
    }
}
