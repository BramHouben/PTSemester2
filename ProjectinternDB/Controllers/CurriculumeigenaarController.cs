using Data.Interfaces;
using Logic;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Model.Onderwijsdelen;
using ProjectinternDB.Models;
using System;
using System.Collections.Generic;

namespace ProjectinternDB.Controllers
{
    [Authorize(Roles = "CurriculumEigenaar")]
    public class CurriculumeigenaarController : Controller
    {
        private MedewerkerLogic _medewerkerLogic;
        private AlgoritmeLogic algoritmeLogic;

        public CurriculumeigenaarController(IAlgoritmeContext algoritmeContext)
        {
            algoritmeLogic = new AlgoritmeLogic(algoritmeContext);
            _medewerkerLogic = new MedewerkerLogic();
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult OverzichtEenheden()
        {
            var model = new EenheidViewModel();
            var result = _medewerkerLogic.KrijgAlleEenheden();
            var tuple = new Tuple<IEnumerable<Eenheid>, EenheidViewModel>(result, model);
            return View(tuple);
        }

        [HttpPost]
        public IActionResult WijzigEenheid(int id, string eenheidNaam, string trajectNaam, IFormCollection form)
        {
            int ECTS = Convert.ToInt32(form["Item2.ECTS"]);
            int AantalKlassen = Convert.ToInt32(form["Item2.AantalKlassen"]);
            Eenheid eenheid = new Eenheid(id, eenheidNaam, trajectNaam, ECTS, AantalKlassen);

            _medewerkerLogic.WijzigEenheid(eenheid);
            return RedirectToAction("OverzichtEenheden", "CurriculumEigenaar");
        }

        public IActionResult ActiverenSysteem()
        {
            var result = algoritmeLogic.ActiverenSysteen();
            return View(result);
        }

        public IActionResult AlgoritmeActiveren()
        {
            algoritmeLogic.AlgoritmeStarten();
            return RedirectToAction("ActiverenSysteem", "CurriculumEigenaar");
        }
    }
}