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
    public class ServerOrAppliancesController : Controller
    {
        private AOCMDBContext db;

        public ServerOrAppliancesController() : base()
        {
            db = new AOCMDBContext();
        }

        public ServerOrAppliancesController(AOCMDBContext ctx) : base()
        {
            db = ctx;
        }

        // GET: ServerOrAppliances
        public ActionResult Index()
        {
            return View(db.ServerOrAppliances.ToList());
        }

        // GET: ServerOrAppliances/Details/5
        public ActionResult Details(long? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ServerOrApplianceNode serverOrApplianceNode = db.ServerOrAppliances.Find(id);
            if (serverOrApplianceNode == null)
            {
                return HttpNotFound();
            }
            return View(serverOrApplianceNode);
        }

        // GET: ServerOrAppliances/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: ServerOrAppliances/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ServerOrApplianceId,DatabaseName,FriendlyName,Details")] ServerOrApplianceNode serverOrApplianceNode)
        {
            if (ModelState.IsValid)
            {
                db.ServerOrAppliances.Add(serverOrApplianceNode);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(serverOrApplianceNode);
        }

        // GET: ServerOrAppliances/Edit/5
        public ActionResult Edit(long? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ServerOrApplianceNode serverOrApplianceNode = db.ServerOrAppliances.Find(id);
            if (serverOrApplianceNode == null)
            {
                return HttpNotFound();
            }
            return View(serverOrApplianceNode);
        }

        // POST: ServerOrAppliances/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ServerOrApplianceId,DatabaseName,FriendlyName,Details")] ServerOrApplianceNode serverOrApplianceNode)
        {
            if (ModelState.IsValid)
            {
                db.Entry(serverOrApplianceNode).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(serverOrApplianceNode);
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
