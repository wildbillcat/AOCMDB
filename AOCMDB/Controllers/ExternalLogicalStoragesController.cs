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
    public class ExternalLogicalStoragesController : Controller
    {
        private AOCMDBContext db;

        public ExternalLogicalStoragesController() : base()
        {
            db = new AOCMDBContext();
        }

        public ExternalLogicalStoragesController(AOCMDBContext ctx) : base()
        {
            db = ctx;
        }

        // GET: ExternalLogicalStorages
        public ActionResult Index()
        {
            return View(db.ExternalLogicalStorages.ToList());
        }

        // GET: ExternalLogicalStorages/Details/5
        public ActionResult Details(long? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ExternalLogicalStorageNode externalLogicalStorageNode = db.ExternalLogicalStorages.Find(id);
            if (externalLogicalStorageNode == null)
            {
                return HttpNotFound();
            }
            return View(externalLogicalStorageNode);
        }

        // GET: ExternalLogicalStorages/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: ExternalLogicalStorages/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ExternalLogicalStorageId,ExternalLogicalStorageName,FriendlyName,Details")] ExternalLogicalStorageNode externalLogicalStorageNode)
        {
            if (ModelState.IsValid)
            {
                db.ExternalLogicalStorages.Add(externalLogicalStorageNode);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(externalLogicalStorageNode);
        }

        // GET: ExternalLogicalStorages/Edit/5
        public ActionResult Edit(long? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ExternalLogicalStorageNode externalLogicalStorageNode = db.ExternalLogicalStorages.Find(id);
            if (externalLogicalStorageNode == null)
            {
                return HttpNotFound();
            }
            return View(externalLogicalStorageNode);
        }

        // POST: ExternalLogicalStorages/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ExternalLogicalStorageId,ExternalLogicalStorageName,FriendlyName,Details")] ExternalLogicalStorageNode externalLogicalStorageNode)
        {
            if (ModelState.IsValid)
            {
                db.Entry(externalLogicalStorageNode).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(externalLogicalStorageNode);
        }

        // GET: ExternalLogicalStorages/Delete/5
        public ActionResult Delete(long? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ExternalLogicalStorageNode externalLogicalStorageNode = db.ExternalLogicalStorages.Find(id);
            if (externalLogicalStorageNode == null)
            {
                return HttpNotFound();
            }
            return View(externalLogicalStorageNode);
        }

        // POST: ExternalLogicalStorages/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(long id)
        {
            ExternalLogicalStorageNode externalLogicalStorageNode = db.ExternalLogicalStorages.Find(id);
            db.ExternalLogicalStorages.Remove(externalLogicalStorageNode);
            db.SaveChanges();
            return RedirectToAction("Index");
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
