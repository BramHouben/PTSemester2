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
            
            try
            {
                connectie = dbconn.GetConnString();
                if (connectie.State != ConnectionState.Open)
                {
                    connectie.Open();
                }
                var cmd = connectie.CreateCommand();
                cmd.Parameters.AddWithValue("@id", id);
                cmd.CommandText = "SELECT DocentID, TeamID, RuimteVoorInzet FROM Docent where TeamID = @id";
                
                var reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    Docent docent = new Docent();
                    docent.DocentId = (int)reader["DocentID"]; 
                    docent.TeamId =(int) reader["TeamID"];
                    docent.RuimteVoorInzet =(int) reader["RuimteVoorInzet"];
                    //docent.MedewerkerId = (string) reader["MedewerkerID"];
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

        public void DocentVerwijderen(int DocentID)
        {
            try
            {
                connectie = dbconn.GetConnString();
                if (connectie.State != ConnectionState.Open)
                {
                    connectie.Open();
                }
                var cmd = connectie.CreateCommand();
                cmd.Parameters.AddWithValue("@docentid", DocentID);
                cmd.CommandText = "UPDATE Docent SET TeamID = NULL WHERE DocentID = @docentid";
                cmd.ExecuteNonQuery();
            }
            catch (SqlException Fout)
            {
                Console.WriteLine(Fout.Message);
            }
            finally
            {
                if (connectie.State != ConnectionState.Closed)
                {
                    connectie.Close();
                }

            }
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

        public void VerwijderDocentUitTeam(int TeamID, int DocentID)
        {
            try
            {
                connectie = dbconn.GetConnString();
                if(connectie.State != ConnectionState.Open)
                {
                    connectie.Open();
                }
                var cmd = connectie.CreateCommand();
                cmd.Parameters.AddWithValue("@teamid", TeamID);
                cmd.Parameters.AddWithValue("@docentid", DocentID);
                cmd.CommandText = "UPDATE Docent SET TeamID = NULL WHERE DocentID = @docentid";
                cmd.ExecuteNonQuery();
            }
            catch(SqlException Fout)
            {
                Console.WriteLine(Fout.Message);
            }
            finally
            {
                if(connectie.State != ConnectionState.Closed)
                {
                    connectie.Close();
                }
                
            }
        }

        public int haalTeamIdOp(string id)
        {
            int result = 0;
            try
            {
                connectie = dbconn.GetConnString();
                var cmd = connectie.CreateCommand();
                cmd.Parameters.AddWithValue("@idstring", id);
                cmd.CommandText = "SELECT TeamID FROM Team WHERE TeamLeiderID IN (SELECT TeamLeiderID FROM TeamLeider WHERE MedewerkerID = @stringid);";
                if(connectie.State != ConnectionState.Open)
                {
                    connectie.Open();
                }
                result = cmd.ExecuteNonQuery();

            }
            catch(SqlException Fout)
            {
                Console.WriteLine(Fout.Message);
                
            }
            finally
            {
                if(connectie.State != ConnectionState.Closed)
                {
                    connectie.Close();
                }
            }
            return result;
        }
    }
}
