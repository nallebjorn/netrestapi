using System.Collections.Generic;
using System.Linq;
using NUnit.Framework;
using RestDemo.Controllers;
using RestDemo.Models;

namespace RestDemo.Tests
{
    [TestFixture]
    public class Tests
    {
        //Controller returns list of categories 
        [Test]
        public void CategoriesTest()
        {
            var controller = new CategoriesController();
            var result = controller.Get();
            Assert.IsNotNull(result);
        }


        //Controller returns list of car marks 
        [Test]
        public void CarMarksTest()
        {
            var controller = new MarksController();
            //Returns carMarks array
            var result = controller.Get();
            Assert.IsNotNull(result);
        }

        //Controller returns list of roles
        [Test]
        public void RolesTest()
        {
            var controller = new RolesController();
            //Returns roles array
            var result = controller.Get();
            Assert.IsNotNull(result);
        }

        //Testing controller create, read, update, delete provider features 
        [Test]
        public void ProviderTest()
        {
            var controller = new UsersController();
            //Initialize new provider
            var provider = new Provider();
            provider.username = "ProviderUsername";
            provider.password = "password";
            provider.email = "email@mail.com";
            provider.phone = "+79117366548";
            provider.role = new Role(2, "provider");
            provider.address = "Random Address";
            provider.name = "Random Name";
            //Test adding provider to database
            var createProvider = controller.Post(provider);
            Assert.IsNotNull(createProvider);

            //Test reading provider from database
            var readProvider = controller.Get(createProvider.username);
            Assert.IsNotNull(readProvider);

            //Test updating provider in database
            provider.surname = "Surname";
            var isUpdated = controller.Put(readProvider.username, provider);
            Assert.IsTrue(isUpdated);

            //Test deleting provider from database
            var isDeleted = controller.Delete(readProvider.username);
            Assert.IsTrue(isDeleted);
        }


        //Testing controller create, read, update, delete manager features 
        [Test]
        public void ManagerTest()
        {
            var controller = new UsersController();
            //Initialize new manager
            var manager = new Provider();
            manager.username = "managerUsername";
            manager.password = "password";
            manager.email = "emailer@mail.com";
            manager.phone = "+79117366558";
            manager.role = new Role(1, "manager");

            //Test adding manager to database
            var created = controller.Post(manager);
            Assert.IsNotNull(created);

            //Test reading manager from database
            var readManager = controller.Get(created.username);
            Assert.IsNotNull(readManager);

            //Test updating manager in database
            manager.email = "newEmail@manager.ru";
            var isUpdated = controller.Put(readManager.username, manager);
            Assert.IsTrue(isUpdated);

            //Test deleting manager from database
            var isDeleted = controller.Delete(readManager.username);
            Assert.IsTrue(isDeleted);
        }

        //Testing controller create, read, update, delete admin features 
        [Test]
        public void AdminTest()
        {
            var controller = new UsersController();
            //Initialize new admin
            var admin = new Provider();
            admin.username = "adminUsername";
            admin.password = "password";
            admin.email = "adminer@mail.com";
            admin.phone = "+79117266558";
            admin.role = new Role(3, "admin");
            //Test adding admin to database
            var created = controller.Post(admin);
            Assert.IsNotNull(created);

            //Test reading admin from database
            var readAdmin = controller.Get(created.username);
            Assert.IsNotNull(readAdmin);

            //Test updating admin in database
            admin.phone = "+888888888888";
            var isUpdated = controller.Put(readAdmin.username, admin);
            Assert.IsTrue(isUpdated);

            //Test deleting admin from database
            var isDeleted = controller.Delete(readAdmin.username);
            Assert.IsTrue(isDeleted);
        }

        //Controller returns users list
        [Test]
        public void UsersTest()
        {
            var controller = new UsersController();
            var result = controller.Get();
            Assert.IsNotNull(result);
        }


        //Testing controller create, read, update, delete spare features 
        [Test]
        public void SpareTest()
        {
            var controller = new UsersController();
            //Initialize spare's provider, category, car mark
            var provider = new Provider();
            provider.username = "ProviderUsername";
            provider.password = "password";
            provider.email = "email@mail.com";
            provider.phone = "+79117366548";
            provider.role = new Role(2, "provider");
            provider.address = "Random Address";
            provider.name = "Random Name";
            var createProvider = controller.Post(provider);
            Assert.IsNotNull(createProvider);
            Provider readProvider = (Provider) controller.Get(createProvider.username);
            var categories = new CategoriesController().Get().ToArray();
            var marks = new MarksController().Get().ToArray();

            var sparesController = new SparesController();
            //Initialize new spare
            var spare = new Spare();
            spare.id = "some-unique-id-required";
            spare.category = categories[0];
            spare.carMark = marks[0];
            spare.name = "dummyName";
            spare.description = "dummy description";
            spare.price = "500$";
            spare.vin = "R34TF6";
            spare.provider = readProvider;
            spare.images = new List<Img>().ToArray();
            //Test adding spare to database
            var isAdded = sparesController.Post(spare);
            Assert.IsTrue(isAdded);

            //Test updating spare in database
            spare.category = categories[1];
            var isUpdated = sparesController.Put(spare.id, spare);
            Assert.IsTrue(isUpdated);

            //Test deleting spare from database
            var deleted = sparesController.Delete(spare.id);
            Assert.IsTrue(deleted);

            //Removing spare's provider from database
            var providerDeleted = controller.Delete(readProvider.username);
            Assert.IsTrue(providerDeleted);

            //Test controller returns list of spares 
            var spares = sparesController.Get();
            Assert.IsNotNull(spares);
        }
    }
}