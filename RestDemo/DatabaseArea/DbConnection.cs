using MySql.Data.MySqlClient;

namespace RestDemo.DatabaseArea
{
    public class DbConnection
    {
        private static string host = "localhost";
        private static string database = "golont";
        private static string uid = "root";
        private static string password = "";

        public DbConnection(string host, string database, string uid, string password)
        {
            DbConnection.host = host;
            DbConnection.database = database;
            DbConnection.uid = uid;
            DbConnection.password = password;
        }

        public MySqlConnection openConection(string host, string database, string uid, string password)
        {
            MySqlConnection connection =
                new MySqlConnection("server=" + host + ";database=" + database + ";uid=" + uid + ";pwd=" + password +
                                    ";");
            connection.Open();
            return connection;
        }
        
        public static MySqlConnection openConection()
        {
            MySqlConnection connection =
                new MySqlConnection("server=" + host + ";database=" + database + ";uid=" + uid + ";pwd=" + password +
                                    ";");
            connection.Open();
            return connection;
        }
    }
}