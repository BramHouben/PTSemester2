using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Text;
using Data.Interfaces;
using Model;

namespace Data.Context
{
    class OnderwijsSQLContext : IOnderwijsContext
    {
        private SqlConnection connectie;

        private DBconn dbconn = new DBconn();
        public string OnderwijstaakNaam(int id)
        {
            connectie = dbconn.GetConnString();
            if (connectie.State != ConnectionState.Open)
            {
                connectie.Open();
            }
            var cmd = connectie.CreateCommand();
            cmd.CommandText = "SELECT Omschrijving FROM OnderwijsTaak WHERE OnderwijsTaakID = @OnderwijsTaakID";
            cmd.Parameters.AddWithValue("@OnderwijsTaakID", id);
            SqlDataReader reader = cmd.ExecuteReader();
            string result = "";
            if (reader.Read())
            {
                result = reader["Omschrijving"].ToString();
            }
            connectie.Close();
            return result;
        }
    }
}