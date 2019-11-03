using MySql.Data.MySqlClient;

namespace RestDemo.DatabaseArea
{
    public class DbCommand
    {
        public static MySqlCommand create(string query)
        {
            DbConnection connection = new DbConnection("localhost", "golont", "root", "");
            var command = new MySqlCommand(query, DbConnection._instance);
            return command;
        }
    }
}