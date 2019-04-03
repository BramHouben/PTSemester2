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

        public List<Team> Teams => IteamContext.TeamsOphalen();
        public List<Docent> Docenten => IteamContext.DocentInTeamOphalen();
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

        public Team TeamOphalenMetID(int ID)
        {
            return IteamContext.TeamOphalenMetID(ID);
        }

        public string CurriculumEigenaarNaamMetCurriculumEigenaarId(int curriculumeigenaarId)
        {
            return IteamContext.CurriculumEigenaarNaamMetCurriculumEigenaarId(curriculumeigenaarId);
        }
    }
}