using System;
using System.Collections.Generic;
using MySql.Data.MySqlClient;
using RestDemo.Models;

namespace RestDemo.DataBase
{
    public class Userdb
    {
        public Userdb()
        {
        }

        public List<Role> getRoles()
        {
            List<Role> roles = new List<Role>();
            using (MySqlConnection connection = new MySqlConnection("server=localhost;database=golont;uid=root;pwd=;"))
            using (var command = new MySqlCommand("SELECT * FROM roles", connection))
            {
                connection.Open();
                using (var reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        Role temp = new Role(Int32.Parse(reader["role_id"].ToString()), reader["role_name"].ToString());
                        roles.Add(temp);
                    }
                }
            }
            return roles;
        }
    }
}