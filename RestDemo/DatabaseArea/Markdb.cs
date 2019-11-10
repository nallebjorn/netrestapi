using System;
using System.Collections.Generic;
using MySql.Data.MySqlClient;
using RestDemo.Models;

namespace RestDemo.DatabaseArea
{
    public class Markdb
    {
        public List<CarMark> getMarks()
        {
            var connection = DbConnection.openConection();
            var marks = new List<CarMark>();
            var cmd = new MySqlCommand("SELECT * FROM car_marks", connection);
            var reader = cmd.ExecuteReader();
            while (reader.Read())
            {
                var mark = new CarMark();
                mark.id = Int32.Parse(reader["car_mark_id"].ToString());
                mark.name = reader["car_mark_name"].ToString();
                marks.Add(mark);
            }

            connection.Close();
            return marks;
        }

        public CarMark getMark(int id)
        {
            var connection = DbConnection.openConection();
            var cmd = new MySqlCommand("SELECT * FROM car_marks WHERE car_mark_id = " + id, connection);
            var reader = cmd.ExecuteReader();
            var mark = new CarMark();
            while (reader.Read())
            {
                mark.id = Int32.Parse(reader["car_mark_id"].ToString());
                mark.name = reader["car_mark_name"].ToString();
            }

            connection.Close();
            return mark;
        }
    }
}