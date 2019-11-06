using System;
using System.Collections.Generic;
using System.Web.Http;
using RestDemo.DataBase;
using RestDemo.DatabaseArea;
using RestDemo.Models;

namespace RestDemo.Controllers
{
    public class UsersController : ApiController
    {
        // GET api/users +
        public IEnumerable<User> Get()
        {
            return new Userdb().getUsers().ToArray();
        }

        // GET api/users/username
        public User Get(string username)
        {
            return new Userdb().getUser(username);
        }

        // POST api/users
        public User Post([FromBody] Provider value)
        {
            //Authenticate user
            if (value.role == null)
            {
                var user = new Userdb(value);
                return user.validateUser();
            }

            if (new Userdb().addUser(value))
            {
                return value;
            }

            return null;
        }


        // PUT api/users/username + UserFromBody
        public bool Put(string username, [FromBody] Provider value)
        {
            if (value.role.id == 2)
            {
                return new Providerdb().updateProvider(username, value);
            }
            
            return new Userdb().updateUser(username, value);
        }

        // Delete api/users/username
        public bool Delete(string username)
        {
            return new Userdb().deleteUser(username);
        }
    }
}