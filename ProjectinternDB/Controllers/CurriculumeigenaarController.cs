using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Data.Interfaces;
using Logic;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Model.Onderwijsdelen;

namespace ProjectinternDB.Controllers
{
    [Authorize(Roles = "CurriculumEigenaar")]
    public class CurriculumeigenaarController : Controller
    {
        private MedewerkerLogic _medewerkerLogic = new MedewerkerLogic();
        private AlgoritmeLogic algoritmeLogic;

        public CurriculumeigenaarController(IAlgoritmeContext algoritmeContext)
        {
            algoritmeLogic = new AlgoritmeLogic(algoritmeContext);
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult OverzichtEenheden()
        {
            var result = _medewerkerLogic.KrijgAlleEenheden();
            return View(result);
        }

        public IActionResult WijzigEenheid(int id, string eenheidNaam, string trajectNaam, int ects, int klassen)
        {
            Eenheid eenheid = new Eenheid(id, eenheidNaam, trajectNaam, ects, klassen);
            //TODO
            _medewerkerLogic.WijzigEenheid(eenheid);
            return RedirectToAction("OverzichtEenheden", "CurriculumEigenaar");
        }

        public IActionResult ActiverenSysteem()
        {
            var result = algoritmeLogic.ActiverenSysteen();
            return View(result);
        }
    }
}