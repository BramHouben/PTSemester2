using System.Collections.Generic;
using Data.Interfaces;
using Model;

namespace Data.Context
{
    public class TeamMemoryContext : ITeamContext
    {
        private List<Docent> docenten;
        private List<Team> teams;

        public List<Team> TeamsOphalen()
        {
            if (teams == null)
            {
                teams = new List<Team>
                {
                    new Team(1, 1, 1),
                    new Team(2, 2, 2)
                };
            }

            return teams;
        }

        public List<Docent> DocentInTeamOphalen()
        {
            docenten = new List<Docent>
            {
                new Docent(1, 1, 600),
                new Docent(2, 1, 500),
                new Docent(3, 2, 550)
            };
            return docenten;
        }

        public void DocentToevoegen(Docent docent)
        {
            docenten.Add(docent);
        }

        public void DocentVerwijderen(Docent docent)
        {
            docenten.Remove(docent);
        }

        public string TeamleiderNaamMetTeamleiderId(int teamleiderId)
        {
            switch (teamleiderId)
            {
                case 1:
                    return "Jantje de Boer";
                case 2:
                    return "Bram de Coenerguy";
                default:
                    return "Teamleider niet gevonden";

            }
        }

        public string CurriculumEigenaarNaamMetCurriculumEigenaarId(int curriculumeigenaarId)
        {
            switch (curriculumeigenaarId)
            {
                case 1:
                    return "Hans Klok";
                case 2:
                    return "Freek Vonk";
                default:
                    return "Curriculum Eigenaar Niet Gevonden";
            }
        }

        public Team TeamOphalenMetID(int id)
        {
            this.TeamsOphalen();
            id--;
            try
            {
                return TeamsOphalen()[id];
            }
            catch
            {
                // return empty team
                return null;
            }
        }
    }
}