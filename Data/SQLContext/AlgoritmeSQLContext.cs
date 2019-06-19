using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Diagnostics;
using System.Text;
using Data.Interfaces;
using Model;
using Model.AlgoritmeMap;
using Model.Onderwijsdelen;

namespace Data.Context
{
    public class AlgoritmeSQLContext : IAlgoritmeContext
    {
       
        SqlConnection sqlConnection = new SqlConnection("Data Source=mssql.fhict.local;User ID=dbi410994;Password=Test123!;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False");
        string constring = "Data Source = mssql.fhict.local; User ID = dbi410994; Password = Test123!; Connect Timeout = 30; Encrypt = False; TrustServerCertificate = False; ApplicationIntent = ReadWrite; MultiSubnetFailover = False";


        private SqlConnection connectie { get; }
        private DBconn dbconn = new DBconn();
        private string _connectie;


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
                using (SqlConnection Sqlconnectie = new SqlConnection("Data Source=mssql.fhict.local;User ID=dbi410994;Password=Test123!;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False"))
                {
                    using (SqlCommand cmd = new SqlCommand())
                    {
                        Int32 rowsAffected;

                        cmd.CommandText = "DeleteAlgoritmeTabel";
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Connection = Sqlconnectie;

                        Sqlconnectie.Open();

                        rowsAffected = cmd.ExecuteNonQuery();
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
                constring = "Data Source = mssql.fhict.local; User ID = dbi410994; Password = Test123!; Connect Timeout = 30; Encrypt = False; TrustServerCertificate = False; ApplicationIntent = ReadWrite; MultiSubnetFailover = False";
                using (SqlConnection con = new SqlConnection(constring))
                {
                    con.Open();
                    using (var command = new SqlCommand("Select * From Taak", con))
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
            constring = "Data Source = mssql.fhict.local; User ID = dbi410994; Password = Test123!; Connect Timeout = 30; Encrypt = False; TrustServerCertificate = False; ApplicationIntent = ReadWrite; MultiSubnetFailover = False";
            using (SqlConnection con = new SqlConnection(constring))
            {
                con.Open();
                using (var command = new SqlCommand("SELECT D.* " +
                                                    "FROM Docent as D " +
                                                    "INNER JOIN Bekwaamheid as B ON D.DocentID = B.Docent_id " +
                                                    "WHERE B.TaakID = @taakID", con))
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
                constring = "Data Source = mssql.fhict.local; User ID = dbi410994; Password = Test123!; Connect Timeout = 30; Encrypt = False; TrustServerCertificate = False; ApplicationIntent = ReadWrite; MultiSubnetFailover = False";
                using (SqlConnection con = new SqlConnection(constring))
                {
                    con.Open();
                    using (SqlCommand cmd = new SqlCommand("SELECT b.TaakID, d.Prioriteit, b.Taak, t.BenodigdeUren, d.Ingedeeld,d.VoorkeurID from Bekwaamheid b inner join DocentVoorkeur d ON b.Bekwaam_Id = d.Bekwaamheid_id inner join Taak t ON b.TaakID = t.TaakID  WHERE d.DocentID = @docentID ", con))
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

        public  void ZetinDbNull(int taakID)
        {
            try
            {
                string constring = "Data Source = mssql.fhict.local; User ID = dbi410994; Password = Test123!; Connect Timeout = 30; Encrypt = False; TrustServerCertificate = False; ApplicationIntent = ReadWrite; MultiSubnetFailover = False";

                using (SqlConnection connectie = new SqlConnection(constring))
                {
                    connectie.Open();
                    using (SqlCommand command = new SqlCommand("insert into EindTabelAlgoritme (Taak_id, Docent_id) values (@taak_id,NULL)", connectie))
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
                constring = "Data Source = mssql.fhict.local; User ID = dbi410994; Password = Test123!; Connect Timeout = 30; Encrypt = False; TrustServerCertificate = False; ApplicationIntent = ReadWrite; MultiSubnetFailover = False";
                using (SqlConnection con = new SqlConnection(constring))
                {
                    con.Open();
                    using (SqlCommand command = new SqlCommand("insert into EindTabelAlgoritme (Docent_id, Taak_id) Values(@docent_id,@taak_id)", con))
                    {
                        command.Parameters.AddWithValue("@docent_id", docentID);
                        command.Parameters.AddWithValue("@taak_id", taakID);
                        command.ExecuteNonQuery();
                    }
                    using (SqlCommand command = new SqlCommand("UPDATE Docent set RuimteVoorInzet = (SELECT RuimteVoorInzet FROM Docent WHERE DocentID = @docent_id) - (SELECT (BenodigdeUren / Aantal_Klassen) from taak where TaakId = @taak_id) where DocentID = @docent_id ", con))
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
                string constring = "Data Source = mssql.fhict.local; User ID = dbi410994; Password = Test123!; Connect Timeout = 30; Encrypt = False; TrustServerCertificate = False; ApplicationIntent = ReadWrite; MultiSubnetFailover = False";

                using (SqlConnection connectie = new SqlConnection(constring))
                {
                    connectie.Open();
                    using (SqlCommand command = new SqlCommand("update docentVoorkeur set ingedeeld=1 where DocentID= @docent_id and VoorkeurID = @voorkeur_id", connectie))
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