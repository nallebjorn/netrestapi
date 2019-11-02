using System;
using MySql.Data.MySqlClient;

namespace RestDemo.DataBase
{
    public class DBConnection
    {
        private DBConnection()
        {
        }

        private string databaseName = string.Empty;

        public string DatabaseName { get; set; }

        public string Password { get; set; }
        private MySqlConnection connection = null;

        public MySqlConnection Connection { get; }

        private static DBConnection _instance = null;

        public static DBConnection Instance()
        {
            if (_instance == null)
                _instance = new DBConnection();
            return _instance;
        }

        public bool IsConnect()
        {
            if (Connection == null)
            {
                if (String.IsNullOrEmpty(DatabaseName))
                {
                    Console.WriteLine(DatabaseName);
                    return false;
                }
                string connstring =
                    string.Format("Server=localhost; database={0}; UID=rgfdsoot; password=", DatabaseName);
                connection = new MySqlConnection(connstring);
                connection.Open();
            }

            return true;
        }

        public void Close()
        {
            connection.Close();
        }
    }
}