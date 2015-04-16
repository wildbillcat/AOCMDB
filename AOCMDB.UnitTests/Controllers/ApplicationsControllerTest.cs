using System;
using System.Web.Mvc;
using AOCMDB.Controllers;
using AOCMDB.Models;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Net;

namespace AOCMDB.UnitTests
{
    [TestClass]
    public class ApplicationsControllerTest
    {
        [TestInitialize]
        public void SetupTest()
        {
            using (var context = new AOCMDBContext())
            {
                context.Database.CreateIfNotExists();
            }
        }

        [TestMethod]
        public void IndexActionReturnsIndexView()
        {
            string expected = "Index";
            ApplicationsController controller = new ApplicationsController();

            var result = controller.Index() as ViewResult;

            Assert.AreEqual(expected, result.ViewName);
        }

        /// <summary>
        /// Details
        /// </summary>
        [TestMethod]
        public void DetailsActionReturnsDetailsViewValidIDAndVersion()
        {
            string expected = "Details";
            ApplicationsController controller = new ApplicationsController();

            var result = controller.Details(1,1) as ViewResult;

            Assert.AreEqual(expected, result.ViewName);
        }

        [TestMethod]
        public void DetailsActionReturnsDetailsViewInValidID()
        {
            ApplicationsController controller = new ApplicationsController();
            
            var result = controller.Details(13333, 1) as ViewResult;

            Assert.IsInstanceOfType(result, typeof(HttpNotFoundResult));
        }

        [TestMethod]
        public void DetailsActionReturnsDetailsViewInValidVersion()
        {
            ApplicationsController controller = new ApplicationsController();

            var result = controller.Details(1, 93485) as ViewResult;

            Assert.IsInstanceOfType(result, typeof(HttpNotFoundResult));
        }

        [TestMethod]
        public void DetailsActionReturnsDetailsViewInValidIDAndVersion()
        {
            ApplicationsController controller = new ApplicationsController();

            var result = controller.Details(1342354, 234234) as ViewResult;

            Assert.IsInstanceOfType(result, typeof(HttpNotFoundResult));
        }

        [TestMethod]
        public void DetailsActionReturnsDetailsViewNullID()
        {
            ApplicationsController controller = new ApplicationsController();

            var result = controller.Details(null, 1) as ViewResult;

            Assert.IsInstanceOfType(result, typeof(HttpStatusCodeResult));
        }

        public void DetailsActionReturnsDetailsViewNullVersion()
        {
            ApplicationsController controller = new ApplicationsController();

            var result = controller.Details(1, null) as ViewResult;

            Assert.IsInstanceOfType(result, typeof(HttpStatusCodeResult));
        }

        public void DetailsActionReturnsDetailsViewNullIDAndVersion()
        {
            ApplicationsController controller = new ApplicationsController();

            var result = controller.Details(null, null) as ViewResult;

            Assert.IsInstanceOfType(result, typeof(HttpStatusCodeResult));
        }
        /// <summary>
        /// Create
        /// </summary>
        [TestMethod]
        public void CreateActionReturnsCreateViewGET()
        {
            string expected = "Create";
            ApplicationsController controller = new ApplicationsController();

            var result = controller.Create() as ViewResult;

            Assert.AreEqual(expected, result.ViewName);
        }

        [TestMethod]
        public void CreateActionReturnsCreateViewPOSTValidParams()
        {
            string expected = "Index";
            ApplicationsController controller = new ApplicationsController();

            var result = controller.Create(
                new Models.Application()
                {
                    ApplicationId = 96,
                    DatabaseRevision = 1,
                    CreatedByUser = "UnitTest",
                    CreatedAt = DateTime.Now,
                    ApplicationName = "TestBot 5000",
                    GlobalApplicationID = 555
                }
                ) as ViewResult;

            Assert.AreEqual(expected, result.ViewName);
        }

        [TestMethod]
        public void CreateActionReturnsCreateViewPOSTInvalidParams()
        {
            string expected = "Create";
            ApplicationsController controller = new ApplicationsController();

            var result = controller.Create(
                new Models.Application()
                {
                    ApplicationId = 88,
                    DatabaseRevision = 1,
                    CreatedByUser = "UnitTest",
                    CreatedAt = DateTime.Now,
                    ApplicationName = "MerpBot 5000",
                    GlobalApplicationID = 55475
                }
                ) as ViewResult;

            Assert.AreEqual(expected, result.ViewName);
        }
        /// <summary>
        /// Edit
        /// </summary>
        [TestMethod]
        public void EditActionReturnsEditView()
        {
            string expected = "Edit";
            ApplicationsController controller = new ApplicationsController();

            var result = controller.Edit(2,1) as ViewResult;

            Assert.AreEqual(expected, result.ViewName);
        }

        [TestMethod]
        public void EditActionReturnsEditViewNullID()
        {
            ApplicationsController controller = new ApplicationsController();

            var result = controller.Edit(null, 1) as ViewResult;

            Assert.AreEqual(result, typeof(HttpStatusCodeResult));
        }

        [TestMethod]
        public void EditActionReturnsEditViewNullVersion()
        {
            ApplicationsController controller = new ApplicationsController();

            var result = controller.Edit(1, null) as ViewResult;

            Assert.AreEqual(result, typeof(HttpStatusCodeResult));
        }

        [TestMethod]
        public void EditActionReturnsEditViewNullIDandVersion()
        {
            ApplicationsController controller = new ApplicationsController();

            var result = controller.Edit(null, null) as ViewResult;

            Assert.AreEqual(result, typeof(HttpStatusCodeResult));
        }

        [TestMethod]
        public void EditActionReturnsEditViewInvalidID()
        {
            ApplicationsController controller = new ApplicationsController();

            var result = controller.Edit(23123555, 1) as ViewResult;

            Assert.AreEqual(result, typeof(HttpNotFoundResult));
        }

        [TestMethod]
        public void EditActionReturnsEditViewInvalidVersion()
        {
            ApplicationsController controller = new ApplicationsController();

            var result = controller.Edit(1, 23123555) as ViewResult;

            Assert.AreEqual(result, typeof(HttpNotFoundResult));
        }

        [TestMethod]
        public void EditActionReturnsEditViewInvalidIDandVersion()
        {
            ApplicationsController controller = new ApplicationsController();

            var result = controller.Edit(23123555, 23123555) as ViewResult;

            Assert.AreEqual(result, typeof(HttpNotFoundResult));
        }

        [TestMethod]
        public void EditActionReturnsEditViewSuccessfullPost()
        {
            string expected = "Index";
            ApplicationsController controller = new ApplicationsController();

            var result = controller.Edit(
                new Models.Application()
                {
                    ApplicationId = 3,
                    DatabaseRevision = 1,
                    CreatedByUser = "UnitTest",
                    CreatedAt = DateTime.Now,
                    ApplicationName = "AngryClicky",
                    GlobalApplicationID = 555
                }
                ) as ViewResult;

            Assert.AreEqual(expected, result.ViewName);
        }

        [TestMethod]
        public void EditActionReturnsEditViewOldRevisionPost()
        {
            string expected = "Edit";
            ApplicationsController controller = new ApplicationsController();

            var result = controller.Edit(
                new Models.Application()
                {
                    ApplicationId = 1,
                    DatabaseRevision = 1,
                    CreatedByUser = "UnitTest",
                    CreatedAt = DateTime.Now,
                    ApplicationName = "AngryClicky",
                    GlobalApplicationID = 555
                }
                ) as ViewResult;

            Assert.AreEqual(expected, result.ViewName);
        }

        [TestMethod]
        public void EditActionReturnsEditViewOldMissingGlobalAppIDPost()
        {
            string expected = "Edit";
            ApplicationsController controller = new ApplicationsController();

            var result = controller.Edit(
                new Models.Application()
                {
                    ApplicationId = 1,
                    DatabaseRevision = 1,
                    CreatedByUser = "UnitTest",
                    CreatedAt = DateTime.Now,
                    ApplicationName = "AngryClicky"
                }
                ) as ViewResult;

            Assert.AreEqual(expected, result.ViewName);
        }
    }
}
