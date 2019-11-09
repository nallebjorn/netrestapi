using System;
using System.Collections.Generic;
using RestDemo.Models;

namespace RestDemo.DatabaseArea
{
    public class Roledb
    {
        public List<Role> getRoles()
        {
            var roles = new List<Role>();
            var cmd = DbCommand.create("SELECT * FROM roles");
            var reader = cmd.ExecuteReader();
            while (reader.Read())
            {
                roles.Add(new Role(Int32.Parse(reader["role_id"].ToString()), reader["role_name"].ToString()));
            }

            return roles;
        }

        public Role getRole(int id)
        {
            var cmd = DbCommand.create("SELECT * FROM roles WHERE role_id = "  +id);
            var reader = cmd.ExecuteReader();
            while (reader.Read())
            {
                return new Role(Int32.Parse(reader["role_id"].ToString()), reader["role_name"].ToString());
            }

            return null;
        }
    }
}