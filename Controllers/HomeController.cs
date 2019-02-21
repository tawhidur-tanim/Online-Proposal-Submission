using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;

namespace ProjectFinal101.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Error()
        {
            return View();
        }

        [Authorize]
        public IActionResult Protected()
        {
            return Ok("Authorized");
        }

        [HttpPost]
        public IActionResult Get()
        {
            return Ok(new
            {
                data = new List<int>(),
                count = 0
            });
        }
    }
}
