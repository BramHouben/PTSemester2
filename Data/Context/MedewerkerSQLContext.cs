using System;
using Data.Interfaces;
using Model;
using Model.Onderwijsdelen;
using System.Data.SqlClient;

namespace Data.Context
{
    public class MedewerkerSQLContext : IMedewerkerContext
    {
        private SqlConnection conn;
        private DBconn dbconn = new DBconn();
        private string _connectie;

        public Medewerker GetMedewerkerId(string id)
        {
            Medewerker medewerker_new = new Medewerker();
            try
            {
                using (SqlConnection conn = dbconn.GetConnString())
                {
                    conn.Open();
                    using (SqlCommand cmd = new SqlCommand("SELECT id, Email FROM AspNetUsers WHERE id = @id", conn),
                        cmd2 = new SqlCommand("SELECT roleId FROM AspNetUserRoles WHERE UserId = @id", conn))
                    {
                        cmd.Parameters.AddWithValue("@id", id);
                        cmd2.Parameters.AddWithValue("@id", id);
                        using (SqlDataReader reader = cmd.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                medewerker_new.MedewerkerId = (string)reader["id"];
                                medewerker_new.Email = (string)reader["Email"];
                            }
                        }
                        using (SqlDataReader reader2 = cmd2.ExecuteReader())
                        {
                            while (reader2.Read())
                            {
                                medewerker_new.Role_id = (string)reader2["roleId"];
                            }
                        }
                    }
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                return medewerker_new;
            }
            return medewerker_new;
        }

        public void VoegAantalUrenToeMBVAantalKlassen(Eenheid eenheid)
        {

        }
    }
}