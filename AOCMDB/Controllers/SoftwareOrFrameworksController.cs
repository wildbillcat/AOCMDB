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

namespace AOCMDB.Controllers
{
    public class SoftwareOrFrameworksController : Controller
    {
        private AOCMDBContext db;

        public SoftwareOrFrameworksController() : base()
        {
            db = new AOCMDBContext();
        }

        public SoftwareOrFrameworksController(AOCMDBContext ctx) : base()
        {
            db = ctx;
        }

        // GET: SoftwareOrFrameworks
        public ActionResult Index()
        {
            return View(db.SoftwareOrFrameworks.ToList());
        }

        // GET: SoftwareOrFrameworks/Details/5
        public ActionResult Details(long? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            SoftwareOrFrameworkNode softwareOrFrameworkNode = db.SoftwareOrFrameworks.Find(id);
            if (softwareOrFrameworkNode == null)
            {
                return HttpNotFound();
            }
            return View(softwareOrFrameworkNode);
        }

        // GET: SoftwareOrFrameworks/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: SoftwareOrFrameworks/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "SoftwareOrFrameworkStorageId,SoftwareOrFrameworkName,FriendlyName,Details")] SoftwareOrFrameworkNode softwareOrFrameworkNode)
        {
            if (ModelState.IsValid)
            {
                db.SoftwareOrFrameworks.Add(softwareOrFrameworkNode);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(softwareOrFrameworkNode);
        }

        // GET: SoftwareOrFrameworks/Edit/5
        public ActionResult Edit(long? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            SoftwareOrFrameworkNode softwareOrFrameworkNode = db.SoftwareOrFrameworks.Find(id);
            if (softwareOrFrameworkNode == null)
            {
                return HttpNotFound();
            }
            return View(softwareOrFrameworkNode);
        }

        // POST: SoftwareOrFrameworks/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "SoftwareOrFrameworkStorageId,SoftwareOrFrameworkName,FriendlyName,Details")] SoftwareOrFrameworkNode softwareOrFrameworkNode)
        {
            if (ModelState.IsValid)
            {
                db.Entry(softwareOrFrameworkNode).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(softwareOrFrameworkNode);
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
