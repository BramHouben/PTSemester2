using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Text;
using Data.Interfaces;
using Model;

namespace Data.Context
{
    class TeamsqlContext : ITeamContext
    {
        private SqlConnection connectie;

        private DBconn dbconn = new DBconn();


        public List<Team> TeamsOphalen()
        {

            List<Team> teamList = new List<Team>();

            connectie = dbconn.GetConnString();
            connectie.Open();

            var cmd = new SqlCommand("SELECT * FROM Team", connectie);
            var reader = cmd.ExecuteReader();

            while (reader.Read())
            {
                var team = new Team();


                team.TeamId = (int)reader["TeamID"];
                team.TeamleiderID = (int)reader["TeamLeiderID"];
                team.CurriculumEigenaarID = (int)reader["CurriculumEigenaarID"];

                teamList.Add(team);
            }
            connectie.Close();
            return teamList;

        }

        public List<Docent> DocentInTeamOphalen()
        {
            List<Docent> docentList = new List<Docent>();

            try
            {
                connectie = dbconn.GetConnString();
                connectie.Open();

                var cmd = new SqlCommand("SELECT * FROM Team", connectie);
                var reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    var docent = new Docent((int)reader["DocentID"], (int)reader["TeamID"],
                        (int)reader["RuimteVoorInzet"])
                    {

                    };

                    docent.MedewerkerId = (string)reader["MedewerkerID"];
                    docent.Naam = (string)reader["Naam"];
                    docentList.Add(docent);
                }

                connectie.Close();
            }
            catch
            {
                // Er is iets fout gegaan met de database connectie. Waarschijnlijk staat er geen docent in de database.
            }

            return docentList;
        }




        public void DocentToevoegen(Docent doc)
        {
            throw new NotImplementedException();
        }

        public void DocentVerwijderen(Docent docent)
        {
            throw new NotImplementedException();
        }

        public string TeamleiderNaamMetTeamleiderId(int teamleiderId)
        {
            throw new NotImplementedException();
        }

        public Team TeamOphalenMetID(int id)
        {
            throw new NotImplementedException();
        }

        public string CurriculumEigenaarNaamMetCurriculumEigenaarId(int curriculumeigenaarId)
        {
            throw new NotImplementedException();
        }
    }
}
