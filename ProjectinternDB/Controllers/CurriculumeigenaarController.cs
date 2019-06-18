using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Data.Interfaces;
using Logic;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Model.Onderwijsdelen;
using ProjectinternDB.Models;

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
            var model = new EenheidViewModel();
            var result = _medewerkerLogic.KrijgAlleEenheden();
            var tuple = new Tuple<IEnumerable<Eenheid>, EenheidViewModel>(result, model);
            return View(tuple);
        }

        public IActionResult WijzigEenheid(int id, string eenheidNaam, string trajectNaam, [FromQuery]int ECTS, [FromQuery]int AantalKlassen)
        {
            Eenheid eenheid = new Eenheid(id, eenheidNaam, trajectNaam, ECTS, AantalKlassen);
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