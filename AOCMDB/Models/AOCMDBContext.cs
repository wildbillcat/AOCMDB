using System;
using System.Data.Entity;
using System.Linq;
using System.Collections.Generic;
using System.Data.Common;

namespace AOCMDB.Models
{
    public class AOCMDBContext : DbContext
    {

        public AOCMDBContext(): base() 
        {
#if DEBUG
            Database.SetInitializer<AOCMDBContext>(new DefaultTestDataInitializer());//Reinitialize the database after everystartup
#endif
        }

        public AOCMDBContext(DbConnection connection) : base(connection, true)
        {
#if DEBUG
            Database.SetInitializer<AOCMDBContext>(new DefaultTestDataInitializer());//Reinitialize the database after everystartup
#endif
        }        

        public DbSet<Application> Applications { get; set; }
        public DbSet<UpstreamApplicationDependency> UpstreamApplicationDependencys { get; set; }

        /// <summary>
        /// Essentially this is a stored procedure that returns a list of all the latest revisions of the Applications
        /// </summary>
        /// <returns></returns>
        public IEnumerable<Application> GetLatestApplicationVersions()
        {
            return this.Applications.GroupBy(p => p.ApplicationId)
                        .Select(group => group.Where(x => x.DatabaseRevision == group.Max(y => y.DatabaseRevision)).FirstOrDefault());
        }

        /// <summary>
        /// Essentially this is a stored procedure that returns a list of all the latest revisions of the Upstream Application Dependencies
        /// </summary>
        /// <returns></returns>
        public IEnumerable<UpstreamApplicationDependency> GetLatestUpstreamApplicationDependencyVersions()
        {
            return this.UpstreamApplicationDependencys.GroupBy(p => p.DownstreamApplicationId)
                        .Select(group => group.Where(x => x.DownstreamDatabaseRevision == group.Max(y => y.DownstreamDatabaseRevision)).FirstOrDefault());
        }

        /// <summary>
        /// Essentially this is a stored procedure that returns a list of all the latest revisions of the Upstream Application Dependencies
        /// </summary>
        /// <returns></returns>
        public IEnumerable<UpstreamApplicationDependency> GetLatestUpstreamApplicationDependencyVersions(int ApplicationId)
        {
            return this.UpstreamApplicationDependencys.GroupBy(p => p.UpstreamApplicationID)
                        .Select(group => group.Where(x => x.DownstreamDatabaseRevision == group.Max(y => y.DownstreamDatabaseRevision) && x.UpstreamApplicationID == ApplicationId).FirstOrDefault());
        }

    }

   
    /// <summary>
    /// Seed Data
    /// </summary>
    public class DefaultTestDataInitializer : DropCreateDatabaseAlways<AOCMDBContext>
    {
        protected override void Seed(AOCMDBContext context)
        {
            context.Applications.Add(new Application() { ApplicationId = 1, DatabaseRevision = 1, CreatedByUser = "wildbillcat", CreatedAt = DateTime.Now, ApplicationName = "ContosoCrossing", GlobalApplicationID = 526 });
            context.Applications.Add(new Application() { ApplicationId = 1, DatabaseRevision = 2, CreatedByUser = "testuser1", CreatedAt = DateTime.Now, ApplicationName = "SuperContosoCrossing", GlobalApplicationID = 526 });
            context.Applications.Add(new Application() { ApplicationId = 1, DatabaseRevision = 3, CreatedByUser = "wildbillcat", CreatedAt = DateTime.Now, ApplicationName = "ContosoCrossing", GlobalApplicationID = 528 });             
            context.Applications.Add(new Application() { ApplicationId = 2, DatabaseRevision = 1, CreatedByUser = "wildbillcat", CreatedAt = DateTime.Now, ApplicationName = "ZiflandiaApp", GlobalApplicationID = 522 });
            context.Applications.Add(new Application() { ApplicationId = 3, DatabaseRevision = 1, CreatedByUser = "wildbillcat", CreatedAt = DateTime.Now, ApplicationName = "ClickOnceMadness", GlobalApplicationID = 523 });
            context.Applications.Add(new Application() { ApplicationId = 4, DatabaseRevision = 1, CreatedByUser = "wildbillcat", CreatedAt = DateTime.Now, ApplicationName = "FlappyHappy", GlobalApplicationID = 524 });

            context.UpstreamApplicationDependencys.Add(new UpstreamApplicationDependency() { DownstreamApplicationId = 1, DownstreamDatabaseRevision = 2, UpstreamApplicationID = 3 });
            context.UpstreamApplicationDependencys.Add(new UpstreamApplicationDependency() { DownstreamApplicationId = 2, DownstreamDatabaseRevision = 1, UpstreamApplicationID = 1});
            context.UpstreamApplicationDependencys.Add(new UpstreamApplicationDependency() { DownstreamApplicationId = 4, DownstreamDatabaseRevision = 1, UpstreamApplicationID = 1 });
            context.UpstreamApplicationDependencys.Add(new UpstreamApplicationDependency() { DownstreamApplicationId = 4, DownstreamDatabaseRevision = 1, UpstreamApplicationID = 3 });
            base.Seed(context);
        }
    }

    public class DefaultStartDataInitializer : CreateDatabaseIfNotExists<AOCMDBContext>
    {
        protected override void Seed(AOCMDBContext context)
        {
            base.Seed(context);
        }
    }


}

