using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Diagnostics;
using System.Text;
using Data.Interfaces;
using Model;

namespace Data.Context
{
    class FixerenSQLContext : IFixerenContext
    {
        private SqlConnection connectie { get; }
        private DBconn dbconn = new DBconn();
        private string _connectie;

        public FixerenSQLContext()
        {
            connectie = dbconn.GetConnString();
        }

        public void TaakFixerenMetDocentID(int docentID, int taakID)
        {
            try
            {
                _connectie = dbconn.ReturnConnectionString();
                using (SqlConnection conn = new SqlConnection(_connectie))
                {
                    conn.Open();

                    using (SqlCommand cmd = new SqlCommand(
                        "INSERT INTO [dbo].[GefixeerdeTaken] (DocentID, Taak_ID) VALUES (@docent, @taak)", conn))
                    {
                        cmd.Parameters.Add(new SqlParameter("@docent", docentID));
                        cmd.Parameters.Add(new SqlParameter("@taak", taakID));
                        cmd.ExecuteNonQuery();
                    }
                }
            }
            catch (SqlException fout)
            {
                Debug.WriteLine(fout.Message);
            }
        }

        public void VerwijderGefixeerdeTaak(int fid)
        {
            try
            {
                _connectie = dbconn.ReturnConnectionString();
                using (SqlConnection conn = new SqlConnection(_connectie))
                {
                    conn.Open();

                    using (SqlCommand cmd = new SqlCommand(
                        "DELETE FROM [dbo].[GefixeerdeTaken] WHERE Fix_id = @fid", conn))
                    {
                        cmd.Parameters.Add(new SqlParameter("@fid", fid));
                        cmd.ExecuteNonQuery();
                    }
                }
            }
            catch (SqlException fout)
            {
                Debug.WriteLine(fout.Message);
            }
        }

        public void VeranderGefixeerdeTaak(int taakID, int docentID)
        {
            try
            {
                _connectie = dbconn.ReturnConnectionString();

                using (SqlConnection conn = new SqlConnection())
                {
                    conn.Open();

                    using (SqlCommand cmd = new SqlCommand(
                        "UPDATE [dbo].[GefixeerdeTaken] SET Taak_id = @taak, DocentID = @docent)", conn))
                    {
                        cmd.Parameters.Add(new SqlParameter("@taak", taakID));
                        cmd.Parameters.Add(new SqlParameter("@docent", docentID));
                        cmd.ExecuteNonQuery();
                    }
                }
            }

            catch (SqlException fout)
            {
                Debug.WriteLine(fout.Message);
            }
        }

        public List<GefixeerdeTaak> HaalAlleGefixeerdeTakenOp(string id)
        {
            List<GefixeerdeTaak> GefixeerdeTaken = new List<GefixeerdeTaak>();
            var teamId = 0;

            try
            {
                _connectie = dbconn.ReturnConnectionString();

                using (SqlConnection conn = new SqlConnection(_connectie))
                {
                    conn.Open();

                    using (SqlCommand cmd = new SqlCommand(
                        "SELECT T.TeamID FROM Team T INNER JOIN TeamLeider TL ON TL.TeamLeiderID=T.TeamLeiderID WHERE TL.MedewerkerID=@id", conn))
                    {
                        cmd.Parameters.Add(new SqlParameter("@id", id));
                        teamId = (int)cmd.ExecuteScalar();
                    }


                    using (SqlCommand cmd = new SqlCommand(
                        "SELECT F.*, T.TaakNaam, (ANU.Voornaam + ' ' + ANU.Achternaam) as Naam FROM [dbo].[GefixeerdeTaken] F INNER JOIN [dbo].[Docent] D ON F.DocentID = D.DocentID INNER JOIN [dbo].[AspNetUsers] ANU ON D.MedewerkerID = ANU.Id INNER JOIN Taak T ON T.TaakId = F.Taak_id INNER JOIN Onderdeel O ON O.OnderdeelId = T.OnderdeelId INNER JOIN Eenheid E ON E.EenheidId = O.EenheidId INNER JOIN Traject TRJ ON TRJ.TrajectId = E.TrajectId WHERE TRJ.TeamID = @id ORDER BY F.DocentID ASC", conn))
                    {
                        cmd.Parameters.Add(new SqlParameter("@id", teamId));

                        using (SqlDataReader reader = cmd.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                GefixeerdeTaak taak = new GefixeerdeTaak();
                                taak.Fix_id = (int)reader["Fix_id"];
                                taak.DocentID = (int)reader["DocentID"];
                                taak.DocentNaam = (string)reader["Naam"];
                                taak.Taak_id = (int)reader["Taak_id"];
                                taak.TaakNaam = (string)reader["TaakNaam"];
                                GefixeerdeTaken.Add(taak);
                            }
                            return GefixeerdeTaken;
                        }
                    }
                }
            }

            catch (SqlException fout)
            {
                Debug.WriteLine(fout.Message);
            }

            return null;
        }

        public GefixeerdeTaak HaalGefixeerdeTaakOpMetID(int teamid)
        {
            GefixeerdeTaak taak = new GefixeerdeTaak();
            try
            {
                _connectie = dbconn.ReturnConnectionString();
                using (SqlConnection conn = new SqlConnection(_connectie))
                {
                    conn.Open();

                    using (SqlCommand cmd = new SqlCommand(
                        "SELECT F.*, (ANU.Voornaam + ' ' + ANU.Achternaam) as Naam FROM [dbo].[GefixeerdeTaken] F INNER JOIN [dbo].[Docent] D ON F.DocentID = D.DocentID " +
                        "INNER JOIN [dbo].[AspNetUsers] ANU ON D.MedewerkerID = ANU.Id INNER JOIN [dbo].[TeamLeider] TL ON D.DocentID=TL.MedewerkerID " +
                        "INNER JOIN [dbo].[Team] T ON TL.TeamleiderID=T.TeamLeiderID WHERE T.TeamID = @team)", conn))
                    {
                        cmd.Parameters.Add(new SqlParameter("@team", teamid));

                        using (SqlDataReader reader = cmd.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                taak.Fix_id = (int)reader["Fix_id"];
                                taak.DocentID = (int)reader["DocentID"];
                                taak.DocentNaam = (string)reader["Naam"];
                                taak.Taak_id = (int)reader["Taak_id"];
                            }
                        }
                    }
                }
            }

            catch (SqlException fout)
            {
                Debug.WriteLine(fout.Message);
            }

            return taak;
        }
    }
}
