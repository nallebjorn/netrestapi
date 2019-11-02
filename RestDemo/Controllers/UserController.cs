using System;
using System.Web.Http;
using System.Web.Mvc;
using Newtonsoft.Json;
using RestDemo.DataBase;
using RestDemo.Models;

namespace RestDemo.Controllers
{
    public class UserController : ApiController
    {
        // POST api/user
        public Role[] Post([FromBody] User value)
        {
            Console.WriteLine(value.ToString());
            var json = JsonConvert.SerializeObject(value);
            Console.WriteLine();
            var role = new Role(22, "admin");
            var user = new User();
            user.id = 1;
            user.email = "vuhiddigov-2939@yopmail.com";
            user.password = "myPerfectPassword";
            user.username = "vuhiddigov";
            user.role = role;
            user.phone = "+7(874) 800-1382";
            
            var lol = new Userdb().getRoles();
            return lol.ToArray();
        }
    }
}