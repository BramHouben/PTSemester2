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
    class BlokeigenaarSQLContext : IBlokeigenaarContext
    {
        private SqlConnection connectie;
        private DBconn dbconn = new DBconn();
        
        public List<Taak> TakenOphalen()
        {
            connectie = dbconn.GetConnString();
            if (connectie.State != ConnectionState.Open)
            {
                connectie.Open();
            }
            var cmd = connectie.CreateCommand();

            cmd.CommandText = "SELECT * FROM Taak";
            SqlDataReader reader = cmd.ExecuteReader();
            List<Taak> taken = new List<Taak>();
            while (reader.Read())
            {
                Taak taak = new Taak
                {
                    Taak_info = reader["Taak_info"]?.ToString(),
                    TaakNaam = (string)reader["TaakNaam"]
                };
                if (reader["TaakId"] != DBNull.Value)
                {
                    taak.TaakId = Convert.ToInt32(reader["TaakId"]);
                }
                

                taken.Add(taak);
            }
            connectie.Close();
            return taken;
        }

        public void TaakToevoegen(Taak taak)
        {
            //try
            //{
            //    connectie = dbconn.GetConnString();
            //    if (connectie.State != ConnectionState.Open)
            //    {
            //        connectie.Open();
            //    }
            //    var cmd = connectie.CreateCommand();
            //    cmd.Parameters.AddWithValue("@VacatureNaam", vac.Naam);
            //    if (vac.Omschrijving == null)
            //    {
            //        cmd.Parameters.AddWithValue("@Omschrijving", DBNull.Value);
            //    }
            //    else
            //    {
            //        cmd.Parameters.AddWithValue("@Omschrijving", vac.Omschrijving);
            //    }
            //    cmd.Parameters.AddWithValue("@TaakID", vac.TaakID);
            //    cmd.CommandText = "INSERT INTO [dbo].[Vacature]([Vacature_naam],[Omschrijving],[TaakID]) VALUES (@VacatureNaam,@Omschrijving,@TaakID)";
            //    cmd.ExecuteNonQuery();
            //}
            //catch (SqlException Fout)
            //{
            //    Console.WriteLine(Fout.Message);
            //}
            //finally
            //{
            //    if (connectie.State != ConnectionState.Closed)
            //    {
            //        connectie.Close();
            //    }
            //}
        }

        public void TaakVerwijderen(int taakId)
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

        /*******************************************************************************************************/
        
        /*
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
                cmd.Parameters.AddWithValue("@TaakID", vac.TaakID);
                cmd.CommandText = "INSERT INTO [dbo].[Vacature]([Vacature_naam],[Omschrijving],[TaakID]) VALUES (@VacatureNaam,@Omschrijving,@TaakID)";
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
                vacature.TaakID = Convert.ToInt32(reader["TaakID"]);
                vacature.VacatureID = Convert.ToInt32(reader["VacatureID"]);
            }
            connectie.Close();
            return vacature;
        }

        public void UpdateVacature(Vacature vac)
        {
            
            try
            {
                connectie = dbconn.GetConnString();
                connectie.Open();
                var command = connectie.CreateCommand();
                command.Parameters.AddWithValue("@ID", vac.VacatureID);
                command.Parameters.AddWithValue("@Naam", vac.Naam);
                command.Parameters.AddWithValue("@Omschrijving", vac.Omschrijving);
                command.Parameters.AddWithValue("@TaakID", vac.TaakID);
                command.CommandText = "UPDATE [dbo].[Vacature] SET [Vacature_naam] = @Naam,[Omschrijving] = @Omschrijving,[TaakID] = @TaakID WHERE VacatureID = @ID";
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
        */
    }
    
}