using System;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using Logic;
using System.Collections.Generic;
using System.Security.Claims;
using Microsoft.AspNetCore.Http;
using Model;
using Model.Onderwijsdelen;
using ProjectinternDB.Models;

namespace ProjectinternDB.Controllers
{
    //[Authorize(Roles = "Blokeigenaar")]
    public class BlokeigenaarController : Controller
    {

        private BlokeigenaarLogic _blokeigenaarLogic = new BlokeigenaarLogic();

        public IActionResult Index()
        {
            IEnumerable<Taak> taken = _blokeigenaarLogic.TakenOphalen();
            return View(taken);
        }

        public IActionResult TaakToevoegen()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult TaakToevoegen(IFormCollection form)
        {
            Taak taak = new Taak { Omschrijving = form["Omschrijving"] };
            if (form["TaakNaam"] == "")
            {
                taak.TaakNaam = null;
            }
            else
            {
                taak.TaakNaam = form["TaakNaam"];
            }

            // AI TaakId
            List<Taak> alleTaken = _blokeigenaarLogic.TakenOphalen();
            int autoIncrementHoogsteId = 1;
            foreach (Taak eenTaak in alleTaken)
            {
                if (eenTaak.TaakId > autoIncrementHoogsteId)
                {
                    autoIncrementHoogsteId = eenTaak.TaakId + 1;
                }
            }
            taak.TaakId = autoIncrementHoogsteId;

            _blokeigenaarLogic.TaakAanmaken(taak);
            return RedirectToAction("Index");
        }

        public IActionResult VerwijderTaak(int id)
        {
            _blokeigenaarLogic.TaakVerwijderen(id);
            return RedirectToAction("Index");
        }
        
        public IActionResult EditTaak(int id)
        {
            List<Traject> TrajectLijst = new List<Traject>();

            TrajectLijst = _blokeigenaarLogic.GetTrajecten();

            TrajectLijst.Insert(0, new Traject { TrajectId = 0, TrajectNaam = "Select" });

            ViewBag.ListOfTraject = TrajectLijst;
            string User_id = User.FindFirstValue(ClaimTypes.NameIdentifier);

            BlokeigenaarViewModel BEVmodel = new BlokeigenaarViewModel();
            //BEVmodel.OnderwijsOnderdelen = _blokeigenaarLogic.GetOnderdelenByEenheidId();
            BEVmodel.Taak = _blokeigenaarLogic.TaakOphalen(id);

            return View(BEVmodel);

            //return View(_blokeigenaarLogic.TaakOphalen(id));
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult EditTaak(int id, IFormCollection form)
        {
            Taak taak = new Taak()
            {
                TaakNaam = form["Taak.TaakNaam"],
                Omschrijving = form["Taak.Omschrijving"],
                //OnderdeelId = Int32.Parse(form["OnderdeelId"]),
                OnderdeelNaam = form["Taak.OnderdeelNaam"],
                TaakId = id,
            };
            _blokeigenaarLogic.UpdateTaak(taak);
            return RedirectToAction("Index");
        }



    }
}