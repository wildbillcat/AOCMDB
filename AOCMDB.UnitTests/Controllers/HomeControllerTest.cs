using System.Web.Mvc;
using AOCMDB.Controllers;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace AOCMDB.UnitTests
{
    [TestClass]
    public class HomeControllerControllerTest
    {
        [TestMethod]
        public void IndexActionReturnsIndexView()
        {
            string expected = "Index";
            HomeController controller = new HomeController();

            var result = controller.Index() as ViewResult;

            Assert.AreEqual(expected, result.ViewName);
        }

        [TestMethod]
        public void AboutActionReturnsAboutView()
        {
            string expected = "About";
            HomeController controller = new HomeController();

            var result = controller.About() as ViewResult;

            Assert.AreEqual(expected, result.ViewName);
        }

        [TestMethod]
        public void ContactActionReturnsContactView()
        {
            string expected = "Contact";
            HomeController controller = new HomeController();

            var result = controller.Contact() as ViewResult;

            Assert.AreEqual(expected, result.ViewName);
        }
    }
}
