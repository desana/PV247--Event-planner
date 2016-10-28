using System;
using Microsoft.AspNetCore.Mvc;

namespace EventPlanner.Controllers
{
    public class CreateAndVoteController : Controller
    {
        // GET: /<controller>/
        public IActionResult Index()
        {
            return CreateAndVoteView();
        }

        private IActionResult CreateAndVoteView()
        {
            throw new NotImplementedException();
        }
    }
}
