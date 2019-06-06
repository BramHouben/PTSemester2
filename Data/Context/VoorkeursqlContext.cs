using Model;
using Model.Onderwijsdelen;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;

namespace Data.Context
{
    public class VoorkeurSQLContext : IVoorkeurContext
    {
        private SqlConnection connectie { get; }
        private DBconn dbconn = new DBconn();

        public VoorkeurSQLContext()
        {
            connectie = dbconn.GetConnString();
        }

        public List<Voorkeur> VoorkeurenOphalen(string id)
        {
            int ResultId;
            List<Voorkeur> vklistmodel = new List<Voorkeur>();
            try
            {
                
                using (SqlConnection con = dbconn.GetConnString())
                {
                    con.Open();
                    using (SqlCommand cmd = new SqlCommand("SELECT DocentID FROM Docent WHERE MedewerkerID = @MedewerkerID", con))
                    {
                        cmd.Parameters.AddWithValue("@MedewerkerID", id);
                        ResultId = (int)cmd.ExecuteScalar();
                    }
                    using (SqlCommand command = new SqlCommand("SELECT * FROM Bekwaamheid where Docent_id = @UserId ORDER BY Traject", con))
                    {
                        command.Parameters.AddWithValue("@UserID", ResultId);
                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                var voorkeur = new Voorkeur
                                {
                                    Id = (int)reader["Bekwaam_Id"],
                                    TrajectNaam = reader["Traject"]?.ToString(),
                                    EenheidNaam = reader["Eenheid"]?.ToString(),
                                    OnderdeelNaam = reader["Onderdeel"]?.ToString(),
                                    TaakNaam = reader["Taak"]?.ToString()
                                };
                                //voorkeur.Prioriteit = (int)reader["Prioriteit"];
                                vklistmodel.Add(voorkeur);
                            }
                        }
                    }
                }
            }
            catch (SqlException error)
            {
                Console.WriteLine(error.Message);
            }

            return vklistmodel;
        }

        public void VoorkeurToevoegen(Voorkeur voorkeur, string id)
        {
            object resultTraject;
            object resultEenheid;
            object resultOnderdeel;
            object resultTaak;
            object result;

            try
            {
                using (SqlConnection conn = dbconn.SqlConnectie)
                {
                    conn.Open();

                    using (SqlCommand cmd = new SqlCommand("SELECT TrajectNaam FROM Traject WHERE TrajectId = @traject", conn))
                    {
                        cmd.Parameters.Add(new SqlParameter("@traject", voorkeur.TrajectNaam));
                        resultTraject = cmd.ExecuteScalar();
                    }

                    if (voorkeur.EenheidNaam == "0")
                    {
                        resultEenheid = DBNull.Value.ToString();
                        resultOnderdeel = DBNull.Value.ToString();
                        resultTaak = DBNull.Value.ToString();
                        voorkeur.TaakNaam = DBNull.Value.ToString();
                    }
                    else
                    {
                        using (SqlCommand cmd = new SqlCommand("SELECT EenheidNaam FROM Eenheid WHERE EenheidId = @eenheid", conn))
                        {
                            cmd.Parameters.Add(new SqlParameter("@eenheid", voorkeur.EenheidNaam));
                            resultEenheid = cmd.ExecuteScalar();
                        }

                        if (voorkeur.OnderdeelNaam == "0")
                        {
                            resultOnderdeel = DBNull.Value.ToString();
                            resultTaak = DBNull.Value.ToString();
                            voorkeur.TaakNaam = DBNull.Value.ToString();
                        }
                        else
                        {
                            using (SqlCommand cmd = new SqlCommand("SELECT OnderdeelNaam FROM Onderdeel WHERE OnderdeelId = @onderdeel", conn))
                            {
                                cmd.Parameters.Add(new SqlParameter("@onderdeel", voorkeur.OnderdeelNaam));
                                resultOnderdeel = cmd.ExecuteScalar();
                            }

                            if (voorkeur.TaakNaam == "0")
                            {
                                resultTaak = DBNull.Value.ToString();
                                voorkeur.TaakNaam = DBNull.Value.ToString();
                            }
                            else
                            {
                                using (SqlCommand cmd = new SqlCommand("SELECT TaakNaam FROM Taak WHERE TaakId = @taak", conn))
                                {
                                    cmd.Parameters.Add(new SqlParameter("@taak", voorkeur.TaakNaam));
                                    resultTaak = cmd.ExecuteScalar();
                                }
                            }
                        }
                    }

                    using (SqlCommand cmd = new SqlCommand("SELECT DocentID FROM Docent WHERE MedewerkerID = @docent", conn))
                    {
                        cmd.Parameters.Add(new SqlParameter("@docent", id));
                        result = (int)cmd.ExecuteScalar();

                        if (voorkeur.TaakNaam == "")
                        {
                            List<Taak> taken = new List<Taak>();
                            using (SqlCommand cmdTraject = new SqlCommand("SELECT T.TaakId, T.TaakNaam, O.OnderdeelNaam, E.EenheidNaam, TR.TrajectNaam FROM Taak T INNER JOIN Onderdeel O " +
                                "ON O.OnderdeelId = T.OnderdeelId  INNER JOIN Eenheid E ON O.EenheidId = E.EenheidId INNER JOIN Traject TR ON E.TrajectId = TR.TrajectId WHERE TR.TrajectNaam = @traject", conn))
                            {
                                cmdTraject.Parameters.AddWithValue("@traject", resultTraject);

                                using (SqlDataReader reader = cmdTraject.ExecuteReader())
                                {
                                    while (reader.Read())
                                    {
                                        Taak taak = new Taak();
                                        taak.TaakId = (int)reader["TaakId"];
                                        taak.TaakNaam = (string)reader["TaakNaam"];
                                        taak.EenheidNaam = (string)reader["EenheidNaam"];
                                        taak.OnderdeelNaam = (string)reader["OnderdeelNaam"];
                                        taak.TrajectNaam = (string)reader["TrajectNaam"];
                                        taken.Add(taak);
                                    }
                                }
                            }

                            foreach (Taak taak in taken)
                            {
                                using (SqlCommand command = new SqlCommand("INSERT INTO Bekwaamheid(Traject, Eenheid, Onderdeel, Taak, Docent_id, TaakID) VALUES(@TrajectNaam, @EenheidNaam, @OnderdeelNaam, @TaakNaam, @UserId, @taakId)", conn))
                                {
                                    command.Parameters.AddWithValue("@TrajectNaam", taak.TrajectNaam);
                                    command.Parameters.AddWithValue("@EenheidNaam", taak.EenheidNaam);
                                    command.Parameters.AddWithValue("@OnderdeelNaam", taak.OnderdeelNaam);
                                    command.Parameters.AddWithValue("@TaakNaam", taak.TaakNaam);
                                    command.Parameters.AddWithValue("@UserId", result);
                                    command.Parameters.AddWithValue("@taakId", taak.TaakId);
                                    command.ExecuteNonQuery();
                                }
                            }
                        }

                        else
                        {

                            using (SqlCommand command = new SqlCommand("INSERT INTO Bekwaamheid (Traject, Eenheid, Onderdeel, Taak, Docent_id, TaakID) VALUES ( @TrajectNaam, @EenheidNaam, @OnderdeelNaam, @TaakNaam, @UserId, @taakId)", conn))
                            {
                                command.Parameters.AddWithValue("@TrajectNaam", resultTraject);
                                command.Parameters.AddWithValue("@EenheidNaam", resultEenheid);
                                command.Parameters.AddWithValue("@OnderdeelNaam", resultOnderdeel);
                                command.Parameters.AddWithValue("@TaakNaam", resultTaak);
                                //command.Parameters.AddWithValue("@Prioriteit", voorkeur.Prioriteit);
                                command.Parameters.AddWithValue("@UserId", result);
                                command.Parameters.AddWithValue("@taakId", voorkeur.TaakNaam);
                                command.ExecuteNonQuery();
                            }
                        }
                    }
                }
            }
            catch (SqlException fout)
            {
                Console.WriteLine(fout.Message);
            }
        }

        public void DeleteVoorkeur(int id)
        {
            try
            {
                using (SqlConnection con = dbconn.GetConnString())
                {
                    con.Open();
                    using (SqlCommand cmd =
                        new SqlCommand("Delete from Bekwaamheid where Bekwaam_Id=@voorkeur_id", con)
                    )
                    {
                        cmd.Parameters.AddWithValue("@voorkeur_id", id);
                        cmd.ExecuteNonQuery();
                    }
                }
            }
            catch (SqlException)
            {
            }
        }

        public List<Traject> GetTrajecten()
        {
            var trajecten = new List<Traject>();
            try
            {
                using (SqlConnection con = dbconn.SqlConnectie)
                {
                    con.Open();
                    using (SqlCommand cmd =
                        new SqlCommand("Select * FROM dbo.Traject", con)
                    )
                    {
                        using (SqlDataReader reader = cmd.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                var traject = new Traject
                                {
                                    TrajectId = (int)reader["TrajectId"],
                                    TrajectNaam = reader["TrajectNaam"]?.ToString(),
                                };

                                trajecten.Add(traject);
                            }
                        }
                    }
                }
            }
            catch (SqlException Fout)
            {
                Console.WriteLine(Fout.Message);
            }

            return trajecten;
        }

        public List<Eenheid> GetEenhedenByTrajectId(int trajectId)
        {
            var eenheden = new List<Eenheid>();
            try
            {
                using (SqlConnection con = dbconn.GetConnString())
                {
                    con.Open();
                    using (SqlCommand cmd =
                        new SqlCommand("SELECT * FROM dbo.Eenheid WHERE Eenheid.trajectId = @trajectid",
                            con)
                    )
                    {
                        cmd.Parameters.AddWithValue("@trajectid", trajectId);
                        using (SqlDataReader reader = cmd.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                var eenheid = new Eenheid
                                {
                                    EenheidId = (int)reader["EenheidId"],
                                    EenheidNaam = reader["EenheidNaam"]?.ToString(),
                                    TrajectId = (int)reader["TrajectId"],
                                };

                                eenheden.Add(eenheid);
                            }
                        }
                    }
                }
            }
            catch (SqlException)
            {
                return new List<Eenheid>();
            }

            return eenheden;
        }

        public List<Onderdeel> GetOnderdelenByEenheidId(int eenheidId)
        {
            var onderdelen = new List<Onderdeel>();
            try
            {
                using (SqlConnection con = dbconn.GetConnString())
                {
                    con.Open();
                    using (SqlCommand cmd =
                        new SqlCommand(
                            "SELECT * FROM dbo.Onderdeel WHERE Onderdeel.EenheidId = @eenheidId", con)
                    )
                    {
                        cmd.Parameters.AddWithValue("@eenheidId", eenheidId);
                        using (SqlDataReader reader = cmd.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                var onderdeel = new Onderdeel
                                {
                                    OnderdeelId = (int)reader["OnderdeelId"],
                                    OnderdeelNaam = reader["OnderdeelNaam"]?.ToString(),
                                    EenheidId = (int)reader["EenheidId"],
                                };

                                onderdelen.Add(onderdeel);
                            }
                        }
                    }
                }
            }
            catch (SqlException)
            {
                return new List<Onderdeel>();
            }

            return onderdelen;
        }

        public List<Taak> GetTakenByOnderdeelId(int onderdeelId)
        {
            var taken = new List<Taak>();
            try
            {
        
                using (SqlConnection con = dbconn.GetConnString())
                {
                    con.Open();
                    using (SqlCommand cmd =
                        new SqlCommand("SELECT * FROM dbo.Taak WHERE Taak.OnderdeelId = @onderdeelId",
                            con)
                    )
                    {
                        cmd.Parameters.AddWithValue("@onderdeelId", onderdeelId);
                        using (SqlDataReader reader = cmd.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                var taak = new Taak
                                {
                                    TaakId = (int)reader["TaakId"],
                                    TaakNaam = reader["TaakNaam"]?.ToString(),
                                    OnderdeelId = (int)reader["OnderdeelId"],
                                    Omschrijving = reader["Omschrijving"]?.ToString(),
                                };

                                taken.Add(taak);
                            }
                        }
                    }
                }
            }
            catch (SqlException)
            {
                return new List<Taak>();
            }

            return taken;
        }

        public string GetTaakInfo(int taakId)
        {
            string info = "";
            try
            {
                using (SqlConnection con = dbconn.GetConnString())
                {
                    con.Open();
                    using (SqlCommand cmd =
                        new SqlCommand("SELECT Omschrijving FROM dbo.Taak WHERE Taak.TaakId = @taakId",
                            con)
                    )
                    {
                        cmd.Parameters.AddWithValue("@taakId", taakId);
                        using (SqlDataReader reader = cmd.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                info = reader["Omschrijving"]?.ToString();
                            }
                        }
                    }
                }

                return info;
            }
            catch (SqlException fout)
            {
                Console.WriteLine(fout.Message);
                return "";
            }
        }

        public List<Medewerker> GetDocentenList(string user_id)
        {
            var List = new List<Medewerker>();
            try
            {
      
                using (SqlConnection connectie = dbconn.GetConnString())
                {
                    connectie.Open();
                    using (SqlCommand getteam = new SqlCommand("select TeamID from Docent where MedewerkerID = @user_id", connectie))
                    {
                        getteam.Parameters.AddWithValue("@user_id", user_id);
                        int team_id = (int)getteam.ExecuteScalar();
                        using (SqlCommand cmd = new SqlCommand("SELECT D.DocentID, D.TeamID, D.RuimteVoorInzet, D.MedewerkerID , (Asp.Voornaam + ' ' + Asp.Achternaam) AS Naam  FROM Docent D inner join AspNetUsers Asp ON D.MedewerkerID = Asp.Id where TeamID = @TeamID", connectie))
                        {
                            cmd.Parameters.AddWithValue("@TeamID", team_id);
                            using (SqlDataReader reader = cmd.ExecuteReader())
                            {
                                while (reader.Read())
                                {
                                    var Info = new Medewerker
                                    {
                                        MedewerkerId = (string)reader["MedewerkerID"],
                                        Naam = (string)reader["Naam"],
                                    };

                                    List.Add(Info);
                                }
                            }
                        }
                    }
                }
                return List;
            }
            catch (SqlException fout)
            {
                Console.WriteLine(fout.Message);
                return List;
            }
        }

        public bool KijkVoorDubbel(Voorkeur voorkeur, string id)
        {
            try
            {
                var resultTraject = "";
                var resultEenheid = "";
                var resultOnderdeel = "";
                var resultTaak = "";
                var resultId = "";
                using (SqlConnection con = dbconn.SqlConnectie)
                {
                    con.Open();
                    using (SqlCommand cmdTraject =
                        new SqlCommand("SELECT TrajectNaam FROM Traject WHERE TrajectId = @voorkeurTrajectNaam", con))
                    {
                        cmdTraject.Parameters.AddWithValue("@voorkeurTrajectNaam", voorkeur.TrajectNaam);
                        using (SqlDataReader reader = cmdTraject.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                resultTraject = reader["TrajectNaam"]?.ToString();
                            }
                        }
                    }

                    using (SqlCommand cmdEenheid =
                        new SqlCommand("SELECT EenheidNaam FROM Eenheid WHERE EenheidId = @voorkeurEenheidNaam", con))
                    {
                        cmdEenheid.Parameters.AddWithValue("@voorkeurEenheidNaam", voorkeur.EenheidNaam);
                        using (SqlDataReader reader = cmdEenheid.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                resultEenheid = reader["EenheidNaam"]?.ToString();
                            }
                        }
                    }

                    using (SqlCommand cmdOnderdeel =
                        new SqlCommand("SELECT OnderdeelNaam FROM Onderdeel WHERE OnderdeelId = @voorkeurOnderdeelNaam", con))
                    {
                        cmdOnderdeel.Parameters.AddWithValue("@voorkeurOnderdeelNaam", voorkeur.OnderdeelNaam);
                        using (SqlDataReader reader = cmdOnderdeel.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                resultOnderdeel = reader["OnderdeelNaam"]?.ToString();
                            }
                        }
                    }

                    using (SqlCommand cmdTaak =
                        new SqlCommand("SELECT TaakNaam FROM Taak WHERE TaakId = @voorkeurTaakNaam", con))
                    {
                        cmdTaak.Parameters.AddWithValue("@voorkeurTaakNaam", voorkeur.TaakNaam);
                        using (SqlDataReader reader = cmdTaak.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                resultTaak = reader["TaakNaam"]?.ToString();
                            }
                        }
                    }

                    using (SqlCommand cmdid =
                        new SqlCommand("SELECT DocentID FROM Docent WHERE MedewerkerID = @medewerkerId", con))
                    {
                        cmdid.Parameters.AddWithValue("@medewerkerId", id);
                        using (SqlDataReader reader = cmdid.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                resultId = reader["DocentID"]?.ToString();
                            }
                        }
                    }

                    if (voorkeur.EenheidNaam == "0")
                    {
                        resultEenheid = DBNull.Value.ToString();
                        resultOnderdeel = DBNull.Value.ToString();
                        resultTaak = DBNull.Value.ToString();
                        voorkeur.TaakNaam = DBNull.Value.ToString();
                    }
                    else if (voorkeur.OnderdeelNaam == "0")
                    {
                        resultOnderdeel = DBNull.Value.ToString();
                        resultTaak = DBNull.Value.ToString();
                        voorkeur.TaakNaam = DBNull.Value.ToString();
                    }
                    else if (voorkeur.TaakNaam == "0")
                    {
                        resultTaak = DBNull.Value.ToString();
                        voorkeur.TaakNaam = DBNull.Value.ToString();
                    }
                    
                    using (SqlCommand cmd =
                        new SqlCommand("SELECT Count(*) FROM Bekwaamheid where Docent_id = @User_id and Traject = @traject and Eenheid= @eenheid and Onderdeel = @onderdeel and Taak=@taak and TaakID= @taakId", con))
                    {
                        cmd.Parameters.AddWithValue("@User_id", resultId);
                        cmd.Parameters.AddWithValue("@traject", resultTraject);
                        cmd.Parameters.AddWithValue("@eenheid", resultEenheid);
                        cmd.Parameters.AddWithValue("@onderdeel", resultOnderdeel);
                        cmd.Parameters.AddWithValue("@taak", resultTaak);
                        cmd.Parameters.AddWithValue("@taakId", voorkeur.TaakNaam);
                        
                        int uitslag = (int)cmd.ExecuteScalar();
                        if (uitslag == 1)
                        {
                            return false;
                        }
                        else
                        {
                            return true;
                        }
                    }
                }
            }
            catch (SqlException fout)
            {
                Console.WriteLine(fout.Message);
                return false;
            }
        }

        public List<Traject> GetTrajectenInzetbaar(string user_id)
        {
            var trajecten = new List<Traject>();
            try
            {
                using (SqlConnection con = dbconn.GetConnString())
                {
                    con.Open();
                    using (SqlCommand cmd =
                        new SqlCommand("Select * FROM dbo.Traject", con)
                    )
                    {
                        using (SqlDataReader reader = cmd.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                var traject = new Traject
                                {
                                    TrajectId = (int)reader["TrajectId"],
                                    TrajectNaam = reader["TrajectNaam"]?.ToString(),
                                };

                                trajecten.Add(traject);
                            }
                        }
                    }
                }
            }
            catch (SqlException)
            {
                return new List<Traject>();
            }

            return trajecten;
        }

        public void InvoegenTaakVoorkeur(int id, int prioriteit, string User_id)
        {
            int count;

            try
            {
                using (SqlConnection connectie = dbconn.GetConnString())
                {
                    connectie.Open();
                    var cmdid = connectie.CreateCommand();
                    cmdid.CommandText = "SELECT DocentID FROM Docent WHERE MedewerkerID = '" + User_id + "'";
                    var ResultId = cmdid.ExecuteScalar();

                    using (SqlCommand command = new SqlCommand("select count(DocentID) from DocentVoorkeur where DocentID=@docent_id and Bekwaamheid_id=@Bekwaamheid_id ", connectie))
                    {
                        command.Parameters.Add(new SqlParameter("Bekwaamheid_id", id));
                        command.Parameters.Add(new SqlParameter("docent_id", ResultId));
                        count = (int)command.ExecuteScalar();
                    }

                    if (count==0)
                    {
                        using (SqlCommand command = new SqlCommand("insert into DocentVoorkeur values(@user_id, @Prioriteit, @Bekwaamheid_id)", connectie))
                        {
                            command.Connection = connectie;
                            command.Parameters.Add(new SqlParameter("user_id", ResultId));
                            command.Parameters.Add(new SqlParameter("Prioriteit", prioriteit));
                            command.Parameters.Add(new SqlParameter("Bekwaamheid_id", id));

                            command.ExecuteNonQuery();
                        }
                    }
                    else
                    {
                        using (SqlCommand command = new SqlCommand("  UPDATE DocentVoorkeur SET Prioriteit = @Prioriteit WHERE Bekwaamheid_id = @Bekwaamheid_id", connectie))
                        {
                            command.Connection = connectie;
                            command.Parameters.Add(new SqlParameter("Prioriteit", prioriteit));
                            command.Parameters.Add(new SqlParameter("Bekwaamheid_id", id));

                            command.ExecuteNonQuery();
                        }
                    }
                  

              
                }
            }
            catch (SqlException error)
            {
                Console.WriteLine(error.Message);
            }
        }

        public Voorkeur GetVoorkeurInfo(int id)
        {
            Voorkeur voorkeur = new Voorkeur();
            string constring = connectie.ConnectionString;
            try
            {
                using (SqlConnection connectie =dbconn.GetConnString())
                {
                    connectie.Open();
                    var cmdid = connectie.CreateCommand();
                    cmdid.CommandText = "SELECT * FROM Bekwaamheid WHERE Bekwaam_Id = '" + id + "'";
                    var ResultId = cmdid.ExecuteScalar();

                    using (SqlCommand command = new SqlCommand("SELECT * FROM Bekwaamheid WHERE Bekwaam_Id = @id", connectie))
                    {
                        command.Parameters.Add(new SqlParameter("id", id));
                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                voorkeur.TrajectNaam = (string)reader["Traject"];
                                voorkeur.EenheidNaam = (string)reader["Eenheid"];
                                voorkeur.OnderdeelNaam = (string)reader["Onderdeel"];
                                voorkeur.TaakNaam = (string)reader["Taak"];
                            }
                        }
                    }
                }
            }
            catch (SqlException Ex)
            {
                Console.WriteLine(Ex.Message);
            }
            return voorkeur;
        }

        public int GetTaakTijd(int taakId)
        {
            int tijd = 0;
            try
            {
                using (SqlConnection con = dbconn.GetConnString())
                {
                    con.Open();
                    using (SqlCommand cmd =
                        new SqlCommand("SELECT BenodigdeUren FROM dbo.Taak WHERE Taak.TaakId = @taakId",
                            con)
                    )
                    {
                        cmd.Parameters.AddWithValue("@taakId", taakId);
                        using (SqlDataReader reader = cmd.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                tijd = (int)reader["BenodigdeUren"];
                            }
                        }
                    }
                }
            }
            catch (SqlException fout)
            {
                Console.WriteLine(fout.Message);
            }
            return tijd;
        }

        public Traject GetTrajectByID(int id)
        {
            Traject traject = new Traject();
            try
            {
                using (SqlConnection con = dbconn.SqlConnectie)
                {
                    con.Open();
                    using (SqlCommand cmd = new SqlCommand("Select * FROM dbo.Traject WHERE TeamID = @id", con))
                    {
                        cmd.Parameters.AddWithValue("@id", id);
                        using (SqlDataReader reader = cmd.ExecuteReader())
                        {
                            while (reader.Read())
                            {

                                traject.TrajectId = (int)reader["TrajectId"];
                                traject.TrajectNaam = reader["TrajectNaam"]?.ToString();
                                
                                
                            }
                        }
                    }
                }
            }
            catch (SqlException Fout)
            {
                Console.WriteLine(Fout.Message);
            }

            return traject;
        }
    }
}