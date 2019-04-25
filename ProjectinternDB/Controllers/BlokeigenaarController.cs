using System;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using Logic;
using System.Collections.Generic;
using Microsoft.AspNetCore.Http;
using Model;
using Model.Onderwijsdelen;

namespace ProjectinternDB.Controllers
{
    //[Authorize(Roles = "Blokeigenaar")]
    public class BlokeigenaarController : Controller
    {

        private BlokeigenaarLogic _taakLogic = new BlokeigenaarLogic();

        public IActionResult Index()
        {
            IEnumerable<Taak> taken = _taakLogic.TakenOphalen();
            return View(taken);
        }

        public IActionResult TaakToevoegen()
        {
            //throw new System.NotImplementedException();
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult TaakToevoegen(IFormCollection form)
        {
            Taak taak = new Taak { Taak_info = form["Taak_info"] };
            if (form["TaakNaam"] == "")
            {
                taak.TaakNaam = null;
            }
            else
            {
                taak.TaakNaam = form["TaakNaam"];
            }
            taak.TaakId = Convert.ToInt32(form["TaakId"]);
            _taakLogic.TaakAanmaken(taak);
            return RedirectToAction("Index");
        }
    }
}