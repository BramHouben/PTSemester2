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
                        TeamId = (int)reader["TeamID"],
                        TeamleiderID = (int)reader["TeamLeiderID"],
                        CurriculumEigenaarID = (int)reader["CurriculumEigenaarID"]
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
                cmd.CommandText = "SELECT DocentID, TeamID, RuimteVoorInzet,Naam FROM Docent where TeamID = @id";

                var reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    Docent docent = new Docent();

                    docent.DocentId = (int)reader["DocentID"];
                    docent.TeamId = (int)reader["TeamID"];
                    if (DBNull.Value.Equals(reader["RuimteVoorInzet"]))
                    {
                        docent.RuimteVoorInzet = 0;
                    }
                    else
                    {
                        docent.RuimteVoorInzet = (int)reader["RuimteVoorInzet"];
                    }
                    docent.Naam = (string)reader["Naam"];
                    
                    //docent.MedewerkerId = (string) reader["MedewerkerID"];
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


        public void VoegDocentToeAanTeam(int DocentID, int TeamID)
        {
            //SqlDataReader reader = null;
            try
            {
                connectie = dbconn.GetConnString();
                if (connectie.State != ConnectionState.Open)
                {
                    connectie.Open();
                }
                var cmd = connectie.CreateCommand();
                cmd.CommandText = "UPDATE Docent SET TeamID = @TeamID WHERE DocentID = @DocentID;";
                cmd.Parameters.AddWithValue("@TeamID", TeamID);
                cmd.Parameters.AddWithValue("@DocentID", DocentID);
                cmd.ExecuteNonQuery();
            }
            catch
            {
                Console.WriteLine("Er is iets misgegaan met het updaten van het Team id");
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
                var cmd = new SqlCommand("SELECT T.*, (ANU.Voornaam + ' ' + ANU.Achternaam) as Naam FROM [dbo].[Team] T INNER JOIN [dbo].[TeamLeider] TL ON T.TeamLeiderID = TL.TeamLeiderID INNER JOIN [dbo].[AspNetUsers] ANU ON TL.MedewerkerID = ANU.Id  WHERE T.TeamID = @id", connectie);
                cmd.Parameters.AddWithValue("@id", id);
                reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    team = new Team();
                    team.TeamId = (int)reader["TeamID"];
                    team.TeamleiderID = (int)reader["TeamLeiderID"];
                    team.CurriculumEigenaarID = (int)reader["CurriculumEigenaarID"];
                    team.TeamleiderNaam = (string)reader["Naam"];
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
                if (connectie.State != ConnectionState.Open)
                {
                    connectie.Open();
                }
                var cmd = connectie.CreateCommand();
                cmd.Parameters.AddWithValue("@teamid", TeamID);
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

        public int haalTeamIdOp(string id)
        {
            int result = 0;
            try
            {
                connectie = dbconn.GetConnString();
                var cmd = connectie.CreateCommand();
                cmd.Parameters.AddWithValue("@stringid", id);
                cmd.CommandText = "SELECT TeamID FROM Team WHERE TeamLeiderID = (SELECT TeamLeiderID FROM TeamLeider WHERE MedewerkerID = @stringid);";
                if (connectie.State != ConnectionState.Open)
                {
                    connectie.Open();
                }
                result = (int)cmd.ExecuteScalar();
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
            return result;
        }

        public List<Docent> haalDocentenZonderTeamOp()
        {
            List<Docent> docenten = new List<Docent>();
            try
            {
                connectie = dbconn.GetConnString();
                connectie.Open();
                var cmd = connectie.CreateCommand();
                cmd.CommandText = "SELECT D.DocentID, (ANU.Voornaam + ' ' + ANU.Achternaam) as Naam, D.RuimteVoorInzet FROM [dbo].[Docent] D INNER JOIN [dbo].[AspNetUsers] ANU ON ANU.Id = D.MedewerkerID WHERE D.TeamID is null";
                var reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    Docent docent = new Docent();
                    docent.DocentId = (int)reader["DocentID"];
                    docent.Naam = (string)reader["Naam"];
                    if(DBNull.Value.Equals(reader["RuimteVoorInzet"]))
                    {
                        docent.RuimteVoorInzet = 0;
                    }
                    else
                    {
                        docent.RuimteVoorInzet = (int)reader["RuimteVoorInzet"];
                    }
                    
                    docenten.Add(docent);
                }
                return docenten;
            }
            catch (SqlException Fout)
            {
                Console.WriteLine(Fout.Message);
                return null;
            }
            finally
            {
                connectie.Close();
            }
        }
    }
}