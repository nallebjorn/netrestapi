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
                    temp.email = reader["user_email"].ToString();
                    temp.phone = reader["user_phone"].ToString();
                    temp.role = new Role(Int32.Parse(reader["role_id"].ToString()), reader["role_name"].ToString());
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
            var cmd = DbCommand.create("SELECT * FROM users INNER JOIN roles ON users.role_id = roles.role_id");
            var reader = cmd.ExecuteReader();
            while (reader.Read())
            {
                var user = new User();
                user.username = reader["username"].ToString();
                user.email = reader["user_email"].ToString();
                user.role = new Role(Int32.Parse(reader["role_id"].ToString()), reader["role_name"].ToString());
                user.phone = reader["user_phone"].ToString();
                users.Add(user);
            }

            return users;
        }
    }
}