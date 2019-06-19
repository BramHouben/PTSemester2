using Data.Interfaces;
using Model;
using Model.AlgoritmeMap;
using Model.Onderwijsdelen;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Diagnostics;

namespace Data.Context
{
    public class AlgoritmeSQLContext : IAlgoritmeContext
    {
        private SqlConnection connectie { get; }
        private DBconn dbconn = new DBconn();
        private string _connectie;
        
        public AlgoritmeSQLContext()
        {
            connectie = dbconn.GetConnString();
        }

        private List<ATaak> taken = new List<ATaak>();

        public List<Algoritme> ActiverenSysteem()
        {
            try
            {
                List<Algoritme> algoritmes = new List<Algoritme>();

                using (SqlConnection connectie = dbconn.GetConnString())
                {
                    connectie.Open();
                    using (SqlCommand command = new SqlCommand("SELECT Eind.*, D.Naam, D.TeamID, T.TaakNaam " +
                                                               "FROM EindTabelAlgoritme as Eind " +
                                                               "LEFT JOIN Docent as D ON D.DocentID = eind.Docent_id " +
                                                               "INNER JOIN Taak as T ON T.TaakId = Eind.Taak_id", connectie))
                    {
                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                Docent docent = new Docent();

                                if (reader["Docent_id"] != System.DBNull.Value)
                                {
                                    docent.DocentId = (int?)reader["Docent_id"];
                                }
                                if (reader["TeamId"] != System.DBNull.Value)
                                {
                                    docent.TeamId = (int?)reader["TeamID"];
                                }
                                docent.Naam = reader["Naam"].ToString();

                                Taak taak = new Taak();
                                taak.TaakId = (int)reader["Taak_id"];
                                taak.TaakNaam = reader["TaakNaam"].ToString();

                                Algoritme algoritme = new Algoritme();
                                algoritme.AlgoritmeId = (int)reader["Row_id"];
                                algoritme.Taak = taak;
                                algoritme.Docent = docent;

                                algoritmes.Add(algoritme);
                            }
                        }
                    }
                }
                return algoritmes;
            }
            catch (SqlException fout)
            {
                Debug.WriteLine(fout.Message);
                throw new ArgumentException("Er is iets fout gegaan bij het ophalen van de data");
            }
        }

        public void DeleteTabel()
        {
            try
            {
                _connectie = dbconn.ReturnConnectionString();

                using (SqlConnection Sqlconnectie = new SqlConnection(_connectie))
                {
                    Sqlconnectie.Open();
                    using (SqlCommand command = new SqlCommand("DeleteAlgoritmeTabel", Sqlconnectie))
                    {
                        command.CommandType = CommandType.StoredProcedure;
                        command.ExecuteNonQuery();


                    }
                   
                    using (SqlCommand cmd = new SqlCommand("update Docent set RuimteVoorInzet = 600", Sqlconnectie))
                    {
                        cmd.ExecuteNonQuery();
                    }
                }
            }
            catch (SqlException fout)
            {
                Debug.WriteLine(fout.Message);
            }
        }

        public List<ATaak> TakenOphalen()
        {
            taken = new List<ATaak>();
            _connectie = dbconn.ReturnConnectionString();

            using (SqlConnection Sqlconnectie = new SqlConnection(_connectie))
            {
                Sqlconnectie.Open();
                using (var command = new SqlCommand("Select * From Taak", Sqlconnectie))
                {
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            var taak = new ATaak();
                            taak.TaakID = (int)reader["TaakID"];
                            taak.TaakNaam = reader["TaakNaam"].ToString();
                            taak.BenodigdeUren = (int)reader["BenodigdeUren"];
                            taak.AantalKlassen = (int)reader["Aantal_Klassen"];

                            taken.Add(taak);
                        }
                    }
                }
            }
            return taken;
        }

        public List<ADocent> InzetbareDocenten(int taakID)
        {
            List<ADocent> docenten = new List<ADocent>();

            _connectie = dbconn.ReturnConnectionString();

            using (SqlConnection Sqlconnectie = new SqlConnection(_connectie))
            {
                Sqlconnectie.Open();
                using (var command = new SqlCommand("SELECT D.* " +
                                                    "FROM Docent as D " +
                                                    "INNER JOIN Bekwaamheid as B ON D.DocentID = B.Docent_id " +
                                                    "WHERE B.TaakID = @taakID", Sqlconnectie))
                {
                    command.Parameters.AddWithValue("@taakID", taakID);
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            var docent = new ADocent();

                            docent.docentID = (int)reader["DocentId"];
                            docent.InzetbareUren = (int)reader["RuimteVoorInzet"];
                            docent.voorkeuren = OphalenVoorkeuren(docent.docentID);

                            docenten.Add(docent);
                        }
                    }
                }
            }
            return docenten;
        }

        private List<AVoorkeur> OphalenVoorkeuren(int docentID)
        {
            List<AVoorkeur> voorkeuren = new List<AVoorkeur>();
            try
            {
                _connectie = dbconn.ReturnConnectionString();

                using (SqlConnection Sqlconnectie = new SqlConnection(_connectie))
                {
                    Sqlconnectie.Open();
                    using (SqlCommand cmd = new SqlCommand("SELECT b.TaakID, d.Prioriteit, b.Taak, t.BenodigdeUren, d.Ingedeeld,d.VoorkeurID from Bekwaamheid b inner join DocentVoorkeur d ON b.Bekwaam_Id = d.Bekwaamheid_id inner join Taak t ON b.TaakID = t.TaakID  WHERE d.DocentID = @docentID ", Sqlconnectie))
                    {
                        cmd.Parameters.AddWithValue("@docentID", docentID);
                        using (SqlDataReader reader = cmd.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                AVoorkeur voorkeur = new AVoorkeur();
                                voorkeur.VoorkeurID = (int)reader["VoorkeurID"];
                                voorkeur.TaakID = (int)reader["TaakID"];
                                voorkeur.TaakNaam = (string)reader["Taak"];
                                voorkeur.Ingedeeld = (Boolean)reader["Ingedeeld"];
                                voorkeur.Prioriteit = (int)reader["Prioriteit"];
                                if (DBNull.Value.Equals(reader["BenodigdeUren"]))
                                {
                                    voorkeur.BenodigdeUren = 0;
                                }
                                else
                                {
                                    voorkeur.BenodigdeUren = (int)reader["BenodigdeUren"];
                                }
                                voorkeuren.Add(voorkeur);
                            }
                        }
                    }
                }
            }
            catch (SqlException fout)
            {
                Console.WriteLine(fout.Message);
            }
            return voorkeuren;
        }

        public void ZetinDbNull(int taakID)
        {
            try
            {
                _connectie = dbconn.ReturnConnectionString();

                using (SqlConnection Sqlconnectie = new SqlConnection(_connectie))
                {
                    Sqlconnectie.Open();
                    using (SqlCommand command = new SqlCommand("insert into EindTabelAlgoritme (Taak_id, Docent_id) values (@taak_id,NULL)", Sqlconnectie))
                    {
                        command.Parameters.AddWithValue("@taak_id", taakID);
                        command.ExecuteNonQuery();
                    }
                }
            }
            catch (SqlException fout)
            {
                Console.WriteLine(fout.Message);
            }
        }

        public void ZetinDb(int docentID, int taakID)
        {
            try
            {
                _connectie = dbconn.ReturnConnectionString();

                using (SqlConnection Sqlconnectie = new SqlConnection(_connectie))
                {
                    Sqlconnectie.Open();
                    using (SqlCommand command = new SqlCommand("insert into EindTabelAlgoritme (Docent_id, Taak_id) Values(@docent_id,@taak_id)", Sqlconnectie))
                    {
                        command.Parameters.AddWithValue("@docent_id", docentID);
                        command.Parameters.AddWithValue("@taak_id", taakID);
                        command.ExecuteNonQuery();
                    }
                    using (SqlCommand command = new SqlCommand("UPDATE Docent set RuimteVoorInzet = (SELECT RuimteVoorInzet FROM Docent WHERE DocentID = @docent_id) - (SELECT (BenodigdeUren / Aantal_Klassen) from taak where TaakId = @taak_id) where DocentID = @docent_id ", Sqlconnectie))
                    {
                        command.Parameters.AddWithValue("@docent_id", docentID);
                        command.Parameters.AddWithValue("@taak_id", taakID);
                        command.ExecuteNonQuery();
                    }
                }
            }
            catch (SqlException foutje)
            {
                Console.WriteLine(foutje.Message);
            }
        }

        public void VerwijderVoorkeur(int docentID, int iD)
        {
            try
            {
                _connectie = dbconn.ReturnConnectionString();

                using (SqlConnection Sqlconnectie = new SqlConnection(_connectie))
                {
                    Sqlconnectie.Open();
                    using (SqlCommand command = new SqlCommand("update docentVoorkeur set ingedeeld=1 where DocentID= @docent_id and VoorkeurID = @voorkeur_id", Sqlconnectie))
                    {
                        command.Parameters.AddWithValue("@Voorkeur_id", iD);
                        command.Parameters.AddWithValue("@docent_id", docentID);
                        command.ExecuteNonQuery();
                    }
                }
            }
            catch (SqlException fout)
            {
                Console.WriteLine(fout.Message);
            }
        }
    }
}