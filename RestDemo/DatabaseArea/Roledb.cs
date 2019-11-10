using System;
using System.Collections.Generic;
using MySql.Data.MySqlClient;
using RestDemo.Models;

namespace RestDemo.DatabaseArea
{
    public class Roledb
    {
        public List<Role> getRoles()
        {
            var connection = DbConnection.openConection();
            var roles = new List<Role>();
            var cmd = new MySqlCommand("SELECT * FROM roles", connection);
            var reader = cmd.ExecuteReader();
            while (reader.Read())
            {
                roles.Add(new Role(Int32.Parse(reader["role_id"].ToString()), reader["role_name"].ToString()));
            }

            connection.Close();
            return roles;
        }

        public Role getRole(int id)
        {
            var connection = DbConnection.openConection();
            var cmd = new MySqlCommand("SELECT * FROM roles WHERE role_id = " + id, connection);
            var reader = cmd.ExecuteReader();
            Role role = null;
            while (reader.Read())
            {
                role = new Role(Int32.Parse(reader["role_id"].ToString()), reader["role_name"].ToString());
            }

            connection.Close();
            return role;
        }
    }
}