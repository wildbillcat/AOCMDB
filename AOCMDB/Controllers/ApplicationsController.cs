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
using DocumentFormat.OpenXml.Packaging;
using System.IO;
using System.Text;


namespace AOCMDB.Controllers
{
    public class ApplicationsController : Controller
    {
        private AOCMDBContext db;

        static Object TemplateGeneration = new Object();

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
            IEnumerable<string> filepaths = System.IO.Directory.EnumerateFiles(Server.MapPath("~/App_Data/ApplicationTemplates"), "*.docx");
            List<string> files = new List<string>();
            foreach (string filepath in filepaths)
            {
                files.Add(filepath.Split('\\').Last());
            }
            ViewData["TemplateNames"] = files;
            return View("Details", application);
        }

        // GET: Applications/GenerateProfile/5
        public ActionResult DownloadApplicationTemplate(int? id, int? version, string TemplateName)
        {
            if (id == null || version == null || TemplateName == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ApplicationNode application = db.Applications.Find((int)id, (int)version);
            string FilePath = HttpContext.Server.MapPath(string.Concat("~/App_Data/ApplicationTemplates/", TemplateName));
            if (application == null || !System.IO.File.Exists(FilePath))
            {
                return HttpNotFound();
            }

            try
            {
                if (!System.IO.Directory.Exists(Server.MapPath("~/App_Data/ApplicationDocx")))
                {
                    System.IO.Directory.CreateDirectory(Server.MapPath("~/App_Data/ApplicationDocx"));
                }
            }
            catch { }

            string NewFile = string.Concat(Server.MapPath("~/App_Data/ApplicationDocx/"), application.ApplicationId, "_", application.DatabaseRevision, "_", application.ApplicationName, "_", TemplateName);
            //Test if File has previously been Generated
            if (!System.IO.File.Exists(NewFile))
            {
                try
                {
                    //Generate the Word File
                    lock (TemplateGeneration)
                    {
                        //Secondary Test to be sure file wasn't generated during first attempt
                        if (!System.IO.File.Exists(NewFile))
                        {
                            System.IO.File.Copy(FilePath, NewFile);

                            using (WordprocessingDocument wordDoc = WordprocessingDocument.Open(NewFile, true))
                            {
                                string documentText;
                                using (StreamReader reader = new StreamReader(wordDoc.MainDocumentPart.GetStream()))
                                {
                                    documentText = reader.ReadToEnd();
                                }

                                documentText = documentText.Replace("ApplicationNameVar", application.ApplicationName);

                                documentText = documentText.Replace("GlobalApplicationIDVar", application.GlobalApplicationID.ToString());

                                documentText = documentText.Replace("SiteURLVar", application.SiteURL);

                                documentText = documentText.Replace("NetworkDiagramOrInventoryVar", application.NetworkDiagramOrInventory);

                                documentText = documentText.Replace("AdministrativeProceduresVar", application.AdministrativeProcedures);

                                documentText = documentText.Replace("ContactInformationVar", application.ContactInformation);

                                documentText = documentText.Replace("ClientConfigurationAndValidationVar", application.ClientConfigurationAndValidation);

                                documentText = documentText.Replace("ServerConfigurationandValidationVar", application.ServerConfigurationandValidation);

                                documentText = documentText.Replace("RecoveryProceduresVar", application.RecoveryProcedures);

                                documentText = documentText.Replace("DatabaseRevisionVar", application.DatabaseRevision.ToString());
                                

                                StringBuilder Dependencies = new StringBuilder();
                               // Dependencies.Append("<dl>");


                                //Start Application Dependencies
                                List<ApplicationNode> Apps = application.GetUpstreamApplicationDependencies().ToList();
                                if (Apps.Count > 0)
                                {
                                    Dependencies.Append("<dt>Upstream Application Dependencies</dt>");
                                    Dependencies.Append(@"<dd><table><tr><th>Application Name</th><th>Global Application ID</th></tr>");
                                    foreach (ApplicationNode UpstreamApp in application.GetUpstreamApplicationDependencies())
                                    {
                                        Dependencies.Append(@"<tr><th>Application Name</th><th>Global Application ID</th></tr>");
                                        Dependencies.Append(UpstreamApp.ApplicationName);
                                        Dependencies.Append("</th><th>");
                                        Dependencies.Append(UpstreamApp.GlobalApplicationID.ToString());
                                        Dependencies.Append("</th></tr>");
                                    }
                                    Dependencies.Append("</table></dd>");
                                }                                
                                //End Application Dependencies






                                //End List
                                //Dependencies.Append("</dl>");

                                documentText = documentText.Replace("DependenciesVar", Dependencies.ToString()); //

                                documentText = documentText.Replace("DatabaseRevisionVar", application.DatabaseRevision.ToString());

                                StringBuilder History = new StringBuilder();
                                documentText = documentText.Replace("DocumentHistoryVar", History.ToString()); //

                                using (StreamWriter writer = new StreamWriter(wordDoc.MainDocumentPart.GetStream(FileMode.Create)))
                                {
                                    writer.Write(documentText);
                                }

                            }
                        }
                    }
                }
                catch (Exception e)
                {
                    System.Console.WriteLine(e.Message);
                }
            }

            if (!System.IO.File.Exists(NewFile))
            {
                return HttpNotFound();
            }
            return File(NewFile, "application/vnd.openxmlformats-officedocument.wordprocessingml.document", string.Concat(application.ApplicationId, "_", application.DatabaseRevision, "_", application.ApplicationName, "_", TemplateName));
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

        public ActionResult EditGet(int? id, int? version)
        {
            return Edit(id, version);
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


