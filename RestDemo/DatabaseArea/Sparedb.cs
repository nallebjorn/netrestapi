using System;
using System.Collections.Generic;
using RestDemo.Models;
using RestDemo.Utilities;

namespace RestDemo.DatabaseArea
{
    public class Sparedb
    {
        private Img[] getImages(string id)
        {
            var images = new List<Img>();
            var query = "SELECT * FROM spare_images WHERE spare_id = \"" + id + "\"";
            var cmd = DbCommand.create(query);
            var reader = cmd.ExecuteReader();
            while (reader.Read())
            {
                var temp = new Img();
                temp.id = reader["spare_image_id"].ToString();
                temp.url = reader["image_url"].ToString();
                images.Add(temp);
            }

            return images.ToArray();
        }

        public Spare getSpare(string id)
        {
            var temp = new Spare();
            var query =
                "SELECT * FROM  spares_storage INNER JOIN providers ON spares_storage.provider_id = providers.provider_id WHERE spares_storage.spare_id = \"" +
                id + "\"";
            var cmd = DbCommand.create(query);
            var reader = cmd.ExecuteReader();
            while (reader.Read())
            {
                temp.id = reader["spare_id"].ToString();
                temp.name = reader["spare_name"].ToString();
                temp.description = reader["spare_description"].ToString();
                temp.price = reader["spare_price"].ToString();
                temp.vin = reader["spare_vin"].ToString();
                temp.images = getImages(id);
                Console.WriteLine(reader["category_id"].ToString());
                
                temp.carMark = new CarMark();
                temp.carMark.id  = Int32.Parse(reader["car_mark_id"].ToString());
                temp.provider = new Provider();
                temp.provider.id = Int32.Parse(reader["provider_id"].ToString());
                temp.category = new Category();
                temp.category.id = Int32.Parse(reader["category_id"].ToString());
            }

            return temp;
        }

        public bool addSpare(Spare spare)
        {
            var query =
                "INSERT INTO `spares_storage` (`spare_id`, `category_id`, `car_mark_id`, `provider_id`,`spare_name`, `spare_description`, `spare_price`, `spare_vin`, `create_date`) VALUES ('" +
                spare.id + "', '" + spare.category.id + "', '" + spare.carMark.id + "', '" + spare.provider.id +
                "', '" + spare.name +
                "', '" + spare.description + "', '" + spare.price + "', '" + spare.vin + "', current_timestamp())";
            var cmd = DbCommand.create(query);
            try
            {
                cmd.ExecuteNonQuery();
            }
            catch
            {
                return false;
            }


            return addImages(spare);
        }

        private bool addImages(Spare spare)
        {
            string providerPath = "/Images/" + spare.provider.id;
            var imagePaths = new FileArea().saveImages(providerPath, spare.images);
            try
            {
                foreach (var path in imagePaths)
                {
                    var query = "INSERT INTO `spare_images` (`spare_id`, `image_url`) VALUES ('" + spare.id +
                                "', '" + path + "')";
                    var cmd = DbCommand.create(query);
                    cmd.ExecuteNonQuery();
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                return false;
            }

            return true;
        }
    }
}