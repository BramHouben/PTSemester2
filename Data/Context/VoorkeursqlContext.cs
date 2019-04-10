using Microsoft.EntityFrameworkCore;
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
            List<Voorkeur> vklistmodel = new List<Voorkeur>();

            connectie.Open();
            var cmd = new SqlCommand("SELECT * FROM Voorkeur where UserId  = @UserId", connectie);
            cmd.Parameters.AddWithValue("@UserId", id);
            var reader = cmd.ExecuteReader();
            while (reader.Read())
            {
                var voorkeur = new Voorkeur();


                voorkeur.Id = (int)reader["Id"];
                voorkeur.Taak_naam = (int)reader["Traject"];
                voorkeur.Prioriteit = (int)reader["Prioriteit"];
                voorkeur.Traject_naam = (int)reader["Eenheid"];
               voorkeur.Taak_naam = (int)reader["Taak"];
                vklistmodel.Add(voorkeur);
            }
            connectie.Close();

            return vklistmodel;
        }



        public void VoorkeurToevoegen(Voorkeur voorkeur, string id)
        {
            try
            {
                connectie.Open();
                var command = connectie.CreateCommand();



                
                command.Parameters.AddWithValue("@Traject", voorkeur.Taak_naam);
                command.Parameters.AddWithValue("@Prioriteit", voorkeur.Prioriteit);
                command.Parameters.AddWithValue("@Eenheid", voorkeur.Onderdeel_naam);
                command.Parameters.AddWithValue("@Traject_naam", voorkeur.Traject_naam);
                command.Parameters.AddWithValue("@UserId", id);
                command.CommandText = "INSERT INTO Voorkeur (Traject, Eenheid,Taak, Prioriteit, UserId) VALUES ( @Traject,@Eenheid, @Prioriteit,@Traject_naam ,@UserId)";
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
                command.CommandText = "Delete from Voorkeur where id=@voorkeur_id";
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

        public List<Onderdeel> GetOnderdelenByTrajectId(int trajectId)
        {
            connectie.Open();

            var cmd = new SqlCommand("SELECT * FROM dbo.Onderdeel WHERE Onderdeel.trajectId = @trajectid", connectie);
            cmd.Parameters.AddWithValue("@trajectid", trajectId);
            var reader = cmd.ExecuteReader();

            var onderdelen = new List<Onderdeel>();

            while (reader.Read())
            {
                var onderdeel = new Onderdeel
                {
                    OnderdeelId = (int)reader["OnderdeelId"],
                    OnderdeelNaam = reader["OnderdeelNaam"]?.ToString(),
                    TrajectId = (int)reader["TrajectId"],

                };

                onderdelen.Add(onderdeel);
            }

            connectie.Close();

            return onderdelen;
        }

        public List<Taak> GetTakenByOnderdeelId(int onderdeelId)
        {
            connectie.Open();

            var cmd = new SqlCommand("SELECT * FROM dbo.Taak WHERE Taak.onderdeelId = @onderdeelId", connectie);
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
                };

                taken.Add(taak);
            }

            connectie.Close();

            return taken;
        }
    }
}