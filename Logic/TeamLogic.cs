using Data;
using System;
using Model;
using System.Collections.Generic;

namespace Logic
{
    public class TeamLogic
    {
        private static TeamRepository TeamRepo = new TeamRepository();

        public List<Team> TeamsOphalen()
        {
            return TeamRepo.Teams;
        }

        public List<Docent> DocentenOphalen()
        {
            return TeamRepo.Docenten;
        }

        public void VoegDocentToeAanTeam(Docent docent)
        {
            TeamRepo.VoegDocentToeAanTeam(docent);
        }
        public void VerwijderDocentUitTeam(Docent docent)
        {
            TeamRepo.VerwijderDocentUitTeam(docent);
        }

        public string TeamleiderNaamMetTeamleiderId(int teamleiderId)
        {
            return TeamRepo.TeamleiderNaamMetTeamleiderId(teamleiderId);
        }
        public Team TeamOphalenMetID(int ID)
        {
            return TeamRepo.TeamOphalenMetID(ID);
        }
        public string CurriculumEigenaarNaamMetCurriculumEigenaarId(int curriculumeigenaarId)
        {
            return TeamRepo.CurriculumEigenaarNaamMetCurriculumEigenaarId(curriculumeigenaarId);
        }
    }
}