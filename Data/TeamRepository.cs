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
            IteamContext = new TeamsqlContext();
        }

        public List<Team> Teams => IteamContext.TeamsOphalen();

        public List<Docent> DocentenInTeamOphalen(int teamid)
        {
           return IteamContext.DocentInTeamOphalen(teamid);
        }
       
        public void VoegDocentToeAanTeam(Docent docent)
        {
            IteamContext.DocentToevoegen(docent);
        }
        public void VerwijderDocentUitTeam(int DocentID)
        {
            IteamContext.DocentVerwijderen(DocentID);
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

        public int haalTeamIdOpMetIDString(string id)
        {
            return IteamContext.haalTeamIdOp(id);
        }
    }
}