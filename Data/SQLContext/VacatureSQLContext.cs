using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Diagnostics;
using System.Text;
using Data.Interfaces;
using Model;

namespace Data.Context
{
    class VacatureSQLContext : IVacatureContext
    {
        private DBconn dbconn = new DBconn();
       
        public List<Vacature> VacaturesOphalen()
        {
            List<Vacature> vacatures = new List<Vacature>();
            try
            {
                using (SqlConnection con = dbconn.SqlConnectie)
                {
                    con.Open();
                    using (SqlCommand cmd =
                        new SqlCommand("SELECT * FROM Vacature", con)
                    )
                    {
                        using (SqlDataReader reader = cmd.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                Vacature vacature = new Vacature
                                {
                                    Omschrijving = (string)reader["Omschrijving"]?.ToString(),
                                    Naam = (string)reader["Vacature_Naam"]
                                };

                                if (reader["TaakID"] != DBNull.Value)
                                {
                                    vacature.TaakID = Convert.ToInt32(reader["TaakID"]);
                                }

                                if (reader["VacatureID"] != DBNull.Value)
                                {
                                    vacature.VacatureID = Convert.ToInt32(reader["VacatureID"]);
                                }

                                vacatures.Add(vacature);
                            }
                        }
                    }
                }
            }
            catch (SqlException fout)
            {
                Debug.WriteLine(fout.Message);
            }
            return vacatures;
        }

        public void VacatureOpslaan(Vacature vac)
        {
            try
            {
                using (SqlConnection con = dbconn.SqlConnectie)
                {
                    con.Open();
                    using (SqlCommand cmd =
                        new SqlCommand("INSERT INTO [dbo].[Vacature]([Vacature_naam],[Omschrijving],[TaakID]) VALUES (@VacatureNaam,@Omschrijving,@TaakID)", con)
                    )
                    {
                        cmd.Parameters.AddWithValue("@VacatureNaam", vac.Naam);
                        if (vac.Omschrijving == null)
                        {
                            cmd.Parameters.AddWithValue("@Omschrijving", DBNull.Value);
                        }
                        else
                        {
                            cmd.Parameters.AddWithValue("@Omschrijving", vac.Omschrijving);
                            cmd.Parameters.AddWithValue("@TaakID", vac.TaakID);
                            cmd.ExecuteNonQuery();
                        }
                    }
                }
            }
            catch (SqlException fout)
            {
                Debug.WriteLine(fout.Message);
            }
        }

        public void DeleteVacature(int id)
        {
            try
            {
                using (SqlConnection con = dbconn.SqlConnectie)
                {
                    con.Open();
                    using (SqlCommand cmd =
                        new SqlCommand(
                            "Delete from Vacature where VacatureID=@VacatureID",
                            con)
                    )
                    {
                        cmd.Parameters.AddWithValue("@VacatureID", id);
                        cmd.ExecuteNonQuery();
                    }
                }
            }
            catch (SqlException fout)
            {
                Debug.WriteLine(fout.Message);
            }
        }

        public Vacature VacatureOphalen(int id)
        {
            try
            {
                using (SqlConnection con = dbconn.SqlConnectie)
                {
                    con.Open();
                    using (SqlCommand cmd = new SqlCommand("SELECT * FROM Vacature WHERE VacatureID = @VacatureID", con))
                    {
                        cmd.Parameters.AddWithValue("@VacatureID", id);
                        using (SqlDataReader reader = cmd.ExecuteReader())
                        {
                            Vacature vacature = new Vacature();
                            if (reader.Read())
                            {
                                vacature.Omschrijving = (string)reader["Omschrijving"]?.ToString();
                                vacature.Naam = (string)reader["Vacature_Naam"];
                                vacature.TaakID = Convert.ToInt32(reader["TaakID"]);
                                vacature.VacatureID = Convert.ToInt32(reader["VacatureID"]);
                            }
                            return vacature;
                        }
                    }
                }
            }
            catch
            {
                return new Vacature();
            }
        }

        public void UpdateVacature(Vacature vac)
        {
            try
            {
                using (SqlConnection con = dbconn.SqlConnectie)
                {
                    con.Open();
                    using (SqlCommand cmd =
                        new SqlCommand("UPDATE [dbo].[Vacature] SET [Vacature_naam] = @Naam,[Omschrijving] = @Omschrijving,[TaakID] = @TaakID WHERE VacatureID = @ID", con)
                    )
                    {
                        cmd.Parameters.AddWithValue("@ID", vac.VacatureID);
                        cmd.Parameters.AddWithValue("@Naam", vac.Naam);
                        // TODO Testen DBNUll Value + SQL Aanpassen.
                        if (string.IsNullOrEmpty(vac.Omschrijving))
                        {
                            cmd.Parameters.AddWithValue("@Omschrijving", DBNull.Value);
                        }
                        else
                        {
                            cmd.Parameters.AddWithValue("@Omschrijving", vac.Omschrijving);
                        }
                        cmd.Parameters.AddWithValue("@TaakID", vac.TaakID);
                        cmd.ExecuteNonQuery();
                    }
                }
            }
            catch (SqlException fout)
            {
                Debug.WriteLine(fout.Message);
            }
        }
    }
}