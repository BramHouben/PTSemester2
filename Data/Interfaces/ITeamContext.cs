using System;
using System.Collections.Generic;
using System.Text;
using Model;

namespace Data.Interfaces
{
    public interface ITeamContext
    {
        List<Team>TeamsOphalen();
        List<Docent> DocentInTeamOphalen();

        void DocentToevoegen(Docent doc);
        void DocentVerwijderen(Docent docent);
        string TeamleiderNaamMetTeamleiderId(int teamleiderId);
    }
}
