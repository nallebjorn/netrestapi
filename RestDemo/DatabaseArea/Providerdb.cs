﻿using System;
using MySql.Data.MySqlClient;
using RestDemo.DataBase;
using RestDemo.Models;

namespace RestDemo.DatabaseArea
{
    public class Providerdb
    {
        public Provider getProvider(string username)
        {
            var connection = DbConnection.openConection();
            var temp = new Provider();
            var query =
                "SELECT * FROM providers INNER JOIN users ON users.username = providers.username WHERE providers.username = \"" +
                username + "\"";
            var cmd = new MySqlCommand(query, connection);
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

            connection.Close();
            return temp;
        }

        public Provider getProvider(int id)
        {
            var connection = DbConnection.openConection();
            var temp = new Provider();
            var query =
                "SELECT * FROM providers INNER JOIN users ON users.username = providers.username WHERE providers.provider_id = " +
                id;
            var cmd = new MySqlCommand(query, connection);
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

            connection.Close();
            return temp;
        }

        public Boolean addProvider(Provider provider)
        {
            var connection = DbConnection.openConection();
            var query =
                "INSERT INTO `providers` (`username`, `provider_name`, `provider_surname`, `provider_address`, `create_date`, `update_date`) VALUES ('" +
                provider.username + "', '" + provider.name + "', '" + provider.surname + "', '" +
                provider.address + "', current_timestamp(), NULL)";
            var cmd = new MySqlCommand(query, connection);
            try
            {
                cmd.ExecuteNonQuery();
            }
            catch
            {
                return false;
            }
            finally
            {
                connection.Close();
            }

            return true;
        }

        public bool updateProvider(string username, Provider value)
        {
            var connection = DbConnection.openConection();
            try
            {
                var query = "UPDATE `providers` SET `provider_name` = '" + value.name + "', `provider_surname` = '" +
                            value.surname + "', `provider_address` = '" + value.address +
                            "', `update_date` = current_timestamp() WHERE `providers`.`username` = \"" + username +
                            "\"";
                var cmd = new MySqlCommand(query, connection);
                cmd.ExecuteNonQuery();
                new Userdb().updateUser(username, value);
            }
            catch
            {
                return false;
            }
            finally
            {
                connection.Close();
            }

            return true;
        }
    }
}