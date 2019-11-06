using System.Collections.Generic;
using System.Web.Http;
using RestDemo.DatabaseArea;
using RestDemo.Models;

namespace RestDemo.Controllers
{
    public class RolesController : ApiController
    {
        // GET api/roles
        public IEnumerable<Role> Get()
        {
            return new Roledb().getRoles().ToArray();
        }
    }
}