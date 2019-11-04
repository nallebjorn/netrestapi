using System;
using System.Collections.Generic;
using System.Web.Http;
using RestDemo.DataBase;
using RestDemo.Models;

namespace RestDemo.Controllers
{
    public class UserController : ApiController
    {
        // GET api/user
        public IEnumerable<User> Get()
        {
            return new Userdb().getUsers().ToArray();
        }

        // POST api/user
        public User Post([FromBody] User value)
        {
            if (value.role == null)
            {
                var user = new Userdb(value);
                return user.validateUser();
            }

            Console.WriteLine("adding new user " + value.ToString());

            return null;
        }
    }
}