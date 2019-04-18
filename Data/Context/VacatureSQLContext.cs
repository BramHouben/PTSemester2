using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Text;
using Data.Interfaces;
using Model;

namespace Data.Context
{
    class VacatureSQLContext : IVacatureContext
    {
        private SqlConnection connectie;

        private DBconn dbconn = new DBconn();
        public List<Vacature> VacaturesOphalen()
        {
            connectie = dbconn.GetConnString();
            if (connectie.State != ConnectionState.Open)
            {
                connectie.Open();
            }
            var cmd = connectie.CreateCommand();
            cmd.CommandText = "SELECT * FROM Vacature";
            SqlDataReader reader = cmd.ExecuteReader();
            List<Vacature> vacatures = new List<Vacature>();
            while (reader.Read())
            {
                Vacature vacature = new Vacature
                {
                    Omschrijving = (string)reader["Omschrijving"]?.ToString(),
                    Naam = (string)reader["Vacature_Naam"]
                };
                if (reader["OnderwijstaakID"] != DBNull.Value)
                {
                    vacature.OnderwijstaakID = Convert.ToInt32(reader["OnderwijstaakID"]);
                }

                if (reader["VacatureID"] != DBNull.Value)
                {
                    vacature.VactureID = Convert.ToInt32(reader["VacatureID"]);
                }

                vacatures.Add(vacature);
            }
            connectie.Close();
            return vacatures;

        }

        public void VacatureOpslaan(Vacature vac)
        {
            try
            {
                connectie = dbconn.GetConnString();
                if (connectie.State != ConnectionState.Open)
                {
                    connectie.Open();
                }
                var cmd = connectie.CreateCommand();
                cmd.Parameters.AddWithValue("@VacatureNaam", vac.Naam);
                if (vac.Omschrijving == null)
                {
                    cmd.Parameters.AddWithValue("@Omschrijving", DBNull.Value);
                }
                else
                {
                    cmd.Parameters.AddWithValue("@Omschrijving", vac.Omschrijving);
                }
                cmd.Parameters.AddWithValue("@OnderwijstaakID", vac.OnderwijstaakID);
                cmd.CommandText = "INSERT INTO [dbo].[Vacature]([Vacature_naam],[Omschrijving],[OnderwijstaakID]) VALUES (@VacatureNaam,@Omschrijving,@OnderwijstaakID)";
                cmd.ExecuteNonQuery();
            }
            catch (SqlException Fout)
            {
                Console.WriteLine(Fout.Message);
            }
            finally
            {
                if (connectie.State != ConnectionState.Closed)
                {
                    connectie.Close();
                }
            }
        }

        public void DeleteVacature(int id)
        {
            try
            {
                connectie = dbconn.GetConnString();
                connectie.Open();
                var command = connectie.CreateCommand();
                command.Parameters.AddWithValue("@VacatureID", id);
                command.CommandText = "Delete from Vacature where VacatureID=@VacatureID";
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

        public Vacature VacatureOphalen(int id)
        {
            connectie = dbconn.GetConnString();
            if (connectie.State != ConnectionState.Open)
            {
                connectie.Open();
            }
            var cmd = connectie.CreateCommand();
            cmd.CommandText = "SELECT * FROM Vacature WHERE VacatureID = @VacatureID";
            cmd.Parameters.AddWithValue("@VacatureID", id);
            SqlDataReader reader = cmd.ExecuteReader();
            Vacature vacature = new Vacature();
            if (reader.Read())
            {

                vacature.Omschrijving = (string)reader["Omschrijving"]?.ToString();
                vacature.Naam = (string)reader["Vacature_Naam"];
                vacature.OnderwijstaakID = Convert.ToInt32(reader["OnderwijstaakID"]);
                vacature.VactureID = Convert.ToInt32(reader["VacatureID"]);
            }
            connectie.Close();
            return vacature;
        }
    }
}