using EventPlanner.Models;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling MVC for empty projects, visit http://go.microsoft.com/fwlink/?LinkID=397860

namespace EventPlanner.Controllers
{
    public class EventController : Controller
    {
        private EventPlannerDbContext _databaseContext = new EventPlannerDbContext();
        private Models.CreateEventModel _createEventModel = new CreateEventModel();


        // GET: /<controller>/
        public IActionResult CreateEvent()
        {
            return View();
        }
    }
}
