using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Diagnostics;
using System.Text;
using Data.Interfaces;
using Model;
using Model.Onderwijsdelen;

namespace Data.Context
{
    public class TeamsqlContext : ITeamContext
    {
        private SqlConnection connectie;
        private DBconn dbconn = new DBconn();

        public List<Team> TeamsOphalen()
        {
            List<Team> teamList = new List<Team>();
            try
            {
                using (SqlConnection conn = dbconn.GetConnString())
                {
                    conn.Open();
                    using (SqlCommand cmd = new SqlCommand("SELECT * FROM Team", conn))
                    {
                        using (SqlDataReader reader = cmd.ExecuteReader())
                        {
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
                    }
                }
            }
            catch (SqlException fout)
            {
                Debug.WriteLine(fout.Message);
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
                using (SqlConnection conn = dbconn.GetConnString())
                {
                    conn.Open();
                    using (SqlCommand cmd =
                        new SqlCommand("SELECT D.DocentID, D.TeamID ,(ANU.Voornaam + ' ' + ANU.Achternaam) as Naam, D.RuimteVoorInzet " +
                                       "FROM [dbo].[Docent] D " +
                                       "INNER JOIN [dbo].[AspNetUsers] ANU ON ANU.Id = D.MedewerkerID where TeamID = @id", conn))
                    {
                        cmd.Parameters.AddWithValue("@id", id);
                        using (SqlDataReader reader = cmd.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                Docent docent = new Docent { DocentId = (int)reader["DocentID"], TeamId = (int)reader["TeamID"] };

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
                    }
                }
            }
            catch (SqlException fout)
            {
                Debug.WriteLine(fout.Message);
            }
            return docentList;
        }

        public void VoegDocentToeAanTeam(int DocentID, int TeamID)
        {
            try
            {
                using (SqlConnection conn = dbconn.GetConnString())
                {
                    conn.Open();
                    using (SqlCommand cmd = new SqlCommand("UPDATE Docent SET TeamID = @TeamID WHERE DocentID = @DocentID", conn))
                    {
                        cmd.Parameters.AddWithValue("@TeamID", TeamID);
                        cmd.Parameters.AddWithValue("@DocentID", DocentID);
                        cmd.ExecuteNonQuery();
                    }
                }
            }
            catch (SqlException fout)
            {
                Debug.WriteLine(fout.Message);
            }
        }

        public void DocentVerwijderen(int DocentID)
        {
            try
            {
                using (SqlConnection conn = dbconn.GetConnString())
                {
                    conn.Open();
                    using (SqlCommand cmd = new SqlCommand("UPDATE Docent SET TeamID = NULL WHERE DocentID = @docentid", conn))
                    {
                        cmd.Parameters.AddWithValue("@docentid", DocentID);
                        cmd.ExecuteNonQuery();
                    }
                }
            }
            catch (SqlException fout)
            {
                Debug.WriteLine(fout.Message);
            }
        }

        public string TeamleiderNaamMetTeamleiderId(int teamleiderId)
        {
            string naam = null;
            try
            {
                using (SqlConnection conn = dbconn.GetConnString())
                {
                    conn.Open();
                    using (SqlCommand cmd = new SqlCommand("SELECT Naam FROM Docent WHERE MedewerkerID = (SELECT MedewerkerID FROM TeamLeider WHERE TeamleiderID = @TeamLeiderID)", conn))
                    {
                        cmd.Parameters.AddWithValue("@TeamLeiderID", teamleiderId);
                        using (SqlDataReader reader = cmd.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                naam = (string)reader["Naam"];
                            }
                        }
                    }
                }
            }
            catch (SqlException fout)
            {
                Debug.WriteLine(fout.Message);
            }
            return naam;
        }

        public Team TeamOphalenMetID(int id)
        {
            try
            {
                Team team = null;
                using (SqlConnection conn = dbconn.GetConnString())
                {
                    conn.Open();
                    using (SqlCommand cmd = new SqlCommand("SELECT T.*, (ANU.Voornaam + ' ' + ANU.Achternaam) as Naam FROM [dbo].[Team] T INNER JOIN [dbo].[TeamLeider] TL ON T.TeamLeiderID = TL.TeamLeiderID INNER JOIN [dbo].[AspNetUsers] ANU ON TL.MedewerkerID = ANU.Id  WHERE T.TeamID = @id", conn))
                    {
                        cmd.Parameters.AddWithValue("@id", id);
                        using (SqlDataReader reader = cmd.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                team = new Team
                                {
                                    TeamId = (int)reader["TeamID"],
                                    TeamleiderID = (int)reader["TeamLeiderID"],
                                    CurriculumEigenaarID = (int)reader["CurriculumEigenaarID"],
                                    TeamleiderNaam = (string)reader["Naam"]
                                };
                            }
                        }
                    }
                }
                return team;
            }
            catch (SqlException fout)
            {
                Debug.WriteLine(fout.Message);
                return null;
            }
        }

        public string CurriculumEigenaarNaamMetCurriculumEigenaarId(int curriculumeigenaarId)
        {
            string naam = null;
            try
            {
                using (SqlConnection conn = dbconn.GetConnString())
                {
                    conn.Open();
                    using (SqlCommand cmd =
                        new SqlCommand(
                            "SELECT Naam FROM Docent WHERE MedewerkerID = (SELECT MedewerkerID FROM CurriculumEigenaar WHERE CurriculumEigenaarID = @CurriculumEigenaarID)",
                            conn))
                    {
                        cmd.Parameters.AddWithValue("@CurriculumEigenaarID", curriculumeigenaarId);
                        using (SqlDataReader reader = cmd.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                naam = (string)reader["Naam"];
                            }
                        }
                    }
                }
            }
            catch (Exception fout)
            {
                Debug.WriteLine(fout.Message);
            }
            return naam;
        }

        public void VerwijderDocentUitTeam(int TeamID, int DocentID)
        {
            try
            {
                using (SqlConnection conn = dbconn.GetConnString())
                {
                    conn.Open();
                    using (SqlCommand cmd = new SqlCommand("UPDATE Docent SET TeamID = NULL WHERE DocentID = @docentid", conn))
                    {
                        cmd.Parameters.AddWithValue("@teamid", TeamID);
                        cmd.Parameters.AddWithValue("@docentid", DocentID);
                        cmd.ExecuteNonQuery();
                    }
                }
            }
            catch (SqlException fout)
            {
                Debug.WriteLine(fout.Message);
            }
        }

        public int HaalTeamIdOp(string id)
        {
            int result = 0;
            try
            {
                using (SqlConnection conn = dbconn.GetConnString())
                {
                    conn.Open();
                    using (SqlCommand cmd = new SqlCommand("SELECT TeamID FROM Team WHERE TeamLeiderID = (SELECT TeamLeiderID FROM TeamLeider WHERE MedewerkerID = @stringid);", conn))
                    {
                        cmd.Parameters.AddWithValue("@stringid", id);
                        result = (int)cmd.ExecuteScalar();
                    }

                }
            }
            catch (Exception fout) when (fout is SqlException || fout is NullReferenceException)
            {
                Debug.WriteLine(fout.Message);
            }
            return result;
        }

        public List<Docent> HaalDocentenZonderTeamOp()
        {
            List<Docent> docenten = new List<Docent>();
            try
            {
                using (SqlConnection conn  = dbconn.GetConnString())
                {
                    conn.Open();
                    using (SqlCommand cmd =  new SqlCommand("SELECT D.DocentID, (ANU.Voornaam + ' ' + ANU.Achternaam) as Naam, D.RuimteVoorInzet FROM [dbo].[Docent] D INNER JOIN [dbo].[AspNetUsers] ANU ON ANU.Id = D.MedewerkerID WHERE D.TeamID is null", conn))
                    {
                        using (SqlDataReader reader = cmd.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                Docent docent = new Docent { DocentId = (int)reader["DocentID"], Naam = (string)reader["Naam"] };
                                if (DBNull.Value.Equals(reader["RuimteVoorInzet"]))
                                {
                                    docent.RuimteVoorInzet = 0;
                                }
                                else
                                {
                                    docent.RuimteVoorInzet = (int)reader["RuimteVoorInzet"];
                                }
                                docenten.Add(docent);
                            }
                        }
                    }
                }
            }
            catch (SqlException fout)
            {
                Debug.WriteLine(fout.Message);
                return null;
            }
            return docenten;
        }

        public List<Taak> GetTaken(int docentid)
        {
            var taken = new List<Taak>();
            try
            {
                using (SqlConnection conn = dbconn.GetConnString())
                {
                    conn.Open();
                    using (SqlCommand cmd = new SqlCommand("SELECT T.TaakId, T.TaakNaam FROM Taak T WHERE NOT EXISTS(SELECT Taak_id, DocentID FROM GefixeerdeTaken GT WHERE T.TaakId=GT.Taak_id AND GT.DocentID = @did) AND EXISTS (SELECT TaakID FROM Bekwaamheid B WHERE T.TaakId=B.TaakID AND B.Docent_id = @did)", conn))
                    {
                        cmd.Parameters.AddWithValue("@did", docentid);
                        using (SqlDataReader reader = cmd.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                var taak = new Taak()
                                {
                                    TaakId = (int)reader["TaakId"],
                                    TaakNaam = reader["TaakNaam"]?.ToString(),
                                };
                                taken.Add(taak);
                            }
                        }
                    }
                }
            }
            catch (SqlException fout)
            {
                Debug.WriteLine(fout.Message);
            }

            return taken;
        }

        public Docent HaalDocentOpMetID(int id)
        {
            Docent docent = new Docent();
            try
            {
                using (SqlConnection conn = dbconn.GetConnString())
                {
                    conn.Open();
                    using (SqlCommand cmd = new SqlCommand("SELECT D.DocentID, D.TeamID, D.RuimteVoorInzet,(ANU.Voornaam + ' ' + ANU.Achternaam) as Naam, D.RuimteVoorInzet FROM [dbo].[Docent] D INNER JOIN [dbo].[AspNetUsers] ANU ON ANU.Id = D.MedewerkerID where d.DocentID = @DocentID", conn))
                    {
                        cmd.Parameters.AddWithValue("@DocentID", id);
                        using (SqlDataReader reader = cmd.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                docent = new Docent { DocentId = (int)reader["DocentID"], Naam = (string)reader["Naam"] };
                                if (!(DBNull.Value.Equals(reader["TeamID"])))
                                {
                                    docent.TeamId = (int)reader["TeamID"];
                                }

                                if (!(DBNull.Value.Equals(reader["RuimteVoorInzet"])))
                                {
                                    docent.RuimteVoorInzet = (int)reader["RuimteVoorInzet"];
                                }
                            }
                        }
                    }
                }
                return docent;
            }
            catch (SqlException fout)
            {
                Debug.WriteLine(fout.Message);
                return null;
            }
        }

        public List<Taak> HaalTakenOpVoorTeamleider(string medewerkerid)
        {
            List<Taak> taken = new List<Taak>();
            try
            {
                using (SqlConnection con = dbconn.GetConnString())
                {
                    con.Open();
                    using (SqlCommand cmd = new SqlCommand("Select Taak.TaakId, Taak.TaakNaam from Taak  Inner JOIN Onderdeel on Onderdeel.OnderdeelId = taak.OnderdeelId Inner Join Eenheid on Eenheid.EenheidId = Onderdeel.EenheidId Inner Join Traject on Traject.TrajectId = Eenheid.TrajectId Inner Join Team on Team.TeamID =Traject.TeamID WHERE Team.TeamID = (Select TeamID From Team Where TeamLeiderID = (Select TeamLeiderID From TeamLeider where TeamLeider.MedewerkerID = @MedewerkerID))", con))
                    {
                        cmd.Parameters.AddWithValue("@MedewerkerID", medewerkerid);
                        using (SqlDataReader reader = cmd.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                Taak taak = new Taak();
                                taak.TaakId = (int)reader["TaakId"];
                                taak.TaakNaam = (string)reader["TaakNaam"];
                                taken.Add(taak);
                            }
                        }
                    }
                }
                return taken;
            }
            catch (SqlException fout)
            {
                Debug.WriteLine(fout.Message);
                return taken;
            }
        }
    }
}