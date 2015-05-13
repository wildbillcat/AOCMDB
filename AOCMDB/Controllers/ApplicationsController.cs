using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using AOCMDB.Models;
using AOCMDB.Models.Nodes;
using AOCMDB.Models.Relationships;


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
            ViewBag.Title = "Index";
            List<ApplicationNode> LatestApplicationVersions = db.GetLatestApplicationVersions().ToList();

            return View("Index", LatestApplicationVersions);
        }

        // GET: Applications
        public ActionResult Dependenies(int id, int[] selected)
        {
            ViewBag.Title = "Dependenies";
            List<ApplicationNode> LatestApplicationVersions = db.GetLatestApplicationVersions().Where(P=>P.ApplicationId != id).ToList();

            return View("Index", LatestApplicationVersions);
        }

        //POST: Applications Search
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Index(string SearchTerm, string SearchField)
        {
            ViewBag.Title = "Index";
            List<ApplicationNode> Results;
            switch(SearchField){
                case "ApplicationName":
                    Results = db.GetLatestApplicationVersions().Where(P => P.ApplicationName != null && P.ApplicationName.Contains(SearchTerm)).ToList();
                    break;
                case "GlobalApplicationID":
                    int globalID;
                    try{
                        globalID = int.Parse(SearchTerm);
                    }catch{//If invalid ID was given, just return empty list
                        Results = new List<ApplicationNode>();
                        break;
                    }
                    Results = db.GetLatestApplicationVersions().Where(P => P.GlobalApplicationID == globalID).ToList();
                    break;
                case "SiteURL":
                    Results = db.GetLatestApplicationVersions().Where(P => P.SiteURL != null && P.SiteURL.Contains(SearchTerm)).ToList();
                    break;
                case "NetworkDiagramOrInventory":
                    Results = db.GetLatestApplicationVersions().Where(P => P.NetworkDiagramOrInventory != null && P.NetworkDiagramOrInventory.Contains(SearchTerm)).ToList();
                    break;
                case "AdministrativeProcedures":
                    Results = db.GetLatestApplicationVersions().Where(P => P.AdministrativeProcedures != null && P.AdministrativeProcedures.Contains(SearchTerm)).ToList();
                    break;
                case "ContactInformation":
                    Results = db.GetLatestApplicationVersions().Where(P => P.ContactInformation != null && P.ContactInformation.Contains(SearchTerm)).ToList();
                    break;
                case "ClientConfigurationAndValidation":
                    Results = db.GetLatestApplicationVersions().Where(P => P.ClientConfigurationAndValidation != null && P.ClientConfigurationAndValidation.Contains(SearchTerm)).ToList();
                    break;
                case "ServerConfigurationandValidation":
                    Results = db.GetLatestApplicationVersions().Where(P => P.ServerConfigurationandValidation != null && P.ServerConfigurationandValidation.Contains(SearchTerm)).ToList();
                    break;
                case "RecoveryProcedures":
                    Results = db.GetLatestApplicationVersions().Where(P => P.RecoveryProcedures != null && P.RecoveryProcedures.Contains(SearchTerm)).ToList();
                    break;                    
                default: //all
                    Results = db.GetLatestApplicationVersions().Where(P => (P.ApplicationName != null && P.ApplicationName.Contains(SearchTerm)) || (P.SiteURL != null && P.SiteURL.Contains(SearchTerm)) || (P.NetworkDiagramOrInventory != null && P.NetworkDiagramOrInventory.Contains(SearchTerm)) || (P.AdministrativeProcedures != null && P.AdministrativeProcedures.Contains(SearchTerm)) || (P.ContactInformation != null && P.ContactInformation.Contains(SearchTerm)) || (P.ClientConfigurationAndValidation != null && P.ClientConfigurationAndValidation.Contains(SearchTerm)) || (P.ServerConfigurationandValidation != null && P.ServerConfigurationandValidation.Contains(SearchTerm)) || (P.RecoveryProcedures != null && P.RecoveryProcedures.Contains(SearchTerm))).ToList();
                    break;                
            }
            return View("Index", Results);
        }

        // GET: Applications/Details/5
        public ActionResult Details(int? id, int? version)
        {
            if (id == null || version == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ApplicationNode application = db.Applications.Find((int)id, (int)version);
            if (application == null)
            {
                return HttpNotFound();
            }
            return View("Details", application);
        }

        // GET: Applications/GenerateProfile/5
        public ActionResult ApplicationTemplateIndex(int? id, int? version, string TemplateName)
        {
            if (id == null || version == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ApplicationNode application = db.Applications.Find((int)id, (int)version);
            if (application == null)
            {
                return HttpNotFound();
            }

            return View("Details", application);
        }

        // GET: Applications/GenerateProfile/5
        public ActionResult ApplicationTemplateDetails(int? id, int? version, string TemplateName)
        {
            if (id == null || version == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ApplicationNode application = db.Applications.Find((int)id, (int)version);
            if (application == null)
            {
                return HttpNotFound();
            }
            return View("Details", application);
        }

        // GET: Applications/History/5
        public ActionResult History(int? id)
        {
            ViewBag.Title = "History";
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            List<ApplicationNode> LatestApplicationVersions = db.Applications.Where(P=>P.ApplicationId == id).ToList();

            return View("Index", LatestApplicationVersions);
        }



        //POST: History Search
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult History(int? id, string SearchTerm, string SearchField)
        {
            ViewBag.Title = "History";
            List<ApplicationNode> Results;
            switch (SearchField)
            {
                case "ApplicationName":
                    Results = db.Applications.Where(P=>P.ApplicationId == id).Where(P => P.ApplicationName != null && P.ApplicationName.Contains(SearchTerm)).ToList();
                    break;
                case "GlobalApplicationID":
                    int globalID;
                    try
                    {
                        globalID = int.Parse(SearchTerm);
                    }
                    catch
                    {//If invalid ID was given, just return empty list
                        Results = new List<ApplicationNode>();
                        break;
                    }
                    Results = db.Applications.Where(P => P.ApplicationId == id).Where(P => P.GlobalApplicationID == globalID).ToList();
                    break;
                case "SiteURL":
                    Results = db.Applications.Where(P => P.ApplicationId == id).Where(P => P.SiteURL != null && P.SiteURL.Contains(SearchTerm)).ToList();
                    break;
                case "NetworkDiagramOrInventory":
                    Results = db.Applications.Where(P => P.ApplicationId == id).Where(P => P.NetworkDiagramOrInventory != null && P.NetworkDiagramOrInventory.Contains(SearchTerm)).ToList();
                    break;
                case "AdministrativeProcedures":
                    Results = db.Applications.Where(P => P.ApplicationId == id).Where(P => P.AdministrativeProcedures != null && P.AdministrativeProcedures.Contains(SearchTerm)).ToList();
                    break;
                case "ContactInformation":
                    Results = db.Applications.Where(P => P.ApplicationId == id).Where(P => P.ContactInformation != null && P.ContactInformation.Contains(SearchTerm)).ToList();
                    break;
                case "ClientConfigurationAndValidation":
                    Results = db.Applications.Where(P => P.ApplicationId == id).Where(P => P.ClientConfigurationAndValidation != null && P.ClientConfigurationAndValidation.Contains(SearchTerm)).ToList();
                    break;
                case "ServerConfigurationandValidation":
                    Results = db.Applications.Where(P => P.ApplicationId == id).Where(P => P.ServerConfigurationandValidation != null && P.ServerConfigurationandValidation.Contains(SearchTerm)).ToList();
                    break;
                case "RecoveryProcedures":
                    Results = db.Applications.Where(P => P.ApplicationId == id).Where(P => P.RecoveryProcedures != null && P.RecoveryProcedures.Contains(SearchTerm)).ToList();
                    break;
                default: //all
                    Results = db.Applications.Where(P => P.ApplicationId == id).Where(P => (P.ApplicationName != null && P.ApplicationName.Contains(SearchTerm)) || (P.SiteURL != null && P.SiteURL.Contains(SearchTerm)) || (P.NetworkDiagramOrInventory != null && P.NetworkDiagramOrInventory.Contains(SearchTerm)) || (P.AdministrativeProcedures != null && P.AdministrativeProcedures.Contains(SearchTerm)) || (P.ContactInformation != null && P.ContactInformation.Contains(SearchTerm)) || (P.ClientConfigurationAndValidation != null && P.ClientConfigurationAndValidation.Contains(SearchTerm)) || (P.ServerConfigurationandValidation != null && P.ServerConfigurationandValidation.Contains(SearchTerm)) || (P.RecoveryProcedures != null && P.RecoveryProcedures.Contains(SearchTerm))).ToList();
                    break;
            }
            return View("Index", Results);
        }

        // GET: Applications/Create
        public ActionResult Create()
        {
            ViewData["Applications"] = db.GetLatestApplicationVersions().ToList();
            ViewData["Databases"] = db.Databases.ToList();
            ViewData["ExternalLosticalStorages"] = db.ExternalLogicalStorages.ToList();
            ViewData["ServersOrAppliances"] = db.ServerOrAppliances.ToList();
            ViewData["SoftwareOrFrameworks"] = db.SoftwareOrFrameworks.ToList();
            return View("Create");
        }

        // POST: Applications/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "CreatedByUser,ApplicationName,GlobalApplicationID,SiteURL,NetworkDiagramOrInventory,AdministrativeProcedures,ContactInformation,ClientConfigurationAndValidation,ServerConfigurationandValidation,RecoveryProcedures")] ApplicationNode application, FormCollection  Form)
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
                //Now Add Dependencies
                string[] ApplicationDeps= Form.AllKeys.Where(P => P.Contains("ApplicationID:")).ToArray();
                foreach (string Dep in ApplicationDeps)
                {
                    try
                    {
                        int id = int.Parse(Dep.Split(':').Last());
                        if (Form.Get(Dep).Contains("true"))
                        {
                            db.ApplicationToApplicationDependencys.Add(new ApplicationToApplicationDependency() { DownstreamApplicationId = application.ApplicationId, DownstreamDatabaseRevision = application.DatabaseRevision, UpstreamApplicationID = id });
                        }
                    }
                    catch { }
                }
                string[] DatabaseDeps = Form.AllKeys.Where(P => P.Contains("DatabaseID:")).ToArray();
                foreach (string Dep in DatabaseDeps)
                {
                    try
                    {
                        int id = int.Parse(Dep.Split(':').Last());
                        if (Form.Get(Dep).Contains("true"))
                        {
                            db.ApplicationToDatabaseDependencys.Add(new ApplicationToDatabaseDependency() { DownstreamApplicationId = application.ApplicationId, DownstreamDatabaseRevision = application.DatabaseRevision, UpstreamDatabaseNodeID = id });
                        }
                    }
                    catch { }
                }
                string[] ExternalLosticalStorageDeps = Form.AllKeys.Where(P => P.Contains("ExternalLosticalStorageID:")).ToArray();
                foreach (string Dep in ExternalLosticalStorageDeps)
                {
                    try
                    {
                        int id = int.Parse(Dep.Split(':').Last());
                        if (Form.Get(Dep).Contains("true"))
                        {
                            db.ApplicationToExternalLogicalStorageDependencys.Add(new ApplicationToExternalLogicalStorageDependency() { DownstreamApplicationId = application.ApplicationId, DownstreamDatabaseRevision = application.DatabaseRevision, UpstreamExternalLogicalStorageNodeNodeID = id });
                        }
                    }
                    catch { }
                }
                string[] ServerOrApplianceDeps = Form.AllKeys.Where(P => P.Contains("ServerOrApplianceID:")).ToArray();
                foreach (string Dep in ServerOrApplianceDeps)
                {
                    try
                    {
                        int id = int.Parse(Dep.Split(':').Last());
                        if (Form.Get(Dep).Contains("true"))
                        {
                            db.ApplicationToServerDependencys.Add(new ApplicationToServerOrApplianceDependency() { DownstreamApplicationId = application.ApplicationId, DownstreamDatabaseRevision = application.DatabaseRevision, UpstreamServerOrApplianceNodeID = id });
                        }
                    }
                    catch { }
                }
                string[] SoftwareOrFrameworkDeps = Form.AllKeys.Where(P => P.Contains("SoftwareOrFrameworkID:")).ToArray();
                foreach (string Dep in SoftwareOrFrameworkDeps)
                {
                    try
                    {
                        int id = int.Parse(Dep.Split(':').Last());
                        if (Form.Get(Dep).Contains("true"))
                        {
                            db.ApplicationToSoftwareOrFrameworkDependencys.Add(new ApplicationToSoftwareOrFrameworkDependency() { DownstreamApplicationId = application.ApplicationId, DownstreamDatabaseRevision = application.DatabaseRevision, UpstreamApplicationToSoftwareOrFrameworkDependencyID = id });
                        }
                    }
                    catch { }
                }
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewData["Applications"] = db.GetLatestApplicationVersions().ToList();
            ViewData["Databases"] = db.Databases.ToList();
            ViewData["ExternalLosticalStorages"] = db.ExternalLogicalStorages.ToList();
            ViewData["ServersOrAppliances"] = db.ServerOrAppliances.ToList();
            ViewData["SoftwareOrFrameworks"] = db.SoftwareOrFrameworks.ToList();
            return View("Create", application);
        }

        // GET: Applications/Edit/5
        public ActionResult Edit(int? id, int? version)
        {
            if (id == null || version == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ApplicationNode application = db.Applications.Find((int)id, (int)version);
            if (application == null)
            {
                return HttpNotFound();
            }
            ViewData["Applications"] = db.GetLatestApplicationVersions().Where(P=>P.ApplicationId != application.ApplicationId).ToList();
            ViewData["Databases"] = db.Databases.ToList();
            ViewData["ExternalLosticalStorages"] = db.ExternalLogicalStorages.ToList();
            ViewData["ServersOrAppliances"] = db.ServerOrAppliances.ToList();
            ViewData["SoftwareOrFrameworks"] = db.SoftwareOrFrameworks.ToList();
            //Dependencies!
            ViewData["ApplicationsDep"] = db.ApplicationToApplicationDependencys.Where(P => P.DownstreamApplicationId == application.ApplicationId && P.DownstreamDatabaseRevision == application.DatabaseRevision).Select(P=>P.UpstreamApplicationID).ToList();
            ViewData["DatabasesDep"] = db.ApplicationToDatabaseDependencys.Where(P => P.DownstreamApplicationId == application.ApplicationId && P.DownstreamDatabaseRevision == application.DatabaseRevision).Select(P=>P.UpstreamDatabaseNodeID).ToList();
            ViewData["ExternalLosticalStoragesDep"] = db.ApplicationToExternalLogicalStorageDependencys.Where(P => P.DownstreamApplicationId == application.ApplicationId && P.DownstreamDatabaseRevision == application.DatabaseRevision).Select(P=>P.UpstreamExternalLogicalStorageNodeNodeID).ToList();
            ViewData["ServersOrAppliancesDep"] = db.ApplicationToServerDependencys.Where(P => P.DownstreamApplicationId == application.ApplicationId && P.DownstreamDatabaseRevision == application.DatabaseRevision).Select(P=>P.UpstreamServerOrApplianceNodeID).ToList();
            ViewData["SoftwareOrFrameworksDep"] = db.ApplicationToSoftwareOrFrameworkDependencys.Where(P => P.DownstreamApplicationId == application.ApplicationId && P.DownstreamDatabaseRevision == application.DatabaseRevision).Select(P=>P.UpstreamApplicationToSoftwareOrFrameworkDependencyID).ToList();

            return View("Edit", application);
        }

        // POST: Applications/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ApplicationId,DatabaseRevision,CreatedByUser,CreatedAt,ApplicationName,GlobalApplicationID,SiteURL,NetworkDiagramOrInventory,AdministrativeProcedures,ContactInformation,ClientConfigurationAndValidation,ServerConfigurationandValidation,RecoveryProcedures")] ApplicationNode application, FormCollection Form)
        {
            ApplicationNode newAppTest;
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
                    //Now Add Dependencies
                    string[] ApplicationDeps = Form.AllKeys.Where(P => P.Contains("ApplicationID:")).ToArray();
                    foreach (string Dep in ApplicationDeps)
                    {
                        try
                        {
                            int id = int.Parse(Dep.Split(':').Last());
                            if (Form.Get(Dep).Contains("true"))
                            {
                                db.ApplicationToApplicationDependencys.Add(new ApplicationToApplicationDependency() { DownstreamApplicationId = newAppTest.ApplicationId, DownstreamDatabaseRevision = newAppTest.DatabaseRevision, UpstreamApplicationID = id });
                            }
                        }
                        catch { }
                    }
                    string[] DatabaseDeps = Form.AllKeys.Where(P => P.Contains("DatabaseID:")).ToArray();
                    foreach (string Dep in DatabaseDeps)
                    {
                        try
                        {
                            int id = int.Parse(Dep.Split(':').Last());
                            if (Form.Get(Dep).Contains("true"))
                            {
                                db.ApplicationToDatabaseDependencys.Add(new ApplicationToDatabaseDependency() { DownstreamApplicationId = newAppTest.ApplicationId, DownstreamDatabaseRevision = newAppTest.DatabaseRevision, UpstreamDatabaseNodeID = id });
                            }
                        }
                        catch { }
                    }
                    string[] ExternalLosticalStorageDeps = Form.AllKeys.Where(P => P.Contains("ExternalLosticalStorageID:")).ToArray();
                    foreach (string Dep in ExternalLosticalStorageDeps)
                    {
                        try
                        {
                            int id = int.Parse(Dep.Split(':').Last());
                            if (Form.Get(Dep).Contains("true"))
                            {
                                db.ApplicationToExternalLogicalStorageDependencys.Add(new ApplicationToExternalLogicalStorageDependency() { DownstreamApplicationId = newAppTest.ApplicationId, DownstreamDatabaseRevision = newAppTest.DatabaseRevision, UpstreamExternalLogicalStorageNodeNodeID = id });
                            }
                        }
                        catch { }
                    }
                    string[] ServerOrApplianceDeps = Form.AllKeys.Where(P => P.Contains("ServerOrApplianceID:")).ToArray();
                    foreach (string Dep in ServerOrApplianceDeps)
                    {
                        try
                        {
                            int id = int.Parse(Dep.Split(':').Last());
                            if (Form.Get(Dep).Contains("true"))
                            {
                                db.ApplicationToServerDependencys.Add(new ApplicationToServerOrApplianceDependency() { DownstreamApplicationId = newAppTest.ApplicationId, DownstreamDatabaseRevision = newAppTest.DatabaseRevision, UpstreamServerOrApplianceNodeID = id });
                            }
                        }
                        catch { }
                    }
                    string[] SoftwareOrFrameworkDeps = Form.AllKeys.Where(P => P.Contains("SoftwareOrFrameworkID:")).ToArray();
                    foreach (string Dep in SoftwareOrFrameworkDeps)
                    {
                        try
                        {
                            int id = int.Parse(Dep.Split(':').Last());
                            if (Form.Get(Dep).Contains("true"))
                            {
                                db.ApplicationToSoftwareOrFrameworkDependencys.Add(new ApplicationToSoftwareOrFrameworkDependency() { DownstreamApplicationId = newAppTest.ApplicationId, DownstreamDatabaseRevision = newAppTest.DatabaseRevision, UpstreamApplicationToSoftwareOrFrameworkDependencyID = id });
                            }
                        }
                        catch { }
                    }
                    db.SaveChanges();
                    return RedirectToAction("Index");
                }
                else
                {//A newer revision of the application has been submitted! Return the user to the edit page with the latest revision
                    application = newAppTest;
                }
            }
            ViewData["Applications"] = db.GetLatestApplicationVersions().Where(P => P.ApplicationId != application.ApplicationId).ToList();
            ViewData["Databases"] = db.Databases.ToList();
            ViewData["ExternalLosticalStorages"] = db.ExternalLogicalStorages.ToList();
            ViewData["ServersOrAppliances"] = db.ServerOrAppliances.ToList();
            ViewData["SoftwareOrFrameworks"] = db.SoftwareOrFrameworks.ToList();
            //Dependencies!
            ViewData["ApplicationsDep"] = db.ApplicationToApplicationDependencys.Where(P => P.DownstreamApplicationId == application.ApplicationId && P.DownstreamDatabaseRevision == application.DatabaseRevision).ToList();
            ViewData["DatabasesDep"] = db.ApplicationToDatabaseDependencys.Where(P => P.DownstreamApplicationId == application.ApplicationId && P.DownstreamDatabaseRevision == application.DatabaseRevision).ToList();
            ViewData["ExternalLosticalStoragesDep"] = db.ApplicationToExternalLogicalStorageDependencys.Where(P => P.DownstreamApplicationId == application.ApplicationId && P.DownstreamDatabaseRevision == application.DatabaseRevision).ToList();
            ViewData["ServersOrAppliancesDep"] = db.ApplicationToServerDependencys.Where(P => P.DownstreamApplicationId == application.ApplicationId && P.DownstreamDatabaseRevision == application.DatabaseRevision).ToList();
            ViewData["SoftwareOrFrameworksDep"] = db.ApplicationToSoftwareOrFrameworkDependencys.Where(P => P.DownstreamApplicationId == application.ApplicationId && P.DownstreamDatabaseRevision == application.DatabaseRevision).ToList();
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


