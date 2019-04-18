using Data;
using System;
using Model;
using System.Collections.Generic;
using Model.Onderwijsdelen;

namespace Logic
{
    public class TeamLogic
    {
        private static TeamRepository TeamRepo = new TeamRepository();

        public List<Team> TeamsOphalen()
        {
            return TeamRepo.Teams;
        }

        public List<Taak> GetTaken()
        {
            return TeamRepo.Taken;
        }

        public List<Docent> DocentenOphalen(int teamid)
        {
            return TeamRepo.DocentenInTeamOphalen(teamid);
        }
        public void VoegDocentToeAanTeam(int DocentID, int TeamID)
        {
            TeamRepo.VoegDocentToeAanTeam(DocentID, TeamID);
        }
        public void VerwijderDocentUitTeam(int docentid)
        {
            TeamRepo.VerwijderDocentUitTeam(docentid);
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

        public int haalTeamIDOpMetString(string id)
        {
            return TeamRepo.haalTeamIdOpMetIDString(id);
        }

        public List<Docent> haalDocentenZonderTeamOp()
        {
            return TeamRepo.haalDocentenZonderTeamOp();
        }
    }
}