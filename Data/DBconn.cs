using System.Data.SqlClient;

namespace Data
{
    public class DBconn
    {
        internal SqlConnection SqlConnectie { get; }
        private string _connectieString;

        public DBconn()
        {
            _connectieString = "Data Source=mssql.fhict.local;User ID=dbi410994;Password=Test123!;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False";
            SqlConnectie = new SqlConnection(_connectieString);
        }

        public SqlConnection GetConnString()
        {
            return SqlConnectie;
        }
    }
}