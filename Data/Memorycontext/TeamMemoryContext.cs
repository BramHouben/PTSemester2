using Data.Interfaces;
using Model;
using Model.Onderwijsdelen;
using System;
using System.Collections.Generic;

namespace Data.Context
{
    public class TeamMemoryContext : ITeamContext
    {
        public List<Team> teams = new List<Team>();
        private List<Taak> taken;
        private List<Docent> docenten;

        public TeamMemoryContext()
        {
            docenten = new List<Docent>
            {
                new Docent(1, 1, 600, "Jan"),
                new Docent(2, 1, 500, "Kees"),
                new Docent(3, 2, 550, "Klaas")
            };
            teams.Add(new Team(1, 1, 1));
            teams.Add(new Team(2, 2, 2));
        }


        public List<Team> TeamsOphalen()
        {
            return teams;
        }

        public List<Docent> DocentInTeamOphalen(int id)
        {
            return docenten;
        }

        public void VoegDocentToeAanTeam(int docentID, int TeamID)
        {
            foreach (Team team in teams)
            {
                if (team.TeamId == TeamID)
                {
                    team.Docenten.Add(new Docent(docentID, TeamID, 0, ""));
                }
            }
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
            foreach (Team team in teams)
            {
                if (team.TeamId == id)
                {
                    return team;
                }
            }
            return null;
            
        }

        public void DocentVerwijderen(int DocentID)
        {
          
            try
            {
                for (int i = 0; i < docenten.Count; i++)
                {
                    if (docenten[i].DocentId == DocentID)
                    {
                        docenten.RemoveAt(i);
                    }
                }
            }
            catch
            {
                Console.WriteLine("Docent verwijderen mislukt");
            }
        }

        public int HaalTeamIdOp(string id)
        {
            return 1;
        }

        public List<Docent> HaalDocentenZonderTeamOp()
        {
            List<Docent> docentenlijst = new List<Docent>();
            foreach (Docent docent in docenten)
            {
                if (docent.TeamId == null)
                {
                    docentenlijst.Add(docent);
                }
            }

            return HaalDocentenZonderTeamOp();
        }

        public List<Taak> GetTaken(int docentid)
        {
            taken = new List<Taak>();
            {
                new Taak(1, "Vingerverven");
                new Taak(2, "LP");
                new Taak(3, "Proftaak");
            }

            return taken;
        }

        public Docent HaalDocentOpMetID(int id)
        {
            throw new NotImplementedException();
        }
    }
}