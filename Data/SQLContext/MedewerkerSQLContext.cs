using System;
using Data.Interfaces;
using Model;
using Model.Onderwijsdelen;
using System.Data.SqlClient;
using System.Collections.Generic;
using System.Diagnostics;

namespace Data.Context
{
    public class MedewerkerSQLContext : IMedewerkerContext
    {
        private SqlConnection conn;
        private DBconn dbconn = new DBconn();
        private string _connectie;

        public Medewerker GetMedewerkerId(string id)
        {
            Medewerker medewerker_new = new Medewerker();
            try
            {
                using (SqlConnection conn = dbconn.GetConnString())
                {
                    conn.Open();
                    using (SqlCommand cmd = new SqlCommand("SELECT id, Email FROM AspNetUsers WHERE id = @id", conn),
                        cmd2 = new SqlCommand("SELECT roleId FROM AspNetUserRoles WHERE UserId = @id", conn))
                    {
                        cmd.Parameters.AddWithValue("@id", id);
                        cmd2.Parameters.AddWithValue("@id", id);
                        using (SqlDataReader reader = cmd.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                medewerker_new.MedewerkerId = (string)reader["id"];
                                medewerker_new.Email = (string)reader["Email"];
                            }
                        }
                        using (SqlDataReader reader2 = cmd2.ExecuteReader())
                        {
                            while (reader2.Read())
                            {
                                medewerker_new.Role_id = (string)reader2["roleId"];
                            }
                        }
                    }
                }
            }
            catch (Exception fout)
            {
                Debug.WriteLine(fout.Message);
                return medewerker_new;
            }
            return medewerker_new;
        }

        public List<Eenheid> KrijgAlleEenheden()
        {
            List<Eenheid> eenheden = new List<Eenheid>();

            try
            {
                _connectie = dbconn.ReturnConnectionString();
                using (SqlConnection conn = new SqlConnection(_connectie))
                {
                    conn.Open();

                    using (SqlCommand cmd = new SqlCommand(
                        "SELECT E.EenheidId, E.EenheidNaam, TR.TrajectNaam, E.ECTS, T.Aantal_Klassen FROM Eenheid E INNER JOIN Traject TR ON E.TrajectId=TR.TrajectId INNER JOIN Onderdeel O ON O.EenheidId=E.EenheidId INNER JOIN Taak T ON T.OnderdeelId=O.OnderdeelId GROUP BY E.EenheidId, E.EenheidNaam, TR.TrajectNaam, E.ECTS, T.Aantal_Klassen", conn))
                    //SELECT ... FROM Eenheid WHERE TrajectId/Naam = Wat er gekozen is in de combobox 
                    {
                        using (SqlDataReader reader = cmd.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                Eenheid eenheid = new Eenheid()
                                {
                                    EenheidId = (int)reader[0],
                                    EenheidNaam = reader[1]?.ToString(),
                                    TrajectNaam = reader[2]?.ToString(),
                                    ECTS = (int)reader[3],
                                    AantalKlassen = (int)reader[4]
                                };
                                eenheden.Add(eenheid);
                            }
                        }
                    }
                }
            }

            catch (SqlException fout)
            {
                Debug.WriteLine(fout.Message);
            }

            return eenheden;
        }



        public void WijzigEenheid(Eenheid eenheid)
        {
            try
            {
                _connectie = dbconn.ReturnConnectionString();
                using (SqlConnection conn = new SqlConnection(_connectie))
                {
                    conn.Open();

                    using (SqlCommand cmd = new SqlCommand(
                        "UPDATE Eenheid SET ECTS = @ects WHERE EenheidId = @id", conn))
                    {
                        cmd.Parameters.Add(new SqlParameter("@ects", eenheid.ECTS));
                        cmd.Parameters.Add(new SqlParameter("@id", eenheid.EenheidId));
                        cmd.ExecuteNonQuery();
                    }

                    using (SqlCommand cmd = new SqlCommand(
                        "UPDATE T SET T.Aantal_Klassen = @klassen FROM Taak T INNER JOIN Onderdeel O ON T.OnderdeelId=O.OnderdeelId INNER JOIN Eenheid E ON O.EenheidId=E.EenheidId WHERE E.EenheidId = @id", conn))
                    {
                        cmd.Parameters.Add(new SqlParameter("@id", eenheid.EenheidId));
                        cmd.Parameters.Add(new SqlParameter("@klassen", eenheid.AantalKlassen));
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