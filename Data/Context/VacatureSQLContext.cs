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
            throw new NotImplementedException();
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
                    cmd.Parameters.AddWithValue("@Omschrijving",DBNull.Value);
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
    }
}
