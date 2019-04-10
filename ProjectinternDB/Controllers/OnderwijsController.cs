using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Logic;
using Microsoft.AspNetCore.Mvc;
using Model;
using ProjectinternDB.Models;

namespace ProjectinternDB.Controllers
{
    public class OnderwijsController : Controller
    {
        private OnderwijsLogic _onderwijsLogic = new OnderwijsLogic();

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult OphalenOnderwijsTaken()
        {
            IEnumerable<Onderwijstaak> taken = _onderwijsLogic.onderwijstaak();
            return View(taken);
        }

        public IActionResult OphalenOnderwijsTrajecten()
        {
            IEnumerable<Onderwijstraject> trajecten = _onderwijsLogic.onderwijstraject();
            return View(trajecten);
        }

        public IActionResult OphalenOnderwijsOnderdelen()
        {
            IEnumerable<Onderwijsonderdeel> onderdelen = _onderwijsLogic.onderwijsonderdeel();
            return View(onderdelen);
        }

        public IActionResult OphalenOnderwijsEenheden()
        {
            IEnumerable<Onderwijseenheid> eenheden = _onderwijsLogic.onderwijseenheid();
            return View(eenheden);
        }
    }
}