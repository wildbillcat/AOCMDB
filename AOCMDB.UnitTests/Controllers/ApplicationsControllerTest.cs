using System;
using System.Web.Mvc;
using AOCMDB.Controllers;
using AOCMDB.Models;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Web.ModelBinding;
using System.Net;
using Moq;

namespace AOCMDB.UnitTests
{
    [TestClass]
    public class ApplicationsControllerTest
    {
        AOCMDBContext context;
        TestableApplicationsController controller;

        [TestInitialize]
        public void SetupTest()
        {
            context = new AOCMDBContext(Effort.DbConnectionFactory.CreateTransient());
            controller = new TestableApplicationsController(context);
        }

        [TestMethod]
        public void IndexActionReturnsIndexView()
        {
            string expected = "Index";
             
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

            var result = controller.Details(1,1) as ViewResult;

            Assert.AreEqual(expected, result.ViewName);
        }

        [TestMethod]
        public void DetailsActionReturnsDetailsViewInValidID()
        {
            var result = controller.Details(13333, 1) as HttpNotFoundResult;

            Assert.IsInstanceOfType(result, typeof(HttpNotFoundResult));
        }

        [TestMethod]
        public void DetailsActionReturnsDetailsViewInValidVersion()
        {
            var result = controller.Details(1, 93485) as HttpNotFoundResult;

            Assert.IsInstanceOfType(result, typeof(HttpNotFoundResult));
        }

        [TestMethod]
        public void DetailsActionReturnsDetailsViewInValidIDAndVersion()
        {
            var result = controller.Details(1342354, 234234) as HttpNotFoundResult;

            Assert.IsInstanceOfType(result, typeof(HttpNotFoundResult));
        }

        [TestMethod]
        public void DetailsActionReturnsDetailsViewNullID()
        {             
            var result = controller.Details(null, 1) as HttpStatusCodeResult;

            Assert.IsInstanceOfType(result, typeof(HttpStatusCodeResult));
        }

        public void DetailsActionReturnsDetailsViewNullVersion()
        {
            var result = controller.Details(1, null) as HttpStatusCodeResult;

            Assert.IsInstanceOfType(result, typeof(HttpStatusCodeResult));
        }

        public void DetailsActionReturnsDetailsViewNullIDAndVersion()
        {
            var result = controller.Details(null, null) as HttpStatusCodeResult;

            Assert.IsInstanceOfType(result, typeof(HttpStatusCodeResult));
        }
        /// <summary>
        /// Create
        /// </summary>
        [TestMethod]
        public void CreateActionReturnsCreateViewGET()
        {
            string expected = "Create";
                     
            var result = controller.Create() as ViewResult;

            Assert.AreEqual(expected, result.ViewName);
        }

        [TestMethod]
        public void CreateActionReturnsCreateViewPOSTValidParams()
        {
            string expected = "Index";

            Application test = new Models.Application()
            {
                ApplicationId = 96,
                DatabaseRevision = 1,
                CreatedByUser = "UnitTest",
                CreatedAt = DateTime.Now,
                ApplicationName = "TestBot 5000",
                GlobalApplicationID = 555
            };

            controller.TestModel(test);

            var result = controller.Create(test) as ViewResult;

            Assert.IsNull(result);
        }

        [TestMethod]
        public void CreateActionReturnsCreateViewPOSTInvalidParams()
        {
            string expected = "Create";

            Application test = new Models.Application()
            {
                ApplicationId = 88,
                DatabaseRevision = 1,
                CreatedByUser = "UnitTest",
                CreatedAt = DateTime.Now,
            };

            controller.TestModel(test);

            var result = controller.Create(test) as ViewResult;

            Assert.AreEqual(expected, result.ViewName);
        }
        /// <summary>
        /// Edit
        /// </summary>
        [TestMethod]
        public void EditActionReturnsEditView()
        {
            string expected = "Edit";             

            var result = controller.Edit(2,1) as ViewResult;

            Assert.AreEqual(expected, result.ViewName);
        }

        [TestMethod]
        public void EditActionReturnsEditViewNullID()
        {      
            var result = controller.Edit(null, 1) as HttpStatusCodeResult;

            Assert.IsInstanceOfType(result, typeof(HttpStatusCodeResult));
        }

        [TestMethod]
        public void EditActionReturnsEditViewNullVersion()
        {
            var result = controller.Edit(1, null) as HttpStatusCodeResult;

            Assert.IsInstanceOfType(result, typeof(HttpStatusCodeResult));
        }

        [TestMethod]
        public void EditActionReturnsEditViewNullIDandVersion()
        {
            var result = controller.Edit(null, null) as HttpStatusCodeResult;

            Assert.IsInstanceOfType(result, typeof(HttpStatusCodeResult));
        }

        [TestMethod]
        public void EditActionReturnsEditViewInvalidID()
        {
            var result = controller.Edit(23123555, 1) as HttpNotFoundResult;

            Assert.IsInstanceOfType(result, typeof(HttpNotFoundResult));
        }

        [TestMethod]
        public void EditActionReturnsEditViewInvalidVersion()
        {
            var result = controller.Edit(1, 23123555) as HttpNotFoundResult;

            Assert.IsInstanceOfType(result, typeof(HttpNotFoundResult));
        }

        [TestMethod]
        public void EditActionReturnsEditViewInvalidIDandVersion()
        {
            var result = controller.Edit(23123555, 23123555) as HttpNotFoundResult;

            Assert.IsInstanceOfType(result, typeof(HttpNotFoundResult));
        }

        [TestMethod]
        public void EditActionReturnsEditViewSuccessfullPost()
        {   
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

            Assert.IsNull(result);
        }

        [TestMethod]
        public void EditActionReturnsEditViewOldRevisionPost()
        {
            string expected = "Edit";
             
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

    public class TestableApplicationsController : ApplicationsController
    {
        public TestableApplicationsController(AOCMDBContext ctx) : base(ctx)
        {
            ControllerContext = (new Mock<ControllerContext>()).Object;
        }
        public void TestModel(Application Model)
        {
            this.TryValidateModel(Model);
        }
        
    }
}
