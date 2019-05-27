using Data.Context;
using Model;
using System.Collections.Generic;
using Data.Interfaces;
using Model.Onderwijsdelen;

namespace Data
{
    public class TeamRepository
    {
        private readonly ITeamContext IteamContext;

        public TeamRepository(ITeamContext teamContext)
        {
            IteamContext = teamContext;
        }

        public List<Team> Teams => IteamContext.TeamsOphalen();

        public List<Taak> Taken(int docentid) => IteamContext.GetTaken(docentid);

        public List<Docent> DocentenInTeamOphalen(int teamid)
        {
           return IteamContext.DocentInTeamOphalen(teamid);
        }
       
        public void VoegDocentToeAanTeam(int DocentID, int TeamID)
        {
            IteamContext.VoegDocentToeAanTeam(DocentID, TeamID);
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

        public int HaalTeamIdOpMetIDString(string id)
        {
            return IteamContext.HaalTeamIdOp(id);
        }

        public List<Docent> HaalDocentenZonderTeamOp()
        {
            return IteamContext.HaalDocentenZonderTeamOp();
        }

        public Docent HaalDocentOpMetID(int id)
        {
            return IteamContext.HaalDocentOpMetID(id);
        }
    }
}