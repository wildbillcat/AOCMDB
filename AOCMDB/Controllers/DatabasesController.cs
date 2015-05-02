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
    public class DatabasesController : Controller
    {
        private AOCMDBContext db;

        public DatabasesController() : base()
        {
            db = new AOCMDBContext();
        }

        public DatabasesController(AOCMDBContext ctx) : base()
        {
            db = ctx;
        }

        // GET: Databases
        public ActionResult Index()
        {
            return View(db.Databases.ToList());
        }

        // GET: Databases/Details/5
        public ActionResult Details(long? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            DatabaseNode databaseNode = db.Databases.Find(id);
            if (databaseNode == null)
            {
                return HttpNotFound();
            }
            return View(databaseNode);
        }

        // GET: Databases/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Databases/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "DatabaseId,DatabaseName,FriendlyName,DatabaseType,Details")] DatabaseNode databaseNode)
        {
            if (ModelState.IsValid)
            {
                db.Databases.Add(databaseNode);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(databaseNode);
        }

        // GET: Databases/Edit/5
        public ActionResult Edit(long? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            DatabaseNode databaseNode = db.Databases.Find(id);
            if (databaseNode == null)
            {
                return HttpNotFound();
            }
            return View(databaseNode);
        }

        // POST: Databases/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "DatabaseId,DatabaseName,FriendlyName,DatabaseType,Details")] DatabaseNode databaseNode)
        {
            if (ModelState.IsValid)
            {
                db.Entry(databaseNode).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(databaseNode);
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
