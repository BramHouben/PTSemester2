using Logic;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Model;
using Model.Onderwijsdelen;
using ProjectinternDB.Models;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Security.Claims;

namespace ProjectinternDB.Controllers
{
    [Authorize(Roles = "Teamleider,Blokeigenaar")]
    public class TeamController : Controller
    {
        private TeamLogic _teamLogic = new TeamLogic();
        private VacatureLogic _vacatureLogic = new VacatureLogic();
        private FixerenLogic _fixerenLogic = new FixerenLogic();
        private OnderwijsLogic _onderwijsLogic = new OnderwijsLogic();
        private BlokeigenaarLogic _blokeigenaarLogic = new BlokeigenaarLogic();

        //public IActionResult TeamOverzicht()
        //{
        //    IEnumerable<Team> teams = _teamLogic.TeamsOphalen();
        //    IEnumerable<Docent> docenten = _teamLogic.DocentenOphalen(1);

        //    return View(teams);
        //}

        public IActionResult Fixeren(int id)
        {
            int ID = id;
            var tupleData = new Tuple<IEnumerable<Taak>, int>(_teamLogic.GetTaken(ID), ID);
            return View(tupleData);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult TaakFixeren(int id)
        {
            var taak = HttpContext.Request.Form["TaakId"];
            int taakid = Convert.ToInt32(taak);
            _fixerenLogic.TaakFixerenMetDocentID(id, taakid);

            return RedirectToAction("Index", "Home");
        }

        // Laten staan - Frits
        public ActionResult HaalGefixeerdeTakenOp()
        {
            int id = _teamLogic.HaalTeamIDOpMetString(User.FindFirstValue(ClaimTypes.NameIdentifier));
            var team = _teamLogic.TeamOphalenMetID(id);
            var result = _fixerenLogic.HaalGefixeerdeTaakOpMetID(Convert.ToInt32(team));
            return View(result);
        }

        public IActionResult VerwijderGefixeerdeTaak(int id)
        {
            _fixerenLogic.VerwijderGefixeerdeTaak(id);
            return RedirectToAction("FixerenOverzicht");
        }

        public ActionResult FixerenOverzicht()
        {
            var result = _fixerenLogic.HaalAlleGefixeerdeTakenOp();
            return View(result);
        }

        public IActionResult DocentenInTeam()
        {
            string id = User.FindFirstValue(ClaimTypes.NameIdentifier);
            int teamid = _teamLogic.HaalTeamIDOpMetString(id);
            List<Docent> docenten = _teamLogic.DocentenOphalen(teamid);
            return View(docenten);
        }

        //public IActionResult DocentenToevoegen(TeamViewModel team)
        //{
        //    foreach (var docent in team.Docenten)
        //    {
        //        _teamLogic.VoegDocentToeAanTeam(docent);
        //    }
        //    return RedirectToAction("Index");
        //}

        public IActionResult Index()
        {
            var teams = new List<TeamViewModel>();
            int id = _teamLogic.HaalTeamIDOpMetString(User.FindFirstValue(ClaimTypes.NameIdentifier));
            var team = _teamLogic.TeamOphalenMetID(id);
            return View(team);
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

        public IActionResult Edit(int id)
        {
            var teamLogic = new TeamLogic();
            Team geselecteerdTeam = teamLogic.TeamOphalenMetID(id);
            if (geselecteerdTeam == null)
            {
                return NotFound();
            }

            return View(geselecteerdTeam);
        }

        public IActionResult DetailsDocent(int id)
        {
            Docent docent = _teamLogic.HaalDocentOpMetID(id);
            return View(docent);
        }

        public IActionResult Details(int id)
        {
            var teamLogic = new TeamLogic();
            Team geselecteerdTeam =
                teamLogic.TeamOphalenMetID(id); //.SingleOrDefault(o => o.Name == id); // there should be only one!

            /*if (geselecteerdTeam == null)
            {
                return NotFound();
            }
            else
            {
                geselecteerdTeam.CurriculumEigenaarNaam =
                teamLogic.CurriculumEigenaarNaamMetCurriculumEigenaarId(geselecteerdTeam.CurriculumEigenaarID);
                geselecteerdTeam.TeamleiderNaam = teamLogic.TeamleiderNaamMetTeamleiderId(geselecteerdTeam.TeamleiderID);
            }*/

            return View(geselecteerdTeam);

            // Team selectedTeamteam = _teamLogic.TeamsOphalen()
        }

        public IActionResult VoegDocentToe()
        {
            // Haalt huidig teamid op
            string id = User.FindFirstValue(ClaimTypes.NameIdentifier);
            int teamid = _teamLogic.HaalTeamIDOpMetString(id);
            List<Docent> docentenZonderTeam = _teamLogic.HaalDocentenZonderTeamOp();
            var teams = new TeamViewModel { DocentenZonderTeam = docentenZonderTeam };
            Team team = _teamLogic.TeamOphalenMetID(teamid);
            teams.TeamID = teamid;
            return View(teams);
        }

        public IActionResult VoegDocentToeAanTeam([FromQuery] int teamid, [FromQuery] int docentid)
        {
            _teamLogic.VoegDocentToeAanTeam(docentid, teamid);
            return RedirectToAction("VoegDocentToe", "Team");
        }

        public IActionResult VerwijderUitTeam(int id)
        {
            try
            {
                _teamLogic.VerwijderDocentUitTeam(id);
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View("DocentenInTeam");
            }
        }

        public IActionResult MaakVacature()
        {
            string User_id = User.FindFirstValue(ClaimTypes.NameIdentifier);
            ViewBag.Taken = _teamLogic.HaalTakenOpVoorTeamleider(User_id);
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult MaakVacature(IFormCollection form)
        {
            Vacature vacature = new Vacature { Omschrijving = form["Omschrijving"] };
            if (form["naam"] == "")
            {
                vacature.Naam = null;
            }
            else
            {
                vacature.Naam = form["Naam"];
            }
            vacature.TaakID = Convert.ToInt32(form["TaakID"]);
            _vacatureLogic.VacatureOpslaan(vacature);
            return RedirectToAction("VacatureOverzicht");
        }

        public IActionResult VacatureOverzicht()
        {
            List<Vacature> vacatures;
            vacatures = _vacatureLogic.VacaturesOphalen();
            return View(vacatures);
        }

        public IActionResult DeleteVacature(int id)
        {
            //Delete Vacature van Db zonder view
            _vacatureLogic.DeleteVacature(id);
            return RedirectToAction("VacatureOverzicht");
        }

        public IActionResult VacatureDetails(int id)
        {
            // TODO: Opmaak Pagina Details aanpassen.
            Vacature vacature = _vacatureLogic.VacatureOphalen(id);
            return View(vacature);
        }
    }
}