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

namespace ProjectinternDB.Controllers
{
    public class TeamController : Controller
    {

        private TeamLogic _teamLogic = new TeamLogic();

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
            foreach (var team in teamLogic.TeamsOphalen())
            {
                teams.Add(new TeamViewModel
                {
                    TeamleiderID = team.TeamleiderID,
                    CurriculumEigenaarID = team.CurriculumEigenaarID,
                    Docenten = teamLogic.DocentenOphalen(team.TeamId),
                    Teams = teamLogic.TeamsOphalen(),
                    TeamleiderNaam = teamLogic.TeamleiderNaamMetTeamleiderId(1)
                });
            }

            return View(teamLogic.TeamsOphalen());
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
           
            if (geselecteerdTeam == null)
            {
                return NotFound();
            }
            else
            {
                geselecteerdTeam.CurriculumEigenaarNaam =
                teamLogic.CurriculumEigenaarNaamMetCurriculumEigenaarId(geselecteerdTeam.CurriculumEigenaarID);
                geselecteerdTeam.TeamleiderNaam = teamLogic.TeamleiderNaamMetTeamleiderId(geselecteerdTeam.TeamleiderID);
            }
            
            return View(geselecteerdTeam);
            
            // Team selectedTeamteam = _teamLogic.TeamsOphalen()
        }

    

        public IActionResult VoegDocentToe()
        {
            // Haalt huidig teamid op
            string id = User.FindFirstValue(ClaimTypes.NameIdentifier);
            int teamid = _teamLogic.haalTeamIDOpMetString(id);
            //
            var teams = new TeamViewModel();
            Team team = _teamLogic.TeamOphalenMetID(teamid);
            teams.Docenten = _teamLogic.DocentenOphalen(teamid) as List<Docent>;
            teams.DocentenZonderTeam = _teamLogic.DocentenOphalen(teamid) as List<Docent>;
            teams.TeamID = teamid;
            return View(teams);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult VoegToeAanTeam(int teamid, int medewerkerid)
        {
            throw new Exception(teamid.ToString() + medewerkerid.ToString());
        }
    }
}