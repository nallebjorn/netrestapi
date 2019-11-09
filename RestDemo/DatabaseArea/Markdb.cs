using System;
using System.Collections.Generic;
using RestDemo.Models;

namespace RestDemo.DatabaseArea
{
    public class Markdb
    {
        public List<CarMark> getMarks()
        {
            var marks = new List<CarMark>();
            var cmd = DbCommand.create("SELECT * FROM car_marks");
            var reader = cmd.ExecuteReader();
            while (reader.Read())
            {
                var mark = new CarMark();
                mark.id = Int32.Parse(reader["car_mark_id"].ToString());
                mark.name = reader["car_mark_name"].ToString();
                marks.Add(mark);
            }

            return marks;
        }
    }
}