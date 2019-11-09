using System;
using System.Collections.Generic;
using System.Web.Http;
using RestDemo.DatabaseArea;
using RestDemo.Models;
using RestDemo.Utilities;

namespace RestDemo.Controllers
{
    public class SparesController : ApiController
    {
        // GET api/spares
        public IEnumerable<Spare> Get()
        {
            return null;
        }

        // GET api/spares/providerid
        public IEnumerable<Spare> Get(int providerid)
        {
            return null;
        }

        // GET api/spares/id
        public Spare Get(string id)
        {
            Console.WriteLine("heere");
            return new Sparedb().getSpare(id);
        }

        // POST api/spares
        public bool Post([FromBody] Spare value)
        {
            return new Sparedb().addSpare(value);
        }
    }
}