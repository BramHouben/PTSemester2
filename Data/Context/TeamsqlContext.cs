using System;
using System.Collections.Generic;
using System.Data;
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
            try
            {
                connectie = dbconn.GetConnString();
                // Verhelpt error Connection is still open
                if (connectie.State != ConnectionState.Open)
                {
                    connectie.Open();
                }



                var cmd = new SqlCommand("SELECT * FROM Team", connectie);
                var reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    var team = new Team();


                    team.TeamId = (int) reader["TeamID"];
                    team.TeamleiderID = (int) reader["TeamLeiderID"];
                    team.CurriculumEigenaarID = (int) reader["CurriculumEigenaarID"];

                    teamList.Add(team);
                }
            }
            catch
            {
            }
            finally
            {
                try
                {
                    if (connectie.State != ConnectionState.Closed)
                    {
                        connectie.Close();
                    }
                }
                catch
                {
                }
            }
           
            foreach (Team team in teamList)
            {
                team.Docenten = DocentInTeamOphalen(team.TeamId);
            }
            return teamList;

        }

        public List<Docent> DocentInTeamOphalen(int id)
        {
            List<Docent> docentList = new List<Docent>();
            SqlDataReader reader = null;
            try
            {
                connectie = dbconn.GetConnString();
                if (connectie.State != ConnectionState.Open)
                {
                    connectie.Open();
                }

                var cmd = new SqlCommand("SELECT * FROM Team", connectie);
               reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    var docent = new Docent((int) reader["DocentID"], (int) reader["TeamID"],
                        (int) reader["RuimteVoorInzet"]);
                    docent.MedewerkerId = (string) reader["MedewerkerID"];
                    docent.Naam = (string) reader["Naam"];
                    docentList.Add(docent);
                }
            }
            catch
            {
                // Er is iets fout gegaan met de database connectie. Waarschijnlijk staat er geen docent in de database.
            }
            finally
            {
                try
                {
                    if (connectie.State != ConnectionState.Closed)
                    {
                        connectie.Close();
                    }
                }
                catch
                {
                }
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
            SqlDataReader reader = null;
            try
            {
                connectie = dbconn.GetConnString();
                if (connectie.State != ConnectionState.Open)
                {
                    connectie.Open();
                }

                Team team = null;
                var cmd = new SqlCommand("SELECT * FROM Team WHERE ID = @id", connectie);
                cmd.Parameters.AddWithValue("@id", id);
                reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    team = new Team((int) reader["TeamID"], (int) reader["TeamLeiderID"],
                        (int) reader["CurriculumEigenaarID"]);

                }
           
                return team;
            }
            catch
            {
                return
                    null; // Er is iets fout gegaan met de database connectie. Waarschijnlijk staat er geen docent in de database.
            }
            finally
            {
                try
                {
                    if (connectie.State != ConnectionState.Closed)
                    {
                        connectie.Close();
                    }
                }
                catch
                {
                }
            }
        }

        public string CurriculumEigenaarNaamMetCurriculumEigenaarId(int curriculumeigenaarId)
        {
            throw new NotImplementedException();
        }
    }
}
