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
            var query =
                "SELECT * FROM users INNER JOIN roles ON users.role_id = roles.role_id WHERE `username` = '" +
                user.username + "';";
            var cmd = DbCommand.create(query);
            var reader = cmd.ExecuteReader();

            var temp = new User();
            temp.username = user.username;
            while (reader.Read())
            {
                if (user.password != reader["user_password"].ToString())
                {
                    temp.message = "Password is incorrect.";
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

            return temp;
        }

        public List<User> getUsers()
        {
            List<User> users = new List<User>();
            var cmd = DbCommand.create(
                "SELECT * FROM users ORDER BY users.create_date DESC");
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

            return users;
        }

        public User getUser(string username)
        {
            var temp = new User();
            var query =
                "SELECT * FROM users WHERE username = \"" + username +
                "\" ORDER BY users.create_date DESC";
            var cmd = DbCommand.create(query);
            var reader = cmd.ExecuteReader();
            while (reader.Read())
            {
                temp.role = new Roledb().getRole(Int32.Parse(reader["role_id"].ToString()));
                if (temp.role.name == "provider")
                {
                    return new Providerdb().getProvider(username);
                }

                temp.username = reader["username"].ToString();
                temp.email = reader["user_email"].ToString();
                temp.phone = reader["user_phone"].ToString();
                temp.password = reader["user_password"].ToString();
            }

            return temp;
        }

        public bool addUser(Provider user)
        {
            var query =
                "INSERT INTO `users` (`username`, `user_password`, `user_phone`,`user_email`, `role_id`, `create_date`, `update_date`) VALUES ('" +
                user.username + "', '" + user.password + "', '" + user.phone + "', '" + user.email + "', " +
                user.role.id + ", current_timestamp(), NULL);";
            var cmd = DbCommand.create(query);
            try
            {
                cmd.ExecuteNonQuery();
            }
            catch
            {
                return false;
            }

            if (user.role.id == 2)
            {
                return new Providerdb().addProvider(user);
            }

            return true;
        }

        public bool deleteUser(string username)
        {
            var query = "DELETE FROM `users` WHERE `users`.`username` = \'" + username + "\'";
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

        public bool updateUser(string username, User user)
        {
            var query =
                "UPDATE `users` SET `username` = '" + user.username + "', `user_password` = '" + user.password +
                "', `user_phone` = '" + user.phone + "', `user_email` = '" + user.email + "', `role_id` = '" +
                user.role.id + "', `update_date` = current_timestamp() WHERE `users`.`username` = '" + username + "'";
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
    }
}