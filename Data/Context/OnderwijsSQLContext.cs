using Data.Interfaces;
using Model;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Text;

namespace Data.Context
{
    public class OnderwijsSQLContext : IOnderwijsContext
    {
        private SqlConnection connectie;
        private DBconn dbconn = new DBconn();



        public IEnumerable<Onderwijstaak> OnderwijsTaakOphalen()
        {
            List<Onderwijstaak> OnderwijsTaakList = new List<Onderwijstaak>();

            connectie = dbconn.GetConnString();
            connectie.Open();

            try
            {
                var query = "SELECT Omschrijving FROM OnderwijsTaak";
                var command = new SqlCommand(query, connectie);
                var reader = command.ExecuteReader();

                while(reader.Read())
                {
                    var onderwijstaak = new Onderwijstaak();
                    onderwijstaak.Omschrijving = (string)reader["Omschrijving"];

                    OnderwijsTaakList.Add(onderwijstaak);
                }
            }

            catch (SqlException ex)
            {
                Console.WriteLine(ex.Message);
            }

            finally
            {
                connectie.Close();
            }

            return OnderwijsTaakList;
        }

        public IEnumerable<Onderwijstraject> OnderwijsTrajectOphalen()
        {
            List<Onderwijstraject> OnderwijsTrajectList = new List<Onderwijstraject>();

            connectie = dbconn.GetConnString();
            connectie.Open();

            try
            {
                var query = "SELECT Omschrijving FROM OnderwijsTraject";
                var command = new SqlCommand(query, connectie);
                var reader = command.ExecuteReader();

                while (reader.Read())
                {
                    var onderwijstraject = new Onderwijstraject();
                    onderwijstraject.Omschrijving = (string)reader["Omschrijving"];

                    OnderwijsTrajectList.Add(onderwijstraject);
                }
            }

            catch (SqlException ex)
            {
                Console.WriteLine(ex.Message);
            }

            finally
            {
                connectie.Close();
            }

            return OnderwijsTrajectList;
        }

        public IEnumerable<Onderwijsonderdeel> OnderwijsOnderdeelOphalen()
        {
            List<Onderwijsonderdeel> OnderwijsOnderdeelList = new List<Onderwijsonderdeel>();

            connectie = dbconn.GetConnString();
            connectie.Open();

            try
            {
                var query = "SELECT Omschrijving FROM OnderwijsOnderdeel";
                var command = new SqlCommand(query, connectie);
                var reader = command.ExecuteReader();

                while (reader.Read())
                {
                    var onderwijsonderdeel = new Onderwijsonderdeel();
                    onderwijsonderdeel.Omschrijving = (string)reader["Omschrijving"];

                    OnderwijsOnderdeelList.Add(onderwijsonderdeel);
                }
            }

            catch (SqlException ex)
            {
                Console.WriteLine(ex.Message);
            }

            finally
            {
                connectie.Close();
            }

            return OnderwijsOnderdeelList;
        }

        public IEnumerable<Onderwijseenheid> OnderwijsEenheidOphalen()
        {
            List<Onderwijseenheid> OnderwijsEenheidList = new List<Onderwijseenheid>();

            connectie = dbconn.GetConnString();
            connectie.Open();

            try
            {
                var query = "SELECT Omschrijving FROM OnderwijsEenheid";
                var command = new SqlCommand(query, connectie);
                var reader = command.ExecuteReader();

                while (reader.Read())
                {
                    var onderwijseenheid = new Onderwijseenheid();
                    onderwijseenheid.Omschrijving = (string)reader["Omschrijving"];

                    OnderwijsEenheidList.Add(onderwijseenheid);
                }
            }

            catch (SqlException ex)
            {
                Console.WriteLine(ex.Message);
            }

            finally
            {
                connectie.Close();
            }

            return OnderwijsEenheidList;
        }
    }
}
