using Logic;
using Microsoft.AspNetCore.Mvc;
using ProjectinternDB.Models;
using System.Collections.Generic;
using System.Diagnostics;

namespace ProjectinternDB.Controllers
{
    public class HomeController : Controller
    {
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

        public IActionResult Voorkeur()
        {
            return View();
        }

        public IActionResult VoorkeurUitslag()
        {
            var voorkeurLogic = new VoorkeurLogic();

            var voorkeuren = new List<VoorkeurViewModel>();

            foreach (var voorkeur in voorkeurLogic.OphalenVoorkeur())
            {
                voorkeuren.Add(new VoorkeurViewModel
                {
                    Vak_naam = voorkeur.Vak_naam,
                    Prioriteit = voorkeur.Prioriteit
                });
            }

            return View(voorkeurLogic.OphalenVoorkeur());
        }

        

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}