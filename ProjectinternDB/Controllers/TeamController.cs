using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Logic;
using Model;
using ProjectinternDB.Models;

namespace ProjectinternDB.Controllers
{
    public class TeamController : Controller
    {

        private TeamLogic _teamLogic = new TeamLogic();

        public IActionResult Index()
        {

            return View(_teamLogic.TeamsOphalen());
        }

        public IActionResult DocentenToevoegen(TeamViewModel team)
        {
            foreach (var docent in team.Docenten)
            {
                _teamLogic.VoegDocentToeAanTeam(docent);
            }
            

            return RedirectToAction("Index");
        }
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
                    Docenten = teamLogic.DocentenOphalen(),
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
    }
}