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
            conn = dbconn.GetConnString();
            conn.Open();
            var cmd = new SqlCommand("SELECT id, Email FROM AspNetUsers where id = @id", conn);
           
            cmd.Parameters.AddWithValue("@id", id);
            var reader = cmd.ExecuteReader();

            Medewerker medewerker_new = new Medewerker();

            while (reader.Read())
            {
                medewerker_new.MedewerkerId = (string)reader["id"];
                medewerker_new.Email = (string)reader["Email"];
            }
            reader.Close();
            var cmd2 = new SqlCommand("select roleId from AspNetUserRoles where UserId = @id", conn);
            cmd2.Parameters.AddWithValue("@id", id);
            var reader2 = cmd2.ExecuteReader();
            while (reader2.Read())
            {
                medewerker_new.Role_id = (string)reader2["roleId"];
            }

            conn.Close();
            return medewerker_new;
        }

        public void VoegAantalUrenToeMBVAantalKlassen(Eenheid eenheid)
        {

        }
    }
}