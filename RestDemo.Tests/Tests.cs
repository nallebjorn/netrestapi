using System;
using System.Collections;
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
        [Test]
        public void CategoriesTest()
        {
            var controller = new CategoriesController();
            //Returns categories array
            var result = controller.Get();
            Assert.IsNotNull(result);
        }

        [Test]
        public void CarMarksTest()
        {
            var controller = new MarksController();
            //Returns carMarks array
            var result = controller.Get();
            Assert.IsNotNull(result);
        }

        [Test]
        public void RolesTest()
        {
            var controller = new RolesController();
            //Returns roles array
            var result = controller.Get();
            Assert.IsNotNull(result);
        }

        [Test]
        public void ProviderTest()
        {
            var controller = new UsersController();
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

            var readProvider = controller.Get(createProvider.username);
            Assert.IsNotNull(readProvider);

            provider.surname = "Surname";
            var isUpdated = controller.Put(readProvider.username, provider);
            Assert.IsTrue(isUpdated);

            var isDeleted = controller.Delete(readProvider.username);
            Assert.IsTrue(isDeleted);
        }

        [Test]
        public void ManagerTest()
        {
            var controller = new UsersController();
            var manager = new Provider();
            manager.username = "managerUsername";
            manager.password = "password";
            manager.email = "emailer@mail.com";
            manager.phone = "+79117366558";
            manager.role = new Role(1, "manager");
            var created = controller.Post(manager);
            Assert.IsNotNull(created);

            var readManager = controller.Get(created.username);
            Assert.IsNotNull(readManager);

            manager.email = "newEmail@manager.ru";
            var isUpdated = controller.Put(readManager.username, manager);
            Assert.IsTrue(isUpdated);

            var isDeleted = controller.Delete(readManager.username);
            Assert.IsTrue(isDeleted);
        }

        [Test]
        public void AdminTest()
        {
            var controller = new UsersController();
            var admin = new Provider();
            admin.username = "adminUsername";
            admin.password = "password";
            admin.email = "adminer@mail.com";
            admin.phone = "+79117266558";
            admin.role = new Role(3, "admin");
            var created = controller.Post(admin);
            Assert.IsNotNull(created);

            var readAdmin = controller.Get(created.username);
            Assert.IsNotNull(readAdmin);

            admin.phone = "+888888888888";
            var isUpdated = controller.Put(readAdmin.username, admin);
            Assert.IsTrue(isUpdated);

            var isDeleted = controller.Delete(readAdmin.username);
            Assert.IsTrue(isDeleted);
        }

        [Test]
        public void UsersTest()
        {
            var controller = new UsersController();
            //users list
            var result = controller.Get();
            Assert.IsNotNull(result);
        }

        [Test]
        public void SpareTest()
        {
            var controller = new UsersController();
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

            Provider readProvider = (Provider)controller.Get(createProvider.username);
            
            var categories = new CategoriesController().Get().ToArray();
            var marks = new MarksController().Get().ToArray();
            
            var sparesController = new SparesController();
            var spare = new Spare();
            spare.id = "some-unique-id-required";
            spare.category  = categories[0];
            spare.carMark = marks[0];
            spare.name = "dummyName";
            spare.description = "dummy description";
            spare.price = "500$";
            spare.vin = "R34TF6";
            spare.provider = readProvider;
            spare.images = new List<Img>().ToArray();
            var isAdded = sparesController.Post(spare);
            Assert.IsTrue(isAdded);

            spare.category = categories[1];
            var isUpdated = sparesController.Put(spare.id, spare);
            Assert.IsTrue(isUpdated);
            
            var deleted = sparesController.Delete(spare.id); 
            Assert.IsTrue(deleted);
            
            var providerDeleted = controller.Delete(readProvider.username);
            Assert.IsTrue(providerDeleted);

            var spares = sparesController.Get();
            Assert.IsNotNull(spares);
        }
    }
}