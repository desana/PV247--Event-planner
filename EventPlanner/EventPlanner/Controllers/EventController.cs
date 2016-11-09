using System.Threading.Tasks;
using AutoMapper;
using EventPlanner.Models;
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

        public IActionResult AddPlaces()
        {
            return View(TempData["event"] as EventViewModel);
        }
    }
}

