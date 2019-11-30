using MySql.Data.MySqlClient;

namespace RestDemo.DatabaseArea
{
    public class DbConnection
    {
        private static string host = "localhost";
        private static string database = "golont";
        private static string uid = "root";
        private static string password = "";
//        private static string host = "remotemysql.com";
//        private static string database = "zDTKpKwNi9";
//        private static string uid = "zDTKpKwNi9";
//        private static string password = "sJBbdZ9Xsd";

        public DbConnection(string host, string database, string uid, string password)
        {
            DbConnection.host = host;
            DbConnection.database = database;
            DbConnection.uid = uid;
            DbConnection.password = password;
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