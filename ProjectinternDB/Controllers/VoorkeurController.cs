using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Logic;
using ProjectinternDB.Models;

namespace ProjectinternDB.Controllers
{
    public class VoorkeurController : Controller
    {

        private VoorkeurLogic _voorkeurLogic = new VoorkeurLogic();

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult InvoegenVoorkeur(VoorkeurViewModel voorkeur)
        {

            _voorkeurLogic.AddVoorkeur(voorkeur.Vak_naam, voorkeur.Prioriteit);

            return RedirectToAction("Index");
        }
    }
}