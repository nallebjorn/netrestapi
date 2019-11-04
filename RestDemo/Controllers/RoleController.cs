using System.Collections.Generic;
using System.Web.Http;
using RestDemo.DatabaseArea;
using RestDemo.Models;

namespace RestDemo.Controllers
{
    public class RoleController : ApiController
    {
        // GET api/role
        public IEnumerable<Role> Get()
        {
            return new Roledb().getRoles().ToArray();
        }
    }
}