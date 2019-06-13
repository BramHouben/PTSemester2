using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Text;
using Data.Interfaces;
using Model;
using Model.Onderwijsdelen;

namespace Data.Context
{
    public class AlgoritmeSQLContext : IAlgoritmeContext
    {
        private SqlConnection connectie { get; }
        private DBconn dbconn = new DBconn();
        private string _connectie;

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
                                                               "INNER JOIN Docent as D ON D.DocentID = eind.Docent_id " +
                                                               "INNER JOIN Taak as T ON T.TaakId = Eind.Taak_id", connectie))
                    {
                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                Docent docent = new Docent();
                                docent.DocentId = (int?)reader["Docent_id"];
                                docent.TeamId = (int?)reader["TeamID"];
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
            catch (SqlException Ex)
            {
                Console.WriteLine(Ex.Message);
                throw new ArgumentException("Er is iets fout gegaan bij het ophalen van de data");
            }
        }
    }
}
