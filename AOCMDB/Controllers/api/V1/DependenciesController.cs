using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Description;
using AOCMDB.Models;
using AOCMDB.Models.Data;

namespace AOCMDB.Controllers.api.V1
{
    public class DependenciesController : ApiController
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: api/Dependencies
        [ResponseType(typeof(List<Dependency>))]
        public async Task<IHttpActionResult> GetDependencies()
        {
            return Ok(await db.Dependencies.ToListAsync());
        }

        // GET: api/Dependencies/5
        [ResponseType(typeof(Dependency))]
        public async Task<IHttpActionResult> GetDependency(long id)
        {
            Dependency dependency = await db.Dependencies.FindAsync(id);
            if (dependency == null)
            {
                return NotFound();
            }

            return Ok(dependency);
        }

        // PUT: api/Dependencies/5
        [ResponseType(typeof(void))]
        public async Task<IHttpActionResult> PutDependency(long upstreamId, long downstreamId)
        {
            try
            {
                Task<Dependency> upstreamDependencyTask = db.Dependencies.FindAsync(upstreamId);
                Task<Dependency> downstreamDependencyTask = db.Dependencies.FindAsync(downstreamId);
                
                await Task.WhenAll(upstreamDependencyTask, downstreamDependencyTask);
                
                Dependency upstreamDependency = await upstreamDependencyTask;
                Dependency downstreamDependency = await downstreamDependencyTask;
                if (upstreamDependency == null || downstreamDependency == null)
                {
                    return NotFound();
                }
                upstreamDependency.DownstreamDependencies.Add(downstreamDependency);
                db.Entry(upstreamDependency).State = EntityState.Modified;
                await db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!DependencyExists(upstreamId) || !DependencyExists(downstreamId))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return StatusCode(HttpStatusCode.NoContent);
        }

        // POST: api/Dependencies
        [ResponseType(typeof(Dependency))]
        public async Task<IHttpActionResult> PostDependency(Dependency dependency)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.Dependencies.Add(dependency);
            await db.SaveChangesAsync();

            return CreatedAtRoute("DefaultApi", new { id = dependency.Id }, dependency);
        }

        // DELETE: api/Dependencies/5
        [ResponseType(typeof(Dependency))]
        public async Task<IHttpActionResult> DeleteDependency(long id)
        {
            Dependency dependency = await db.Dependencies.FindAsync(id);
            if (dependency == null)
            {
                return NotFound();
            }

            db.Dependencies.Remove(dependency);
            await db.SaveChangesAsync();

            return Ok(dependency);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool DependencyExists(long id)
        {
            return db.Dependencies.Count(e => e.Id == id) > 0;
        }
    }
}