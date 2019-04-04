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

        void DocentToevoegen(Docent doc);
        void DocentVerwijderen(int DocentID);
        string TeamleiderNaamMetTeamleiderId(int teamleiderId);
        Team TeamOphalenMetID(int id);
        string CurriculumEigenaarNaamMetCurriculumEigenaarId(int curriculumeigenaarId);
        int haalTeamIdOp(string id);
        //void VerwijderDocentUitTeam(int TeamID, int DocentID);
    }
}
