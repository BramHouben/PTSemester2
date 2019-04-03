using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Logic;
using ProjectinternDB.Models;
using Microsoft.AspNetCore.Identity;
using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;

namespace ProjectinternDB.Controllers
{
    //[Authorize(Roles = "Teamleider")]
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
    }
}