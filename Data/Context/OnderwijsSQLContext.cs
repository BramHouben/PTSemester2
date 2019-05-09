using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Text;
using Data.Interfaces;
using Model;
using Model.Onderwijsdelen;

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
            cmd.CommandText = "SELECT TaakNaam FROM Taak WHERE TaakID = @TaakID";
            cmd.Parameters.AddWithValue("@TaakID", id);
            SqlDataReader reader = cmd.ExecuteReader();
            string result = "";
            if (reader.Read())
            {
                result = reader["TaakNaam"].ToString();
            }
            connectie.Close();
            return result;
        }

        public List<Taak> TakenOphalen()
        {
            List<Taak> taken = new List<Taak>();
            connectie = dbconn.GetConnString();
            if (connectie.State != ConnectionState.Open)
            {
                connectie.Open();
            }
            var cmd = connectie.CreateCommand();
            cmd.CommandText = "SELECT * FROM Taak";
            SqlDataReader reader = cmd.ExecuteReader();
            while (reader.Read())
            {
                Taak taak = new Taak
                {
                    TaakNaam = reader["TaakNaam"].ToString(),
                    TaakId = Convert.ToInt32(reader["TaakId"]),
                    OnderdeelId = Convert.ToInt32(reader["OnderdeelId"]),
                    Omschrijving = reader["Omschrijving"].ToString()
                };
            }
            connectie.Close();
            return taken;
        }

        public void TaakToevoegen(Taak taak)
        {
            throw new NotImplementedException();
        }

        public void TaakVerwijderen(int taakId)
        {
            throw new NotImplementedException();
        }

        public Taak TaakOphalen(int id)
        {
            throw new NotImplementedException();
        }

        public void UpdateTaak(Taak taak)
        {
            throw new NotImplementedException();
        }

        public List<Traject> GetTrajecten()
        {
            throw new NotImplementedException();
        }

        public List<Eenheid> GetEenhedenByTrajectId(int trajectId)
        {
            throw new NotImplementedException();
        }

        public List<Onderdeel> GetOnderdelenByEenheidId(int eenheidId)
        {
            throw new NotImplementedException();
        }

        public List<Taak> GetTakenByOnderdeelId(int onderdeelId)
        {
            throw new NotImplementedException();
        }
    }
}