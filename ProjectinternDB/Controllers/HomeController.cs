using Logic;
using Microsoft.AspNetCore.Mvc;
using ProjectinternDB.Models;
using System.Collections.Generic;
using System.Diagnostics;
using System.Security.Claims;
using Data;
using Model.Onderwijsdelen;

namespace ProjectinternDB.Controllers
{
    public class HomeController : Controller
    {
        private VoorkeurLogic _voorkeurLogic;

        public HomeController(IVoorkeurContext context)
        {
            _voorkeurLogic = new VoorkeurLogic(context);
        }

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

        public IActionResult Privacy()
        {
            return View();
        }
    

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}