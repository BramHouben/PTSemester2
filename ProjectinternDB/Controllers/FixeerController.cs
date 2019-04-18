using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace ProjectinternDB.Controllers
{
    public class FixeerController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult TaakFixerenMetDocentID()
        {
            string id = User.FindFirstValue(ClaimTypes.NameIdentifier);
            int 

            return View();
        }
    }
}