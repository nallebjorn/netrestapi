using System;
using System.Collections.Generic;
using MySql.Data.MySqlClient;
using RestDemo.Models;

namespace RestDemo.DatabaseArea
{
    public class Categorydb
    {
        public List<Category> getCategories()
        {
            var connection = DbConnection.openConection();
            var categories = new List<Category>();
            var cmd = new MySqlCommand("SELECT * FROM categories", connection);
            var reader = cmd.ExecuteReader();
            while (reader.Read())
            {
                var category = new Category();
                category.id = Int32.Parse(reader["category_id"].ToString());
                category.name = reader["category_name"].ToString();
                categories.Add(category);
            }

            connection.Close();
            return categories;
        }

        public Category getCategory(int id)
        {
            var connection = DbConnection.openConection();
            var cmd = new MySqlCommand("SELECT * FROM categories WHERE category_id = " + id, connection);
            var reader = cmd.ExecuteReader();
            var category = new Category();
            while (reader.Read())
            {
                category.id = Int32.Parse(reader["category_id"].ToString());
                category.name = reader["category_name"].ToString();
            }

            connection.Close();
            return category;
        }
    }
}