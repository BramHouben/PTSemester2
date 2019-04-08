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
                    var team = new Team
                    {
                        TeamId = (int) reader["TeamID"],
                        TeamleiderID = (int) reader["TeamLeiderID"],
                        CurriculumEigenaarID = (int) reader["CurriculumEigenaarID"]
                    };



                    teamList.Add(team);
                }
            }
            catch
            {
                Console.WriteLine("Geen gegevens in Team Tabel of SQL connectie error");
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
                    Console.WriteLine("Er is geprobeerd een connectie met status null te sluiten");
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
                Console.WriteLine("Er is iets fout gegaan met de database connectie. Waarschijnlijk ontbreken er gegevens in de team tabel.");
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
                    Console.WriteLine("Er is geprobeerd een connectie met status null te sluiten");
                }
            }

            return docentList;
        }


        public void DocentToevoegen(Docent doc)
        {
            SqlDataReader reader = null;
            try
            {
                connectie = dbconn.GetConnString();
                if (connectie.State != ConnectionState.Open)
                {
                    connectie.Open();
                }
                var cmd = connectie.CreateCommand();
                cmd.CommandText = "UPDATE Docent SET TeamID = @TeamID WHERE DocentID = @DocentID;";
                cmd.Parameters.AddWithValue("@TeamID", doc.TeamId);
                cmd.Parameters.AddWithValue("@DocentID", doc.DocentId);
                reader = cmd.ExecuteReader();
            }
            catch
            {
                Console.WriteLine("Er is iets misgegaan met het updaten van het docent id");
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
                    Console.WriteLine("Er is geprobeerd een connectie met status null te sluiten");
                }
            }
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
            SqlDataReader reader = null;
            string naam = null;
            try
            {
                connectie = dbconn.GetConnString();
                if (connectie.State != ConnectionState.Open)
                {
                    connectie.Open();
                }
                var cmd = new SqlCommand("SELECT Naam FROM Docent WHERE MedewerkerID = (SELECT MedewerkerID FROM TeamLeider WHERE TeamleiderID = @TeamLeiderID)", connectie);
                cmd.Parameters.AddWithValue("@TeamLeiderID", teamleiderId);
                reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                   naam = (string)reader["Naam"];
                }

               
            }
            catch
            {
                Console.WriteLine("Er is iets fout gegaan met de database connectie. Waarschijnlijk is er geen record met het opgegeven teamleiderID.");
                
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
                    Console.WriteLine("Er is geprobeerd een connectie met status null te sluiten");
                }
            }
            return naam;
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
                var cmd = new SqlCommand("SELECT * FROM [dbo].[Team] WHERE TeamID = @id", connectie);
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
                Console.WriteLine("Er is iets fout gegaan met de database connectie. Waarschijnlijk is er geen record met het opgegeven ID.");
                return
                    null;
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
                    Console.WriteLine("Er is geprobeerd een connectie met status null te sluiten");
                }
            }
        }

        public string CurriculumEigenaarNaamMetCurriculumEigenaarId(int curriculumeigenaarId)
        {
            SqlDataReader reader = null;
            string naam = null;
            try
            {
                connectie = dbconn.GetConnString();
                if (connectie.State != ConnectionState.Open)
                {
                    connectie.Open();
                }
                var cmd = new SqlCommand("SELECT Naam FROM Docent WHERE MedewerkerID = (SELECT MedewerkerID FROM CurriculumEigenaar WHERE CurriculumEigenaarID = @CurriculumEigenaarID)", connectie);
                cmd.Parameters.AddWithValue("@CurriculumEigenaarID", curriculumeigenaarId);
                reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    naam = (string)reader["Naam"];
                }


            }
            catch
            {
                Console.WriteLine("Er is iets fout gegaan met de database connectie. Waarschijnlijk is er geen record met het opgegeven teamleiderID.");

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
                    Console.WriteLine("Er is geprobeerd een connectie met status null te sluiten");
                }
            }
            return naam;
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
                cmd.Parameters.AddWithValue("@stringid", id);
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
