using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using AOCMDB.Models;


namespace AOCMDB.Controllers
{
    public class ApplicationsController : Controller
    {
        private AOCMDBContext db;

        public ApplicationsController() : base()
        {
            db = new AOCMDBContext();
        }

        public ApplicationsController(AOCMDBContext ctx) : base()
        {
            db = ctx;
        }        

        // GET: Applications
        public ActionResult Index()
        {            
            List<Application> LatestApplicationVersions = db.GetLatestApplicationVersions().ToList();

            return View("Index", LatestApplicationVersions);
        }

        // GET: Applications/Details/5
        public ActionResult Details(int? id, int? version)
        {
            if (id == null || version == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Application application = db.Applications.Find((int)id, (int)version);
            if (application == null)
            {
                return HttpNotFound();
            }
            ViewData["UpstreamApplications"] = application.GetUpstreamApplicationDependencies();
            ViewData["DownstreamApplications"] = application.GetDownstreamApplicationDependencies();
            return View("Details", application);
        }

        // GET: Applications/Create
        public ActionResult Create()
        {
            return View("Create");
        }

        // POST: Applications/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "CreatedByUser,ApplicationName,GlobalApplicationID,SiteURL,NetworkDiagramOrInventory,AdministrativeProcedures,ContactInformation,ClientConfigurationAndValidation,ServerConfigurationandValidation,RecoveryProcedures")] Application application)
        {
            application.DatabaseRevision = 1;
            application.CreatedAt = DateTime.Now;

            if (User != null && User.Identity.IsAuthenticated)
            {
                application.CreatedByUser = User.Identity.Name;
            }
            else
            {
#if DEBUG
                application.CreatedByUser = "Unauthenticated user!";
#else
                new HttpStatusCodeResult(HttpStatusCode.Unauthorized);
#endif
            }            
            application.ApplicationId = db.Applications.Max(p => p.ApplicationId) + 1;

            if (ModelState.IsValid)
            {
                db.Applications.Add(application);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View("Create", application);
        }

        // GET: Applications/Edit/5
        public ActionResult Edit(int? id, int? version)
        {
            if (id == null || version == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Application application = db.Applications.Find((int)id, (int)version);
            if (application == null)
            {
                return HttpNotFound();
            }
            return View("Edit", application);
        }

        // POST: Applications/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ApplicationId,DatabaseRevision,CreatedByUser,CreatedAt,ApplicationName,GlobalApplicationID,SiteURL,NetworkDiagramOrInventory,AdministrativeProcedures,ContactInformation,ClientConfigurationAndValidation,ServerConfigurationandValidation,RecoveryProcedures")] Application application)
        {
            Application newAppTest;
            if (ModelState.IsValid)//If valid, try saving. Else return to edit page with validation errors
            {
                if (User != null && User.Identity.IsAuthenticated)
                {
                    application.CreatedByUser = User.Identity.Name;
                }
                else
                {
#if DEBUG
                    application.CreatedByUser = "Unauthenticated user!";
#else
                    new HttpStatusCodeResult(HttpStatusCode.Unauthorized);
#endif
                }
                newAppTest = db.Applications.Find(application.ApplicationId, (application.DatabaseRevision+1));
                if(newAppTest == null)//Newer version of application not found! Generate one and save it.
                {
                    newAppTest = application.GenerateNewRevision();

                    db.Applications.Add(newAppTest);
                    db.SaveChanges();
                    return RedirectToAction("Index");
                }
                else
                {//A newer revision of the application has been submitted! Return the user to the edit page with the latest revision
                    application = newAppTest;
                }
            }
            return View("Edit", application);
        }       

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}


