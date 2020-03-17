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
            return new Sparedb().getSpares().ToArray();
        }

        // GET api/spares/providerid
        // GET api/spares?providerid=somestring
        public IEnumerable<Spare> Get(int providerId)
        {
            return new Sparedb().getSpares(providerId);
        }

        // GET api/spares/id
        // GET api/spares/someidThisCanBeString
        public Spare Get(string id)
        {
            return new Sparedb().getSpare(id);
        }

        // POST api/spares
        public bool Post([FromBody] Spare value)
        {
            return new Sparedb().addSpare(value);
        }

        public bool Delete(string id)
        {
            return new Sparedb().deleteSpare(id);
        }

        public bool Put(string id, [FromBody] Spare value)
        {
            return true;
        }
    }
}