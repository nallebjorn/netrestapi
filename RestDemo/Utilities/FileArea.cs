using System;
using System.Collections.Generic;
using System.IO;
using System.Web;
using RestDemo.Models;

namespace RestDemo.Utilities
{
    public class FileArea
    {
        private void validateDir(string dir)
        {
            bool exists = Directory.Exists(dir);
            if (!exists)
            {
                Directory.CreateDirectory(dir);
            }
        }
        
        public List<string> saveImages(string path, Img[] images)
        {
            try
            {
                var links = new List<string>();
                validateDir(HttpContext.Current.Server.MapPath(path));
                foreach (var img in images)
                {
                    string p = path + "/" + img.id + getExtension(img.name);
                    Console.WriteLine(p);
                    links.Add(p);
                    File.WriteAllBytes(HttpContext.Current.Server.MapPath(p), Convert.FromBase64String(img.content));
                }

                return links;
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                return null;
            }
        }
        
        private string getExtension(string input)
        {
            return Path.GetExtension(input);
        }
    }
}