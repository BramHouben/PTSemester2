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
            return TeamRepo.teams;
        }

        public List<Docent> DocentenOphalen()
        {
            return TeamRepo.docenten;
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
    }
}