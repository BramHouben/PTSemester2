using Data.Context;
using Model;
using System.Collections.Generic;
using Data.Interfaces;

namespace Data
{
    public class TeamRepository
    {
        private readonly ITeamContext IteamContext;

        public TeamRepository()
        {
            IteamContext = new TeamMemoryContext();
        }

        public List<Team> teams => IteamContext.TeamsOphalen();
        public List<Docent> docenten => IteamContext.DocentInTeamOphalen();
        public void VoegDocentToeAanTeam(Docent docent)
        {
            IteamContext.DocentToevoegen(docent);
        }
        public void VerwijderDocentUitTeam(Docent docent)
        {
            IteamContext.DocentVerwijderen(docent);
        }

        public string TeamleiderNaamMetTeamleiderId(int teamleiderId)
        {
            return IteamContext.TeamleiderNaamMetTeamleiderId(teamleiderId);
        }
    }
}