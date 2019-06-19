using System;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using Logic;
using System.Collections.Generic;
using System.Security.Claims;
using Data.Context;
using Microsoft.AspNetCore.Http;
using Model;
using Model.Onderwijsdelen;
using ProjectinternDB.Models;

namespace ProjectinternDB.Controllers
{
    [Authorize(Roles = "Blokeigenaar")]
    public class BlokeigenaarController : Controller
    {

        private BlokeigenaarLogic _blokeigenaarLogic = new BlokeigenaarLogic();
        public IActionResult Index()
        {
            IEnumerable<Taak> taken = _blokeigenaarLogic.TakenOphalen(1);
            return View(taken);
        }

        public IActionResult TaakToevoegen()
        {
            string User_id = User.FindFirstValue(ClaimTypes.NameIdentifier);
           
            List<Traject> TrajectLijst = new List<Traject>();
            TrajectLijst = _blokeigenaarLogic.GetTrajecten();
            TrajectLijst.Insert(0, new Traject { TrajectId = 0, TrajectNaam = "Select" });
            ViewBag.ListOfTraject = TrajectLijst;

            List<Eenheid> EenheidLijst = new List<Eenheid>();
            EenheidLijst = _blokeigenaarLogic.OphalenEenhedenBlokeigenaar(User_id);
            EenheidLijst.Insert(0, new Eenheid { EenheidId = 0, EenheidNaam = "Select" });
            ViewBag.EenhedenBlokeigenaar = EenheidLijst;
            
            BlokeigenaarViewModel BEVmodel = new BlokeigenaarViewModel();

            return View(BEVmodel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult TaakToevoegen(IFormCollection form)
        {
            Taak taak = new Taak
            {
                OnderdeelNaam = form["OnderdeelNaam"],
                OnderdeelId = Int32.Parse(form["OnderdeelId"]),
                Omschrijving = form["Omschrijving"],
                BenodigdeUren = Int32.Parse(form["BenodigdeUren"]),
                AantalKlassen = Int32.Parse(form["AantalKlassen"])
            };
            if (form["TaakNaam"] == "")
            {
                taak.TaakNaam = null;
            }
            else
            {
                taak.TaakNaam = form["TaakNaam"];
            }
            
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
            string User_id = User.FindFirstValue(ClaimTypes.NameIdentifier);

            BlokeigenaarViewModel BEVmodel = new BlokeigenaarViewModel();
            BEVmodel.Taak = _blokeigenaarLogic.TaakOphalen(id);

            return View(BEVmodel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult EditTaak(int id, IFormCollection form)
        {
            Taak taak = new Taak()
            {
                TaakId = id,
                TaakNaam = form["Taak.TaakNaam"],
                //OnderdeelId = Int32.Parse(form["OnderdeelId"]),
                Omschrijving = form["Taak.Omschrijving"],
                OnderdeelNaam = form["Taak.OnderdeelNaam"],
                BenodigdeUren = Int32.Parse(form["Taak.BenodigdeUren"]),
                AantalKlassen = Int32.Parse(form["Taak.AantalKlassen"])
            };

            _blokeigenaarLogic.UpdateTaak(taak);
            return RedirectToAction("Index");
        }
        
    }
}