using System;
using System.Web.Http;
using RestDemo.DataBase;
using RestDemo.DatabaseArea;
using RestDemo.Models;

namespace RestDemo.Controllers
{
    public class ProviderController : ApiController
    {
        // POST api/provider
        public Provider Post([FromBody] Provider value)
        {
            if (new Providerdb().addProvider(value))
            {
                return value;
            }

            return null;
        }
    }
}