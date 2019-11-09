using MySql.Data.MySqlClient;

namespace RestDemo.DatabaseArea
{
    public class DbCommand
    {
        public static MySqlCommand create(string query)
        {
//            DbConnection connection = new DbConnection("MYSQL6001.site4now.net", "db_a4fcaf_golont", "a4fcaf_golont", "qaz1qaz1");
            DbConnection connection = new DbConnection("localhost", "golont", "root", "");
            var command = new MySqlCommand(query, DbConnection._instance);
            return command;
        }
    }
}