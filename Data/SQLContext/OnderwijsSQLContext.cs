using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Text;
using Data.Interfaces;
using Model;
using Model.Onderwijsdelen;

namespace Data.Context
{
    class OnderwijsSQLContext : IOnderwijsContext
    {
        //private SqlConnection connectie;
        private DBconn dbconn = new DBconn();

        public string OnderwijstaakNaam(int id)
        {
            try
            {
                using (SqlConnection con = dbconn.GetConnString())
                {
                    con.Open();
                    using (SqlCommand cmd = new SqlCommand("SELECT TaakNaam FROM Taak WHERE TaakID = @TaakID", con))
                    {
                        cmd.Parameters.AddWithValue("@TaakID", id);
                        using (SqlDataReader reader = cmd.ExecuteReader())
                        {
                            
                            string result = "";
                            if (reader.Read())
                            {
                                result = reader["TaakNaam"].ToString();
                            }
                            return result;
                        }
                    }
                }
            }
            catch (SqlException Fout)
            {
                Console.WriteLine(Fout.Message);
                return null;
            }
        }

        public List<Taak> TakenOphalen()
        {
            List<Taak> taken = new List<Taak>();
            try
            {
                using (SqlConnection con = dbconn.GetConnString())
                {
                    con.Open();
                    using (SqlCommand cmd = new SqlCommand("SELECT T.*, E.EenheidNaam AS EenheidNaam, O.OnderdeelNaam AS OnderdeelNaam " +
                                       "FROM Taak T " +
                                       "INNER JOIN Onderdeel O ON O.OnderdeelId = T.OnderdeelId " +
                                       "INNER JOIN Eenheid E ON E.EenheidId = O.EenheidId ", con))
                    {
                        using (SqlDataReader reader = cmd.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                Taak taak = new Taak();
                                taak.TaakNaam = reader["TaakNaam"].ToString();
                                taak.TaakId = Convert.ToInt32(reader["TaakId"]);
                                taak.OnderdeelId = Convert.ToInt32(reader["OnderdeelId"]);
                                taak.Omschrijving = reader["Omschrijving"].ToString();
                                taak.BenodigdeUren = (int)reader["BenodigdeUren"];
                                taak.AantalKlassen = (int)reader["Aantal_Klassen"];
                                taak.OnderdeelNaam = (string)reader["OnderdeelNaam"];
                                taak.EenheidNaam = (string)reader["EenheidNaam"];
                                taak.AantalBekwaam = AantalBekwaamOphalen(taak.TaakId);
                                taken.Add(taak);
                            }
                        }
                    }

                    
                }
            }
            catch (SqlException Fout)
            {
                Console.WriteLine(Fout.Message);
                return null;
            }
            return taken;
        }

        public int AantalBekwaamOphalen(int taakId)
        {
            int aantalTaken = 0;
            try
            {
                using (SqlConnection con = dbconn.GetConnString())
                {
                    con.Open();
                    using (SqlCommand cmd = new SqlCommand("SELECT COUNT(B.Docent_id) AS AantalBekwaam " +
                                                           "FROM Bekwaamheid B " +
                                                           "INNER JOIN Taak T ON T.TaakId = B.TaakID " +
                                                           "WHERE B.TaakID = @TaakID " +
                                                           "GROUP BY B.TaakID", con))
                    {
                        cmd.Parameters.AddWithValue("@TaakID", taakId);
                        
                        using (SqlDataReader reader = cmd.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                aantalTaken = (int) reader["AantalBekwaam"];
                            }
                        }
                    }
                }
            }
            catch (SqlException Fout)
            {
                Console.WriteLine(Fout.Message);
                return 0;
            }
            return aantalTaken;
        }

        public void TaakToevoegen(Taak taak)
        {
            try
            {
                using (SqlConnection con = dbconn.GetConnString())
                {
                    con.Open();
                    var command = con.CreateCommand();
                    command.Parameters.AddWithValue("@TaakNaam", taak.TaakNaam);
                    command.Parameters.AddWithValue("@OnderdeelId", taak.OnderdeelId);
                    command.Parameters.AddWithValue("@Omschrijving", taak.Omschrijving);
                    command.Parameters.AddWithValue("@BenodigdeUren", taak.BenodigdeUren);
                    command.Parameters.AddWithValue("@Aantal_Klassen", taak.AantalKlassen);

                    command.CommandText =
                        "INSERT INTO dbo.Taak (TaakNaam, OnderdeelId, Omschrijving, BenodigdeUren, Aantal_Klassen, TaakID)  " +
                        "VALUES (@TaakNaam, @OnderdeelId, @Omschrijving, @BenodigdeUren, @Aantal_Klassen, (SELECT Coalesce(MAX(TaakId), 0) + 1 FROM TAAK))";
                    command.ExecuteNonQuery();
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }
        }

        public void TaakVerwijderen(int taakId)
        {
            try
            {
                using (SqlConnection con = dbconn.GetConnString())
                {
                    con.Open();
                    using (SqlCommand cmd =
                        new SqlCommand("Delete from dbo.Taak where TaakId=@taak_id", con))
                    {
                        cmd.Parameters.AddWithValue("@taak_id", taakId);
                        cmd.ExecuteNonQuery();
                    }
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }
        }

        public Taak TaakOphalen(int id)
        {
            var taak = new Taak();
            try
            {
                using (SqlConnection con = dbconn.GetConnString())
                {
                    con.Open();
                    using (SqlCommand cmd =
                        new SqlCommand("SELECT T.*, E.EenheidNaam AS EenheidNaam, O.OnderdeelNaam AS OnderdeelNaam " +
                                       "FROM Taak T " +
                                       "INNER JOIN Onderdeel O ON O.OnderdeelId = T.OnderdeelId " +
                                       "INNER JOIN Eenheid E ON E.EenheidId = O.EenheidId " +
                                       "WHERE T.TaakId = @taakId", con))
                    {
                        cmd.Parameters.AddWithValue("@taakId", id);
                        using (SqlDataReader reader = cmd.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                taak.TaakId = id;
                                taak.TaakNaam = (string) reader["TaakNaam"];
                                taak.OnderdeelId = (int) reader["OnderdeelId"];
                                taak.Omschrijving = (string) reader["Omschrijving"];
                                taak.BenodigdeUren = (int) reader["BenodigdeUren"];
                                taak.AantalKlassen = (int) reader["Aantal_Klassen"];
                                taak.OnderdeelNaam = (string)reader["OnderdeelNaam"];
                                taak.EenheidNaam = (string)reader["EenheidNaam"];
                            }
                        }
                    }
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }
            return taak;
        }

        public void UpdateTaak(Taak taak)
        {
            try
            {
                using (SqlConnection con = dbconn.GetConnString())
                {
                    con.Open();
                    using (SqlCommand cmd =
                        new SqlCommand("UPDATE dbo.Taak SET [TaakNaam] = @taakNaam, [Omschrijving] = @omschrijving, [BenodigdeUren] = @BenodigdeUren, [Aantal_Klassen] = @Aantal_Klassen WHERE TaakId = @id", con))
                    {
                        cmd.Parameters.AddWithValue("@id", taak.TaakId);
                        cmd.Parameters.AddWithValue("@taakNaam", taak.TaakNaam);
                        //cmd.Parameters.AddWithValue("@OnderdeelId", taak.OnderdeelId);
                        cmd.Parameters.AddWithValue("@BenodigdeUren", taak.BenodigdeUren);
                        cmd.Parameters.AddWithValue("@Aantal_Klassen", taak.AantalKlassen);
                        if (string.IsNullOrEmpty(taak.Omschrijving))
                        {
                            cmd.Parameters.AddWithValue("@omschrijving", DBNull.Value);
                        }
                        else
                        {
                            cmd.Parameters.AddWithValue("@omschrijving", taak.Omschrijving);
                        }
                        cmd.ExecuteNonQuery();
                    }
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
    
            }
        }

        public List<Traject> GetTrajecten()
        {
            List<Traject> trajecten = new List<Traject>();
            try
            {
                using (SqlConnection con = dbconn.GetConnString())
                {
                    con.Open();
                    using (SqlCommand cmd = new SqlCommand("SELECT * FROM dbo.Traject", con))
                    {
                        using (SqlDataReader reader = cmd.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                Traject traject = new Traject();
                                traject.TrajectId = (int) reader["TrajectId"];
                                traject.TrajectNaam = (string) reader["TrajectNaam"];
                                trajecten.Add(traject);
                            }
                        }
                    }
                }
                return trajecten;
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                return trajecten;
            }
        }

        public List<Eenheid> GetEenhedenByTrajectId(int trajectId)
        {
            List<Eenheid> eenheden = new List<Eenheid>();
            try
            {
                using (SqlConnection con = dbconn.GetConnString())
                {
                    con.Open();
                    using (SqlCommand cmd =
                        new SqlCommand("SELECT * FROM dbo.Eenheid WHERE TrajectId = @TrajectId", con))
                    {
                        cmd.Parameters.AddWithValue("@TrajectId", trajectId);
                        using (SqlDataReader reader = cmd.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                Eenheid eenheid = new Eenheid();
                                eenheid.EenheidId = (int) reader["EenheidId"];
                                eenheid.EenheidNaam = (string) reader["EenheidNaam"];
                                eenheid.TrajectId = trajectId;
                                eenheden.Add(eenheid);
                            }
                        }
                    }
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }
            return eenheden;
        }

        public List<Onderdeel> GetOnderdelenByEenheidId(int eenheidId)
        {
            List<Onderdeel> onderdelen = new List<Onderdeel>();
            try
            {
                using (SqlConnection con = dbconn.GetConnString())
                {
                    con.Open();
                    using (SqlCommand cmd =
                        new SqlCommand("SELECT * FROM dbo.Onderdeel WHERE EenheidId = @EenheidId", con))
                    {
                        cmd.Parameters.AddWithValue("@EenheidId", eenheidId);
                        using (SqlDataReader reader = cmd.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                Onderdeel onderdeel = new Onderdeel();
                                onderdeel.OnderdeelId = (int) reader["OnderdeelId"];
                                onderdeel.OnderdeelNaam = (string) reader["OnderdeelNaam"];
                                onderdeel.EenheidId = eenheidId;
                                onderdelen.Add(onderdeel);
                            }
                        }
                    }
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }
            return onderdelen;
        }

        public List<Taak> GetTakenByOnderdeelId(int onderdeelId)
        {
            List<Taak> taken = new List<Taak>();
            try
            {
                using (SqlConnection con = dbconn.GetConnString())
                {
                    con.Open();
                    using (SqlCommand cmd =
                        new SqlCommand("SELECT * FROM dbo.Taak WHERE OnderdeelId = @OnderdeelId", con))
                    {
                        cmd.Parameters.AddWithValue("@OnderdeelId", onderdeelId);
                        using (SqlDataReader reader = cmd.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                Taak taak = new Taak();
                                taak.TaakId = (int) reader["TaakId"];
                                taak.TaakNaam = (string) reader["TaakNaam"];
                                taak.OnderdeelId = onderdeelId;
                                taak.Omschrijving = (string) reader["Omschrijving"];
                                taken.Add(taak);
                            }
                        }
                    }
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }
            return taken;
        }

        public List<Eenheid> OphalenEenhedenBlokeigenaar(string blokeigenaarId)
        {
            List<Eenheid> eenhedenBlokeigenaar = new List<Eenheid>();
            try
            {
                using (SqlConnection con = dbconn.GetConnString())
                {
                    con.Open();
                    /* SELECT E.*
                        FROM Eenheid E
                        INNER JOIN Blokeigenaar B ON B.BlokeigenaarId = E.BlokeigenaarId
                        WHERE B.MedwerkerID = '1f5a94b6-67a4-49ca-912e-6356a7842b86'
                        */
                    using (SqlCommand cmd =
                        new SqlCommand("SELECT E.* " +
                                       "FROM Eenheid E " +
                                       "INNER JOIN Blokeigenaar B ON B.BlokeigenaarId = E.BlokeigenaarId " +
                                       "WHERE B.MedwerkerID = @BlokeigenaarId", con))
                    {
                        cmd.Parameters.AddWithValue("@BlokeigenaarId", blokeigenaarId);
                        using (SqlDataReader reader = cmd.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                Eenheid eenheid = new Eenheid();
                                eenheid.EenheidId = (int)reader["EenheidId"];
                                eenheid.EenheidNaam = (string)reader["EenheidNaam"];
                                eenheid.TrajectId = (int) reader["TrajectId"];
                                eenhedenBlokeigenaar.Add(eenheid);
                            }
                        }
                    }
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }
            return eenhedenBlokeigenaar;
        }

    }
}