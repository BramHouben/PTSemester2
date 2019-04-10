using System;
using System.Collections.Generic;
using System.Text;
using Model;

namespace Data.Interfaces
{
    public interface ITeamContext
    {
        List<Team>TeamsOphalen();
        List<Docent> DocentInTeamOphalen(int id);

        void VoegDocentToeAanTeam(int DocentID, int TeamID);
        void DocentVerwijderen(int DocentID);
        string TeamleiderNaamMetTeamleiderId(int teamleiderId);
        Team TeamOphalenMetID(int id);
        string CurriculumEigenaarNaamMetCurriculumEigenaarId(int curriculumeigenaarId);
        int haalTeamIdOp(string id);
        List<Docent> haalDocentenZonderTeamOp();
        //void VerwijderDocentUitTeam(int TeamID, int DocentID);
    }
}
