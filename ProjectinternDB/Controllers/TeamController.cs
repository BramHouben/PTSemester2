using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Logic;
using Model;
using ProjectinternDB.Models;
using Microsoft.AspNetCore.Identity;
using System.Security.Claims;

using Microsoft.AspNetCore.Http;
using Microsoft.CodeAnalysis.CSharp.Syntax;

using Microsoft.AspNetCore.Authorization;


namespace ProjectinternDB.Controllers
{
    [Authorize(Roles = "Teamleider")]
    public class TeamController : Controller
    {
        private TeamLogic _teamLogic = new TeamLogic();
        private VacatureLogic _vacatureLogic = new VacatureLogic();

        public IActionResult Index()
        {
            IEnumerable<Team> teams= _teamLogic.TeamsOphalen();
            IEnumerable<Docent> docenten = _teamLogic.DocentenOphalen(1);

             return View(teams);
        }

        public IActionResult DocentenInTeam()
        {
            string id = User.FindFirstValue(ClaimTypes.NameIdentifier);
            int teamid = _teamLogic.haalTeamIDOpMetString(id);
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
        public IActionResult TeamOverzicht()
        {
            var teamLogic = new TeamLogic();
            var teams = new List<TeamViewModel>();
            /*foreach (var team in teamLogic.TeamsOphalen())
            {
                teams.Add(new TeamViewModel
                {
                    TeamleiderID = team.TeamleiderID,
                    CurriculumEigenaarID = team.CurriculumEigenaarID,
                    Docenten = teamLogic.DocentenOphalen(team.TeamId),
                    Teams = teamLogic.TeamsOphalen(),
                    TeamleiderNaam = teamLogic.TeamleiderNaamMetTeamleiderId(1)
                });
            }*/
            int id = teamLogic.haalTeamIDOpMetString(User.FindFirstValue(ClaimTypes.NameIdentifier));
            var team = teamLogic.TeamOphalenMetID(id);

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

        public IActionResult Details(int id)
        {
            var teamLogic = new TeamLogic();
            Team geselecteerdTeam = teamLogic.TeamOphalenMetID(id); //.SingleOrDefault(o => o.Name == id); // there should be only one!
           
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
            int teamid = _teamLogic.haalTeamIDOpMetString(id);
            List<Docent> docentenZonderTeam = _teamLogic.haalDocentenZonderTeamOp();
            var teams = new TeamViewModel();
            teams.DocentenZonderTeam = docentenZonderTeam;
            Team team = _teamLogic.TeamOphalenMetID(teamid);
            teams.TeamID = teamid;
            return View(teams);
        }

        public IActionResult VoegDocentToeAanTeam([FromQuery]int teamid, [FromQuery]int docentid)
        {
            _teamLogic.VoegDocentToeAanTeam(docentid, teamid);
            return RedirectToAction("VoegDocentToe", "Team");
        }

        public IActionResult MaakVacature()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult MaakVacature(IFormCollection form)
        {
            Vacature vacature = new Vacature();
            vacature.Omschrijving = form["Omschrijving"];
            if (form["naam"] == "")
            {
                vacature.Naam = null;
            }
            else
            {
            vacature.Naam = form["Naam"];
            }

            vacature.OnderwijstaakID = Convert.ToInt32(form["OnderwijstaakID"]);
            _vacatureLogic.VacatureOpslaan(vacature);
           return RedirectToAction("Index");
        }
    }
}