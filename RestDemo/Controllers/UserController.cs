using System.Web.Http;
using RestDemo.DataBase;
using RestDemo.Models;

namespace RestDemo.Controllers
{
    public class UserController : ApiController
    {
        // POST api/user
        public User Post([FromBody] User value)
        {
            var user = new Userdb(value);
            return user.validateUser();
        }
    }
}