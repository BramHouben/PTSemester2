using System.Collections.Generic;
using Data.Interfaces;
using Model;

namespace Data.Context
{
    public class TeamMemoryContext : ITeamContext
    {
        private List<Docent> docenten;
        public List<Team> TeamsOphalen()
        {

            List<Team> teams = new List<Team>
            {
                new Team(1, 1, 1),
                new Team(2, 2, 2)
            };

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
    }
}