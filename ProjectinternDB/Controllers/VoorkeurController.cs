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
using Microsoft.AspNetCore.Authorization;
using Model.Onderwijsdelen;
using Microsoft.AspNetCore.Mvc.Rendering;
using Data;
using Data.Context;
using Microsoft.AspNetCore.Http;

namespace ProjectinternDB.Controllers
{
    //[Authorize(Roles = "Teamleider")]
    public class VoorkeurController : Controller
    {
        private VoorkeurLogic _voorkeurLogic;

        public VoorkeurController(IVoorkeurContext context)
        {
            _voorkeurLogic = new VoorkeurLogic(context);
        }

        public IActionResult Index()
        {
            return View();
        }

        public JsonResult GetOnderdeel(int TrajectId)
        {
            List<Onderdeel> Onderdelen = new List<Onderdeel>();

            Onderdelen = _voorkeurLogic.GetOnderdelenByTrajectId(TrajectId);
            Onderdelen.Insert(0, new Onderdeel { OnderdeelId = 0, OnderdeelNaam = "Select" });

            return Json(new SelectList(Onderdelen, "OnderdeelId", "OnderdeelNaam"));
        }

        public JsonResult GetTaak(int OnderdeelId)
        {
            List<Taak> productList = new List<Taak>();

       
            productList = _voorkeurLogic.GetTakenByOnderdeelId(OnderdeelId);

            // ------- Inserting Select Item in List -------
            productList.Insert(0, new Taak { TaakId = 0, TaakNaam = "Select" });

            return Json(new SelectList(productList, "TaakId", "TaakNaam"));
        }

        public IActionResult userInlog(string User_id)
        {
            User_id = User.FindFirstValue(ClaimTypes.NameIdentifier);
            _voorkeurLogic.krijgUser_id(User_id);

            return RedirectToAction("Index");
        }
        [HttpPost]
        public IActionResult InvoegenVoorkeur(/*VoorkeurViewModel voorkeur*/VoorkeurViewModel objTraject, IFormCollection formCollection)
        {

            //_voorkeurLogic.AddVoorkeur(voorkeur.TrajectNaam, voorkeur.Prioriteit, voorkeur.Onderdeel.OnderdeelNaam, voorkeur.Taak.TaakNaam, User.FindFirstValue(ClaimTypes.NameIdentifier));

            //return RedirectToAction("Index");
            var Prioriteit = HttpContext.Request.Form["Prioriteit"].ToString();
            int test = Convert.ToInt32(Prioriteit);
            var OnderdeelId = HttpContext.Request.Form["OnderdeelId"].ToString();
            var TaakId = HttpContext.Request.Form["TaakId"].ToString();
            _voorkeurLogic.AddVoorkeur(objTraject.TrajectId, test, OnderdeelId, TaakId, User.FindFirstValue(ClaimTypes.NameIdentifier));
            return RedirectToAction("Index");
        }
        //[HttpPost]
        //public IActionResult InvoegenVoorkeur(VoorkeurViewModel objTraject, IFormCollection formCollection)
        //{

        //}
        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

        public IActionResult Delete(int id)
        {
            _voorkeurLogic.DeleteVoorkeur(id);
            return RedirectToAction("Index");
        }
    }
}