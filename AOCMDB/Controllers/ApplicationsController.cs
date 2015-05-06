﻿using System;
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
            List<ApplicationNode> Applications = db.GetLatestApplicationVersions().ToList();
            List<DatabaseNode> Databases = db.Databases.ToList();
            List<ExternalLogicalStorageNode> ExternalLosticalStorages = db.ExternalLogicalStorages.ToList();
            List<ServerOrApplianceNode> ServersOrAppliances = db.ServerOrAppliances.ToList();
            List<SoftwareOrFrameworkNode> SoftwareOrFrameworks = db.SoftwareOrFrameworks.ToList();
            ViewData["Applications"] = Applications;
            ViewData["Databases"] = Databases;
            ViewData["ExternalLosticalStorages"] = ExternalLosticalStorages;
            ViewData["ServersOrAppliances"] = ServersOrAppliances;
            ViewData["SoftwareOrFrameworks"] = SoftwareOrFrameworks;

            return View("Create");
        }

        // POST: Applications/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "CreatedByUser,ApplicationName,GlobalApplicationID,SiteURL,NetworkDiagramOrInventory,AdministrativeProcedures,ContactInformation,ClientConfigurationAndValidation,ServerConfigurationandValidation,RecoveryProcedures")] ApplicationNode application)
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
            ApplicationNode application = db.Applications.Find((int)id, (int)version);
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
        public ActionResult Edit([Bind(Include = "ApplicationId,DatabaseRevision,CreatedByUser,CreatedAt,ApplicationName,GlobalApplicationID,SiteURL,NetworkDiagramOrInventory,AdministrativeProcedures,ContactInformation,ClientConfigurationAndValidation,ServerConfigurationandValidation,RecoveryProcedures")] ApplicationNode application)
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


