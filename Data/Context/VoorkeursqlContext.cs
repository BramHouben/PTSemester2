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
            try
            {
                List<Voorkeur> vklistmodel = new List<Voorkeur>();

                connectie.Open();

                var cmdid = connectie.CreateCommand();
                cmdid.CommandText = "SELECT DocentID FROM Docent WHERE MedewerkerID = '" + id + "'";
                var ResultId = cmdid.ExecuteScalar();




                var cmd = new SqlCommand("SELECT * FROM Bekwaamheid where Docent_id  = @UserId", connectie);
                /*var cmd = new SqlCommand("SELECT Traject.TrajectNaam, Onderdeel.OnderdeelNaam, Taak.TaakNaam, vk.Prioriteit, vk.UserID " +
                    "FROM Voorkeur AS vk INNER JOIN Traject ON vk.Traject=Traject.TrajectId INNER JOIN Onderdeel ON vk.Onderdeel=Onderdeel.OnderdeelId " +
                    "INNER JOIN Taak ON vk.Taak=Taak.TaakId WHERE vk.UserId = @UserId", connectie);*/
                cmd.Parameters.AddWithValue("@UserId", ResultId);
                var reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    var voorkeur = new Voorkeur();

                    voorkeur.Id = (int)reader["Bekwaam_Id"];
                    voorkeur.TrajectNaam = reader["Traject"]?.ToString();
                    voorkeur.EenheidNaam = reader["Eenheid"]?.ToString();
                    voorkeur.OnderdeelNaam = reader["Onderdeel"]?.ToString();
                    voorkeur.TaakNaam = reader["Taak"]?.ToString();
                    //voorkeur.Prioriteit = (int)reader["Prioriteit"];
                    vklistmodel.Add(voorkeur);
                }
                connectie.Close();

                return vklistmodel;
            }
            catch (SqlException fout)
            {
                Console.WriteLine(fout.Message);
                return null;
            }
        }

        public void VoorkeurToevoegen(Voorkeur voorkeur, string id)
        {
            try
            {

                connectie.Open();
                var cmdTraject = connectie.CreateCommand();
                cmdTraject.CommandText = "SELECT TrajectNaam FROM Traject WHERE TrajectId = '" + voorkeur.TrajectNaam + "'";
                var resultTraject = cmdTraject.ExecuteScalar();

                var cmdEenheid = connectie.CreateCommand();
                cmdEenheid.CommandText = "SELECT EenheidNaam FROM Eenheid WHERE EenheidId = '" + voorkeur.EenheidNaam + "'";
                var resultEenheid = cmdEenheid.ExecuteScalar();

                var cmdOnderdeel = connectie.CreateCommand();
                cmdOnderdeel.CommandText = "SELECT OnderdeelNaam FROM Onderdeel WHERE OnderdeelId = '" + voorkeur.OnderdeelNaam + "'";
                var resultOnderdeel = cmdOnderdeel.ExecuteScalar();

                var cmdTaak = connectie.CreateCommand();
                cmdTaak.CommandText = "SELECT TaakNaam FROM Taak WHERE TaakId = '" + voorkeur.TaakNaam + "'";
                var resultTaak = cmdTaak.ExecuteScalar();

                var cmdid = connectie.CreateCommand();
                cmdid.CommandText = "SELECT DocentID FROM Docent WHERE MedewerkerID = '" + id + "'";
                var ResultId = cmdid.ExecuteScalar();



                if (voorkeur.EenheidNaam == "0")
                {
                    resultEenheid = DBNull.Value.ToString();
                    resultOnderdeel = DBNull.Value.ToString();
                    resultTaak = DBNull.Value.ToString();
                }
                else if (voorkeur.OnderdeelNaam == "0")
                {
                    resultOnderdeel = DBNull.Value.ToString();
                    resultTaak = DBNull.Value.ToString();
                }
                else if (voorkeur.TaakNaam == "0")
                {
                    resultTaak = DBNull.Value.ToString();
                }

                var command = connectie.CreateCommand();
                command.Parameters.AddWithValue("@TrajectNaam", resultTraject);
                command.Parameters.AddWithValue("@EenheidNaam", resultEenheid);
                command.Parameters.AddWithValue("@OnderdeelNaam", resultOnderdeel);
                command.Parameters.AddWithValue("@TaakNaam", resultTaak);
                //command.Parameters.AddWithValue("@Prioriteit", voorkeur.Prioriteit);
                command.Parameters.AddWithValue("@UserId", ResultId);

                command.CommandText = "INSERT INTO Bekwaamheid (Traject, Eenheid, Onderdeel, Taak, Docent_id) VALUES ( @TrajectNaam, @EenheidNaam, @OnderdeelNaam, @TaakNaam, @UserId)";
                command.ExecuteNonQuery();

            }
            catch (SqlException fout)
            {
                Console.WriteLine(fout.Message);
            }
            finally
            {
                connectie.Close();
            }
        }

        public void DeleteVoorkeur(int id)
        {
            try
            {
                connectie.Open();
                var command = connectie.CreateCommand();
                command.Parameters.AddWithValue("@voorkeur_id", id);
                command.CommandText = "Delete from Bekwaamheid where Bekwaam_Id=@voorkeur_id";
                command.ExecuteNonQuery();

            }
            catch (SqlException fout)
            {
                Console.WriteLine(fout.Message);
            }
            finally
            {
                connectie.Close();
            }
        }

        public List<Traject> GetTrajecten()
        {
            try
            {
                connectie.Open();

                var cmd = new SqlCommand("Select * FROM dbo.Traject", connectie);
                var reader = cmd.ExecuteReader();

                var trajecten = new List<Traject>();

                while (reader.Read())
                {
                    var traject = new Traject
                    {
                        TrajectId = (int)reader["TrajectId"],
                        TrajectNaam = reader["TrajectNaam"]?.ToString(),
                    };

                    trajecten.Add(traject);
                }

                connectie.Close();

                return trajecten;
            }
            catch (SqlException fout)
            {
                Console.WriteLine(fout.Message);
                throw;
            }
        }

        public List<Eenheid> GetEenhedenByTrajectId(int trajectId)
        {
            try
            {
                connectie.Open();

                var cmd = new SqlCommand("SELECT * FROM dbo.Eenheid WHERE Eenheid.trajectId = @trajectid", connectie);
                cmd.Parameters.AddWithValue("@trajectid", trajectId);
                var reader = cmd.ExecuteReader();

                var eenheden = new List<Eenheid>();

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

                connectie.Close();

                return eenheden;
            }
            catch (SqlException fout)
            {
                Console.WriteLine(fout.Message);
                throw;
            }
        }

        public List<Onderdeel> GetOnderdelenByEenheidId(int eenheidId)
        {
            try
            {
                connectie.Open();

                var cmd = new SqlCommand("SELECT * FROM dbo.Onderdeel WHERE Onderdeel.EenheidId = @eenheidId", connectie);
                cmd.Parameters.AddWithValue("@eenheidId", eenheidId);
                var reader = cmd.ExecuteReader();

                var onderdelen = new List<Onderdeel>();

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

                connectie.Close();

                return onderdelen;
            }
            catch (SqlException fout)
            {
                Console.WriteLine(fout.Message);
                throw;

            }
        }

        public List<Taak> GetTakenByOnderdeelId(int onderdeelId)
        {
            try
            {
                connectie.Open();

                var cmd = new SqlCommand("SELECT * FROM dbo.Taak WHERE Taak.OnderdeelId = @onderdeelId", connectie);
                cmd.Parameters.AddWithValue("@onderdeelId", onderdeelId);
                var reader = cmd.ExecuteReader();

                var taken = new List<Taak>();

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

                connectie.Close();

                return taken;
            }
            catch (SqlException fout)
            {
                Console.WriteLine(fout.Message);
                throw;

            }
        }

        public string GetTaakInfo(int taakId)
        {
            try
            {
                connectie.Open();

                var cmd = new SqlCommand("SELECT Omschrijving FROM dbo.Taak WHERE Taak.TaakId = @taakId", connectie);
                cmd.Parameters.AddWithValue("@taakId", taakId);
                var reader = cmd.ExecuteReader();

                string info = "";

                if (reader.Read())
                {
                    info = reader["Omschrijving"]?.ToString();
                }

                connectie.Close();
                return info;
            }
            catch (SqlException fout)
            {
                Console.WriteLine(fout.Message);
                throw;
            }
        }

        public List<Medewerker> GetDocentenList(string user_id)
        {
            try
            {
                connectie.Open();

                var getteam = new SqlCommand("select TeamID from Docent where MedewerkerID = @user_id", connectie);
                getteam.Parameters.AddWithValue("@user_id", user_id);

                int team_id = (int)getteam.ExecuteScalar();


                var cmd = new SqlCommand("SELECT * FROM Docent where TeamID = @TeamID", connectie);
                cmd.Parameters.AddWithValue("@TeamID", team_id);
                var reader = cmd.ExecuteReader();

                var List = new List<Medewerker>();

                while (reader.Read())
                {
                    var Info = new Medewerker
                    {
                        MedewerkerId = (string)reader["MedewerkerID"],
                        Naam = (string)reader["Naam"],
              
                    };

                    List.Add(Info);
                }

                connectie.Close();

                return List;
            }
            catch (SqlException fout)
            {
                Console.WriteLine(fout.Message);
                throw;

            }
        }

        public bool KijkVoorDubbel(Voorkeur voorkeur, string id)
        {
            try
            {
                connectie.Open();
                var cmdTraject = connectie.CreateCommand();
                cmdTraject.CommandText = "SELECT TrajectNaam FROM Traject WHERE TrajectId = '" + voorkeur.TrajectNaam + "'";
                var resultTraject = cmdTraject.ExecuteScalar();

                var cmdEenheid = connectie.CreateCommand();
                cmdEenheid.CommandText = "SELECT EenheidNaam FROM Eenheid WHERE EenheidId = '" + voorkeur.EenheidNaam + "'";
                var resultEenheid = cmdEenheid.ExecuteScalar();

                var cmdOnderdeel = connectie.CreateCommand();
                cmdOnderdeel.CommandText = "SELECT OnderdeelNaam FROM Onderdeel WHERE OnderdeelId = '" + voorkeur.OnderdeelNaam + "'";
                var resultOnderdeel = cmdOnderdeel.ExecuteScalar();

                var cmdTaak = connectie.CreateCommand();
                cmdTaak.CommandText = "SELECT TaakNaam FROM Taak WHERE TaakId = '" + voorkeur.TaakNaam + "'";
                var resultTaak = cmdTaak.ExecuteScalar();

                var cmdid = connectie.CreateCommand();
                cmdid.CommandText = "SELECT DocentID FROM Docent WHERE MedewerkerID = '" + id + "'";
                var ResultId = cmdid.ExecuteScalar();

                var cmd = new SqlCommand("SELECT Count(*) FROM Bekwaamheid where Docent_id = @User_id and Traject = @traject and Eenheid= @eenheid and Onderdeel = @onderdeel and Taak=@taak", connectie);
                cmd.Parameters.AddWithValue("@User_id", ResultId);
                cmd.Parameters.AddWithValue("@traject", resultTraject);
                cmd.Parameters.AddWithValue("@eenheid", resultEenheid);
                cmd.Parameters.AddWithValue("@onderdeel", resultOnderdeel);
                cmd.Parameters.AddWithValue("@taak", resultTaak);
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
            catch (SqlException fout)
            {
                Console.WriteLine(fout.Message);
                throw;

            }
            finally
            {
                connectie.Close();
            }
        }

        public List<Traject> GetTrajectenInzetbaar(string user_id)
        {
            try
            {
                connectie.Open();

                var cmd = new SqlCommand("Select * FROM dbo.Traject", connectie);
                var reader = cmd.ExecuteReader();

                var trajecten = new List<Traject>();

                while (reader.Read())
                {
                    var traject = new Traject
                    {
                        TrajectId = (int)reader["TrajectId"],
                        TrajectNaam = reader["TrajectNaam"]?.ToString(),
                    };

                    trajecten.Add(traject);
                }

                connectie.Close();

                return trajecten;
            }
            catch (SqlException fout)
            {
                Console.WriteLine(fout.Message);
                throw;
            }
        }

        public void InvoegenTaakVoorkeur(int id, int prioriteit, string User_id)
        {
         
            string constring = connectie.ConnectionString;
            try
            {
                using (SqlConnection connectie = new SqlConnection(constring))
                {
                    connectie.Open();
                    var cmdid = connectie.CreateCommand();
                    cmdid.CommandText = "SELECT DocentID FROM Docent WHERE MedewerkerID = '" + User_id + "'";
                    var ResultId = cmdid.ExecuteScalar();




                    using (SqlCommand command = new SqlCommand("insert into DocentVoorkeur values(@user_id, @Prioriteit, @Bekwaamheid_id)"))
                    {
                        command.Connection = connectie;
                        command.Parameters.Add(new SqlParameter("user_id", ResultId));
                        command.Parameters.Add(new SqlParameter("Prioriteit", prioriteit));
                        command.Parameters.Add(new SqlParameter("Bekwaamheid_id", id));

                        command.ExecuteNonQuery();

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
                using (SqlConnection connectie = new SqlConnection(constring))
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
    }
}