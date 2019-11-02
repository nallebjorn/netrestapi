using Newtonsoft.Json;
using RestDemo.Models;
using System;
using System.Collections.Generic;
using System.Web.Http;

namespace RestDemo.Controllers
{
    public class ValuesController : ApiController
    {
        // GET api/values
        public IEnumerable<string> Get()
        {
            return new string[] {"value1", "value2"};
        }

        // GET api/values/5
        public string Get(int id)
        {
            return "value";
        }

        // POST api/values
        public User Post([FromBody] User value)
        {
//            Console.WriteLine(value.ToString());
//            var json = JsonConvert.SerializeObject(value);
//            Console.WriteLine();
//            var role = new Role();
//            role.id = 22;
//            role.name = "admin";
//            var user = new User();
//            user.id = 1;
//            user.email = "vuhiddigov-2939@yopmail.com";
//            user.password = "myPerfectPassword";
//            user.username = "vuhiddigov";
//            user.role = role;
//            user.phone = "+7(874) 800-1382";
//            return user;
            return null;
        }

        // PUT api/values/5
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/values/5
        public void Delete(int id)
        {
        }
    }
}