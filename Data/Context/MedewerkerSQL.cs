using Model;
using System.Data.SqlClient;

namespace Data.Context
{
    public class MedewerkerSQL
    {
        private SqlConnection conn;
        private DBconn dbconn = new DBconn();

        public medewerker getmedewerkerid(string id)
        {
            conn = dbconn.GetConnString();
            conn.Open();
            var cmd = new SqlCommand("SELECT id, Email FROM AspNetUsers where id = @id", conn);
           
            cmd.Parameters.AddWithValue("@id", id);
            var reader = cmd.ExecuteReader();

            medewerker medewerker_new = new medewerker();

            while (reader.Read())
            {
                medewerker_new.id = (string)reader["id"];
                medewerker_new.email = (string)reader["Email"];
            }
            reader.Close();
            var cmd2 = new SqlCommand("select roleId from AspNetUserRoles where UserId = @id", conn);
            cmd2.Parameters.AddWithValue("@id", id);
            var reader2 = cmd2.ExecuteReader();
            while (reader2.Read())
            {
                medewerker_new.role_id = (string)reader2["roleId"];
            }

            conn.Close();
            return medewerker_new;
        }
    }
}