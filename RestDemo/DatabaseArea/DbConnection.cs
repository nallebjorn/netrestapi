using MySql.Data.MySqlClient;

namespace RestDemo.DatabaseArea
{
    public class DbConnection
    {
        public static MySqlConnection _instance;

        public DbConnection(string host, string database, string uid, string password)
        {
            DbConnection._instance = openConection(host, database, uid, password);
        }

        private MySqlConnection openConection(string host, string database, string uid, string password)
        {
            MySqlConnection connection =
                new MySqlConnection("server=" + host + ";database=" + database + ";uid=" + uid + ";pwd=" + password +
                                    ";");
//                new MySqlConnection("server=" + host + ";database=" + database + ";uid=" + uid + ";pwd=" + password +
//                                    ";");
            connection.Open();
            return connection;
        }

        public static void closeConnection()
        {
            _instance.Clone();
        }
    }
}