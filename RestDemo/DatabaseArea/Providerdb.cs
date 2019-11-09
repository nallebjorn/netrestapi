using System;
using RestDemo.DataBase;
using RestDemo.Models;

namespace RestDemo.DatabaseArea
{
    public class Providerdb
    {
        public Provider getProvider(string username)
        {
            var temp = new Provider();
            var query =
                "SELECT * FROM providers INNER JOIN users ON users.username = providers.username WHERE providers.username = \"" +
                username + "\"";
            var cmd = DbCommand.create(query);
            var reader = cmd.ExecuteReader();
            while (reader.Read())
            {
                temp.role = new Role(Int32.Parse(reader["role_id"].ToString()), "provider");
                temp.id = Int32.Parse(reader["provider_id"].ToString());
                temp.username = reader["username"].ToString();
                temp.email = reader["user_email"].ToString();
                temp.phone = reader["user_phone"].ToString();
                temp.address = reader["provider_address"].ToString();
                temp.name = reader["provider_name"].ToString();
                temp.surname = reader["provider_surname"].ToString();
                temp.password = reader["user_password"].ToString();
            }

            return temp;
        }

        public Boolean addProvider(Provider provider)
        {
            var query =
                "INSERT INTO `providers` (`username`, `provider_name`, `provider_surname`, `provider_address`, `create_date`, `update_date`) VALUES ('" +
                provider.username + "', '" + provider.name + "', '" + provider.surname + "', '" +
                provider.address + "', current_timestamp(), NULL)";
            Console.WriteLine(query);
            var cmd = DbCommand.create(query);
            try
            {
                cmd.ExecuteNonQuery();
            }
            catch
            {
                return false;
            }

            return true;
        }

        public bool updateProvider(string username, Provider value)
        {
            try
            {
                var query = "UPDATE `providers` SET `provider_name` = '" + value.name + "', `provider_surname` = '" +
                            value.surname + "', `provider_address` = '" + value.address +
                            "', `update_date` = current_timestamp() WHERE `providers`.`username` = \"" + username +
                            "\"";
                Console.WriteLine(query);
                var cmd = DbCommand.create(query);
                cmd.ExecuteNonQuery();
                new Userdb().updateUser(username, value);
            }
            catch
            {
                return false;
            }

            return true;
        }
    }
}