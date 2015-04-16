using System;
using System.Web.Mvc;
using AOCMDB.Controllers;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace AOCMDB.UnitTests
{
    [TestClass]
    public class ApplicationsControllerTest
    {
        [TestMethod]
        public void IndexActionReturnsIndexView()
        {
            string expected = "Index";
            ApplicationsController controller = new ApplicationsController();

            var result = controller.Index() as ViewResult;

            Assert.AreEqual(expected, result.ViewName);
        }

        [TestMethod]
        public void DetailsActionReturnsDetailsViewValidIDAndVersion()
        {
            throw new NotImplementedException();
            string expected = "Details";
            ApplicationsController controller = new ApplicationsController();

            var result = controller.Create() as ViewResult;

            Assert.AreEqual(expected, result.ViewName);
        }

        [TestMethod]
        public void DetailsActionReturnsDetailsViewInValidIDOrVersion()
        {
            throw new NotImplementedException();
            string expected = "Details";
            ApplicationsController controller = new ApplicationsController();

            var result = controller.Create() as ViewResult;

            Assert.AreEqual(expected, result.ViewName);
        }

        [TestMethod]
        public void DetailsActionReturnsDetailsViewInValidIDOrVersion()
        {
            throw new NotImplementedException();
            string expected = "Details";
            ApplicationsController controller = new ApplicationsController();

            var result = controller.Create() as ViewResult;

            Assert.AreEqual(expected, result.ViewName);
        }

        [TestMethod]
        public void CreateActionReturnsCreateViewGET()
        {
            throw new NotImplementedException();
            string expected = "Create";
            ApplicationsController controller = new ApplicationsController();

            var result = controller.Create() as ViewResult;

            Assert.AreEqual(expected, result.ViewName);
        }

        [TestMethod]
        public void CreateActionReturnsCreateViewPOST()
        {
            throw new NotImplementedException();
            string expected = "Create";
            ApplicationsController controller = new ApplicationsController();

            var result = controller.Create() as ViewResult;

            Assert.AreEqual(expected, result.ViewName);
        }

        [TestMethod]
        public void CreateActionReturnsCreateViewPOST()
        {
            throw new NotImplementedException();
            string expected = "Create";
            ApplicationsController controller = new ApplicationsController();

            var result = controller.Create() as ViewResult;

            Assert.AreEqual(expected, result.ViewName);
        }

        [TestMethod]
        public void ContactActionReturnsContactView()
        {
            throw new NotImplementedException();
            string expected = "Details";
            ApplicationsController controller = new ApplicationsController();

            var result = controller.Details() as ViewResult;

            Assert.AreEqual(expected, result.ViewName);
        }

        [TestMethod]
        public void ContactActionReturnsHttpNotFoundView()
        {
            throw new NotImplementedException();
            string expected = "Details";
            ApplicationsController controller = new ApplicationsController();

            var result = controller.Details() as ViewResult;

            Assert.AreEqual(expected, result.ViewName);
        }

        [TestMethod]
        public void ContactActionReturnsContactView()
        {
            throw new NotImplementedException();
            string expected = "Edit";
            ApplicationsController controller = new ApplicationsController();

            var result = controller.Edit() as ViewResult;

            Assert.AreEqual(expected, result.ViewName);
        }
    }
}
