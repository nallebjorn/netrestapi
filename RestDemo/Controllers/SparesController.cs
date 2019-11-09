using System;
using System.Drawing;
using System.IO;
using System.Web;
using System.Web.Http;
using RestDemo.Models;

namespace RestDemo.Controllers
{
    public class SparesController : ApiController
    {
        public bool Post([FromBody] Spare value)
        {
            string providerPath = HttpContext.Current.Server.MapPath("/" + value.provider.id + "/Images/");
            validateDir(providerPath);
            return saveImages(providerPath, value.images);
        }

        private void validateDir(string dir)
        {
            bool exists = Directory.Exists(dir);
            if (!exists)
            {
                Directory.CreateDirectory(dir);
            }
        }

        private bool saveImages(string path, Img[] images)
        {
            try
            {
                foreach (var img in images)
                {
                    string p = path + img.id + getExtension(img.name);
                    Console.WriteLine(p);
                    File.WriteAllBytes(p, Convert.FromBase64String(img.content));
                }

                return true;
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                return false;
            }
        }

        private string getExtension(string input)
        {
            return Path.GetExtension(input);
        }
    }
}