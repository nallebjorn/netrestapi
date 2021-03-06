﻿using System;
using System.Collections.Generic;
using System.Web;
using MySql.Data.MySqlClient;
using RestDemo.Models;
using RestDemo.Utilities;

namespace RestDemo.DatabaseArea
{
    public class Sparedb
    {
        private Img[] getImages(string id)
        {
            var connection = DbConnection.openConection();
            var images = new List<Img>();
            var query = "SELECT * FROM spare_images WHERE spare_id = \"" + id + "\"";
            var cmd = new MySqlCommand(query, connection);
            var reader = cmd.ExecuteReader();
            while (reader.Read())
            {
                var temp = new Img();
                temp.id = reader["spare_image_id"].ToString();
                temp.url = reader["image_url"].ToString();
                images.Add(temp);
            }

            connection.Close();
            return images.ToArray();
        }

        public List<Spare> getSpares()
        {
            var connection = DbConnection.openConection();
            var spares = new List<Spare>();
            var query = "SELECT * FROM spares_storage";
            var cmd = new MySqlCommand(query, connection);
            var reader = cmd.ExecuteReader();
            while (reader.Read())
            {
                var temp = new Spare();
                temp.id = reader["spare_id"].ToString();
                temp.name = reader["spare_name"].ToString();
                temp.description = reader["spare_description"].ToString();
                temp.price = reader["spare_price"].ToString();
                temp.vin = reader["spare_vin"].ToString();
                temp.images = getImages(temp.id);
                temp.carMark = new Markdb().getMark(Int32.Parse(reader["car_mark_id"].ToString()));
                temp.provider = new Providerdb().getProvider(Int32.Parse(reader["provider_id"].ToString()));
                temp.category = new Categorydb().getCategory(Int32.Parse(reader["category_id"].ToString()));
                spares.Add(temp);
            }

            connection.Close();
            return spares;
        }

        public List<Spare> getSpares(int providerId)
        {
            var connection = DbConnection.openConection();
            var spares = new List<Spare>();
            var query = "SELECT * FROM spares_storage WHERE provider_id = " + providerId;
            var cmd = new MySqlCommand(query, connection);
            var reader = cmd.ExecuteReader();
            while (reader.Read())
            {
                var temp = new Spare();
                temp.id = reader["spare_id"].ToString();
                temp.name = reader["spare_name"].ToString();
                temp.description = reader["spare_description"].ToString();
                temp.price = reader["spare_price"].ToString();
                temp.vin = reader["spare_vin"].ToString();
                temp.images = getImages(temp.id);
                temp.carMark = new Markdb().getMark(Int32.Parse(reader["car_mark_id"].ToString()));
                temp.provider = new Providerdb().getProvider(Int32.Parse(reader["provider_id"].ToString()));
                temp.category = new Categorydb().getCategory(Int32.Parse(reader["category_id"].ToString()));
                spares.Add(temp);
            }

            connection.Close();
            return spares;
        }

        public Spare getSpare(string id)
        {
            var connection = DbConnection.openConection();
            var temp = new Spare();
            var query =
                "SELECT * FROM  spares_storage WHERE spares_storage.spare_id = \"" +
                id + "\"";
            var cmd = new MySqlCommand(query, connection);
            var reader = cmd.ExecuteReader();
            while (reader.Read())
            {
                temp.id = reader["spare_id"].ToString();
                temp.name = reader["spare_name"].ToString();
                temp.description = reader["spare_description"].ToString();
                temp.price = reader["spare_price"].ToString();
                temp.vin = reader["spare_vin"].ToString();
                temp.images = getImages(id);
                temp.carMark = new Markdb().getMark(Int32.Parse(reader["car_mark_id"].ToString()));
                temp.provider = new Providerdb().getProvider(Int32.Parse(reader["provider_id"].ToString()));
                temp.category = new Categorydb().getCategory(Int32.Parse(reader["category_id"].ToString()));
            }

            connection.Close();
            return temp;
        }

        public bool addSpare(Spare spare)
        {
            var connection = DbConnection.openConection();
            var query =
                "INSERT INTO `spares_storage` (`spare_id`, `category_id`, `car_mark_id`, `provider_id`,`spare_name`, `spare_description`, `spare_price`, `spare_vin`, `create_date`) VALUES ('" +
                spare.id + "', '" + spare.category.id + "', '" + spare.carMark.id + "', '" + spare.provider.id +
                "', '" + spare.name +
                "', '" + spare.description + "', '" + spare.price + "', '" + spare.vin + "', current_timestamp())";
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

            return addImages(spare);
        }

        private bool addImages(Spare spare)
        {

            if (spare.images.Length == 0)
            {
                return true;
            }

            string providerPath = "/Images/" + spare.provider.id;
            var connection = DbConnection.openConection();
            var imagePaths = new FileArea().saveImages(providerPath, spare.images);
            try
            {
                    foreach (var path in imagePaths)
                    {
                        var query = "INSERT INTO `spare_images` (`spare_id`, `image_url`) VALUES ('" + spare.id +
                                    "', '" + path + "')";
                        var cmd = new MySqlCommand(query, connection);
                        cmd.ExecuteNonQuery();
                    }
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                return false;
            }
            finally
            {
                connection.Close();
            }

            return true;
        }

        public bool deleteSpare(string id)
        {
            var connection = DbConnection.openConection();
            var query = "DELETE FROM spares_storage WHERE spare_id = \"" + id + "\"";
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

        public bool updateSpare(string id, Spare spare)
        {
//            string query = "UPDATE `spares` SET "
            return false;
        }
    }
}