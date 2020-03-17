
using RestDemo.Controllers;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace RestDemo.Tests
{
    [TestClass]
    public class CategoriesControllerTest
    {
        [TestMethod]
        public void TestCategories()
        {
            var controller = new CategoriesController();
            var result = controller.Get();
            Assert.IsNotNull(result);
        }
    }
}