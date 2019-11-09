using System.Collections.Generic;
using System.Web.Http;
using RestDemo.DatabaseArea;
using RestDemo.Models;

namespace RestDemo.Controllers
{
    public class CategoriesController : ApiController
    {
        // GET api/categories
        public IEnumerable<Category> Get()
        {
            return new Categorydb().getCategories().ToArray();
        }
    }
}