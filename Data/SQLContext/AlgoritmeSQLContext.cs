using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Text;
using Model;

namespace Data.Context
{
    class AlgoritmeSQLContext
    {
        private SqlConnection connectie { get; }
        private DBconn dbconn = new DBconn();
        private string _connectie;
        internal List<Algoritme> ActiverenSysteem()
        {
            Algoritme algoritme = new Algoritme();
            string constring = connectie.ConnectionString;
            try
            {
                using (SqlConnection connectie = dbconn.GetConnString())
                {
             

                    using (SqlCommand command = new SqlCommand("SELECT * FROM Bekwaamheid", connectie))
                    {
                        
                        using (SqlDataReader reader = command.ExecuteReader())
                        {

                            while (reader.Read())
                            {
                                algoritme.TaakID = (int)reader["Taak_id"];
                                algoritme.t
                            }
                        }
                    }
                }
            }
            catch (SqlException Ex)
            {
                Console.WriteLine(Ex.Message);
            }
            
        }
    }
}
