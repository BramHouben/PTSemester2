using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Logic;
using ProjectinternDB.Models;
using Microsoft.AspNetCore.Identity;
using System.Security.Claims;

namespace ProjectinternDB.Controllers
{
    public class VoorkeurController : Controller
    {

        private VoorkeurLogic _voorkeurLogic = new VoorkeurLogic();

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult userInlog(string User_id)
        {
            User_id = User.FindFirstValue(ClaimTypes.NameIdentifier);
            _voorkeurLogic.krijgUser_id(User_id);

            return RedirectToAction("Index");
        }

        public IActionResult InvoegenVoorkeur(VoorkeurViewModel voorkeur)
        {

            _voorkeurLogic.AddVoorkeur(voorkeur.Vak_naam, voorkeur.Prioriteit, User.FindFirstValue(ClaimTypes.NameIdentifier));

            return RedirectToAction("Index");
        }
        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}