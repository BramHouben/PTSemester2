using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Text;
using Data.Interfaces;
using Model;

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
                    using (SqlCommand command = new SqlCommand("SELECT Eind.*, D.Naam, D.TeamID " +
                                                               "FROM EindTabelAlgoritme as Eind " +
                                                               "INNER JOIN Docent as D ON D.DocentID = eind.Docent_id", connectie))
                    {
                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                Docent docent = new Docent();
                                docent.DocentId = (int)reader["Docent_id"];
                                docent.TeamId = (int?)reader["TeamID"];
                                docent.Naam = reader["Naam"].ToString();

                                Algoritme algoritme = new Algoritme();
                                algoritme.AlgoritmeId = (int)reader["Row_id"];
                                algoritme.TaakID = (int)reader["Taak_id"];
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
