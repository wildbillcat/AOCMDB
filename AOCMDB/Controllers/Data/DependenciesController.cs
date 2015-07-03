using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using AOCMDB.Models;
using AOCMDB.Models.Data;

namespace AOCMDB.Controllers.Data
{
    public class DependenciesController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: Dependencies
        public ActionResult Index()
        {
            return View(db.Dependencies.ToList());
        }

        // GET: Dependencies/Details/5
        public ActionResult Details(long? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Dependency dependency = db.Dependencies.Find(id);
            if (dependency == null)
            {
                return HttpNotFound();
            }
            return View(dependency);
        }

        // GET: Dependencies/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Dependencies/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,Name,FriendlyName,Details")] Dependency dependency)
        {
            if (ModelState.IsValid)
            {
                db.Dependencies.Add(dependency);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(dependency);
        }

        // GET: Dependencies/Edit/5
        public ActionResult Edit(long? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Dependency dependency = db.Dependencies.Find(id);
            if (dependency == null)
            {
                return HttpNotFound();
            }
            return View(dependency);
        }

        // POST: Dependencies/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Name,FriendlyName,Details")] Dependency dependency)
        {
            if (ModelState.IsValid)
            {
                db.Entry(dependency).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(dependency);
        }

        // GET: Dependencies/Delete/5
        public ActionResult Delete(long? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Dependency dependency = db.Dependencies.Find(id);
            if (dependency == null)
            {
                return HttpNotFound();
            }
            return View(dependency);
        }

        // POST: Dependencies/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(long id)
        {
            Dependency dependency = db.Dependencies.Find(id);
            db.Dependencies.Remove(dependency);
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
