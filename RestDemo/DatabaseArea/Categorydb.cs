﻿using System;
using System.Collections.Generic;
using RestDemo.Models;

namespace RestDemo.DatabaseArea
{
    public class Categorydb
    {
        public List<Category> getCategories()
        {
            var categories = new List<Category>();
            var cmd = DbCommand.create("SELECT * FROM categories");
            var reader = cmd.ExecuteReader();
            while (reader.Read())
            {
                var category = new Category();
                category.id = Int32.Parse(reader["category_id"].ToString());
                category.name = reader["category_name"].ToString();
                categories.Add(category);
            }

            return categories;
        }

        public Category getCategory(int id)
        {
            var cmd = DbCommand.create("SELECT * FROM categories WHERE category_id = " + id);
            var reader = cmd.ExecuteReader();
            var category = new Category();
            while (reader.Read())
            {
                category.id = Int32.Parse(reader["category_id"].ToString());
                category.name = reader["category_name"].ToString();
            }

            return category;
        }
    }
}