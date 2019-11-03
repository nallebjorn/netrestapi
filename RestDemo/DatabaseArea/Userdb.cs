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

        public User validateUser()
        {
            var query =
                "SELECT * FROM users INNER JOIN roles ON users.role_id = roles.role_id WHERE `username` = '" +
                user.username + "';";
            var command = DbCommand.create(query);
            var reader = command.ExecuteReader();

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

        public List<Role> getRoles()
        {
            MySqlConnection connection = DbConnection._instance;
            List<Role> roles = new List<Role>();
            var command = DbCommand.create("SELECT * FROM roles");
            using (var reader = command.ExecuteReader())
            {
                while (reader.Read())
                {
                    Role temp = new Role(Int32.Parse(reader["role_id"].ToString()), reader["role_name"].ToString());
                    roles.Add(temp);
                }
            }


            return roles;
        }
    }
}