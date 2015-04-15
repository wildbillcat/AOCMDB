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
        private AOCMDBContext db = new AOCMDBContext();

        // GET: Applications
        public ActionResult Index()
        {
            //  var query = (
            //  from contact in context.Contacts
            //  group contact by contact.LastName.Substring(0, 1) into contactGroup
            //  select new { FirstLetter = contactGroup.Key, Names = contactGroup }).
            //    OrderBy(letter => letter.FirstLetter);
            //ToList()
            List<Application> LatestApplicationVersions = db.Applications.GroupBy(p => p.ApplicationId)
                        .Select(group => group.Where(x => x.DatabaseRevision == group.Max(y => y.DatabaseRevision)).FirstOrDefault()).ToList();
            //List<Application> apps = db.Applications
            //    .GroupBy(p => p.ApplicationId)
            //    .Select(g => g.Max()).ToList();
            //Dictionary<int, Application> thing = db.Applications.GroupBy(P => P.ApplicationId).ToDictionary(p => p.Key);
            return View(LatestApplicationVersions);
        }

        // GET: Applications/Details/5
        public ActionResult Details(int? id, int? version)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            //Application application = db.Applications.Find(new int[] { (int)id, db.Applications.Where(P => P.ApplicationId == id).Max(p => p.DatabaseRevision) });
            Application application = db.Applications.Find(new int[] { (int)id, (int)version });
            if (application == null)
            {
                return HttpNotFound();
            }
            return View(application);
        }

        // GET: Applications/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Applications/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ApplicationId,DatabaseRevision,CreatedByUser,CreatedAt,ApplicationName,GlobalApplicationID,SiteURL,NetworkDiagramOrInventory,AdministrativeProcedures,ContactInformation,ClientConfigurationAndValidation,ServerConfigurationandValidation,RecoveryProcedures")] Application application)
        {
            if (ModelState.IsValid)
            {
                db.Applications.Add(application);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(application);
        }

        // GET: Applications/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Application application = db.Applications.Find(id);
            if (application == null)
            {
                return HttpNotFound();
            }
            return View(application);
        }

        // POST: Applications/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ApplicationId,DatabaseRevision,CreatedByUser,CreatedAt,ApplicationName,GlobalApplicationID,SiteURL,NetworkDiagramOrInventory,AdministrativeProcedures,ContactInformation,ClientConfigurationAndValidation,ServerConfigurationandValidation,RecoveryProcedures")] Application application)
        {
            if (ModelState.IsValid)
            {
                throw new NotImplementedException();//Logic to implement version control has to be added.
                db.Entry(application).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(application);
        }

        //Delete should not be available, since everything should be under version control.
        // GET: Applications/Delete/5
        //public ActionResult Delete(int? id)
        //{
        //    if (id == null)
        //    {
        //        return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
        //    }
        //    Application application = db.Applications.Find(id);
        //    if (application == null)
        //    {
        //        return HttpNotFound();
        //    }
        //    return View(application);
        //}

        //// POST: Applications/Delete/5
        //[HttpPost, ActionName("Delete")]
        //[ValidateAntiForgeryToken]
        //public ActionResult DeleteConfirmed(int id)
        //{
        //    Application application = db.Applications.Find(id);
        //    db.Applications.Remove(application);
        //    db.SaveChanges();
        //    return RedirectToAction("Index");
        //}

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
