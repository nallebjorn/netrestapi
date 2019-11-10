using System;
using System.Collections.Generic;
using MySql.Data.MySqlClient;
using Newtonsoft.Json;
using RestDemo.DatabaseArea;
using RestDemo.Models;

namespace RestDemo.DataBase
{
    public class Userdb
    {
        private User user;

        public Userdb(User user)
        {
            this.user = user;
        }

        public Userdb()
        {
        }

        public User validateUser()
        {
            var connection = DbConnection.openConection();
            var query =
                "SELECT * FROM users INNER JOIN roles ON users.role_id = roles.role_id WHERE `username` = '" +
                user.username + "';";
            var cmd = new MySqlCommand(query, connection);
            var reader = cmd.ExecuteReader();

            var temp = new User();
            temp.username = user.username;
            while (reader.Read())
            {
                if (user.password != reader["user_password"].ToString())
                {
                    temp.message = "Password is incorrect.";
                    connection.Close();
                    return temp;
                }

                if (user.username != null)
                {
                    temp = new Userdb().getUser(temp.username);
                    temp.message = "Welcome to website, " + user.username + ".";
                }
            }

            if (temp.password == null && temp.message == null)
            {
                temp.message = "Login is incorrect.";
            }

            connection.Close();
            return temp;
        }

        public List<User> getUsers()
        {
            var connection = DbConnection.openConection();
            List<User> users = new List<User>();
            var cmd = new MySqlCommand(
                "SELECT * FROM users ORDER BY users.create_date DESC", connection);
            var reader = cmd.ExecuteReader();
            while (reader.Read())
            {
                var user = new User();
                user.role = new Roledb().getRole(Int32.Parse(reader["role_id"].ToString()));
                user.username = reader["username"].ToString();
                user.email = reader["user_email"].ToString();
                user.phone = reader["user_phone"].ToString();
                users.Add(user);
            }

            connection.Close();
            return users;
        }

        public User getUser(string username)
        {
            var connection = DbConnection.openConection();
            var temp = new User();
            var query =
                "SELECT * FROM users WHERE username = \"" + username +
                "\" ORDER BY users.create_date DESC";
            var cmd = new MySqlCommand(query, connection);
            var reader = cmd.ExecuteReader();
            while (reader.Read())
            {
                temp.role = new Roledb().getRole(Int32.Parse(reader["role_id"].ToString()));
                if (temp.role.name == "provider")
                {
                    connection.Close();
                    return new Providerdb().getProvider(username);
                }

                temp.username = reader["username"].ToString();
                temp.email = reader["user_email"].ToString();
                temp.phone = reader["user_phone"].ToString();
                temp.password = reader["user_password"].ToString();
            }

            connection.Close();
            return temp;
        }

        public bool addUser(Provider user)
        {
            var connection = DbConnection.openConection();
            var query =
                "INSERT INTO `users` (`username`, `user_password`, `user_phone`,`user_email`, `role_id`, `create_date`, `update_date`) VALUES ('" +
                user.username + "', '" + user.password + "', '" + user.phone + "', '" + user.email + "', " +
                user.role.id + ", current_timestamp(), NULL);";
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

            if (user.role.id == 2)
            {
                return new Providerdb().addProvider(user);
            }

            return true;
        }

        public bool deleteUser(string username)
        {
            var connection = DbConnection.openConection();
            var query = "DELETE FROM `users` WHERE `users`.`username` = \'" + username + "\'";
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

        public bool updateUser(string username, User user)
        {
            var connection = DbConnection.openConection();
            var query =
                "UPDATE `users` SET `username` = '" + user.username + "', `user_password` = '" + user.password +
                "', `user_phone` = '" + user.phone + "', `user_email` = '" + user.email + "', `role_id` = '" +
                user.role.id + "', `update_date` = current_timestamp() WHERE `users`.`username` = '" + username + "'";
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
    }
}