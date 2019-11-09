using System.Collections.Generic;
using System.Web.Http;
using RestDemo.DatabaseArea;
using RestDemo.Models;

namespace RestDemo.Controllers
{
    public class MarksController : ApiController
    {
        // GET api/marks
        public IEnumerable<CarMark> Get()
        {
            return new Markdb().getMarks().ToArray();
        }
    }
}