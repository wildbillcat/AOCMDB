using System;
using System.Data.Entity;
using System.Linq;
using System.Collections.Generic;
using System.Data.Common;
using AOCMDB.Models.Nodes;
using AOCMDB.Models.Relationships;

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
        /// <summary>
        /// Nodes
        /// </summary>
        public DbSet<ApplicationNode> Applications { get; set; }
        public DbSet<DatabaseNode> Databases { get; set; }
        public DbSet<ExternalLogicalStorageNode> ExternalLogicalStorages { get; set; }
        public DbSet<ServerOrApplianceNode> ServerOrAppliances { get; set; }
        public DbSet<SoftwareOrFrameworkNode> SoftwareOrFrameworks { get; set; }

        /// <summary>
        /// Application Relationships
        /// </summary>
        public DbSet<ApplicationToApplicationDependency> ApplicationToApplicationDependencys { get; set; }
        public DbSet<ApplicationToDatabaseDependency> ApplicationToDatabaseDependencys { get; set; }
        public DbSet<ApplicationToServerOrApplianceDependency> ApplicationToServerDependencys { get; set; }
        public DbSet<ApplicationToExternalLogicalStorageDependency> ApplicationToExternalLogicalStorageDependencys { get; set; }
        public DbSet<ApplicationToSoftwareOrFrameworkDependency> ApplicationToSoftwareOrFrameworkDependencys { get; set; }

        /// <summary>
        /// Indirect Relationships
        /// </summary>
        public DbSet<DatabaseToServerOrApplianceDependency> DatabaseToServerOrApplianceDependencys { get; set; }
        public DbSet<ServerOrApplianceToExternalLogicalStorageDependency> ServerOrApplianceToExternalLogicalStorageDependencys { get; set; }
        public DbSet<ExternalLogicalStorageDependencyToServerOrAppliance> ExternalLogicalStorageDependencyToServerOrAppliances { get; set; }


        /// <summary>
        /// Essentially this is a stored procedure that returns a list of all the latest revisions of the Applications
        /// </summary>
        /// <returns></returns>
        public IEnumerable<ApplicationNode> GetLatestApplicationVersions()
        {
            return this.Applications.GroupBy(p => p.ApplicationId)
                        .Select(group => group.Where(x => x.DatabaseRevision == group.Max(y => y.DatabaseRevision)).FirstOrDefault());
        }

        /// <summary>
        /// Essentially this is a stored procedure that returns a list of all the latest revisions of the Upstream Application Dependencies
        /// </summary>
        /// <returns></returns>
        public IEnumerable<ApplicationToApplicationDependency> GetLatestUpstreamApplicationDependencyVersions()
        {
            return this.ApplicationToApplicationDependencys.GroupBy(p => p.DownstreamApplicationId)
                        .Select(group => group.Where(x => x.DownstreamDatabaseRevision == group.Max(y => y.DownstreamDatabaseRevision)).FirstOrDefault());
        }

        /// <summary>
        /// Essentially this is a stored procedure that returns a list of all the latest revisions of the Upstream Application Dependencies
        /// </summary>
        /// <returns></returns>
        public IEnumerable<ApplicationToApplicationDependency> GetLatestUpstreamApplicationDependencyVersions(int ApplicationId)
        {
            return this.ApplicationToApplicationDependencys.GroupBy(p => p.UpstreamApplicationID)
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
            context.Applications.Add(new ApplicationNode() { ApplicationId = 1, DatabaseRevision = 1, CreatedByUser = "wildbillcat", CreatedAt = DateTime.Now, ApplicationName = "ContosoCrossing", GlobalApplicationID = 526 });
            context.Applications.Add(new ApplicationNode() { ApplicationId = 1, DatabaseRevision = 2, CreatedByUser = "testuser1", CreatedAt = DateTime.Now, ApplicationName = "SuperContosoCrossing", GlobalApplicationID = 526 });
            context.Applications.Add(new ApplicationNode() { ApplicationId = 1, DatabaseRevision = 3, CreatedByUser = "wildbillcat", CreatedAt = DateTime.Now, ApplicationName = "ContosoCrossing", GlobalApplicationID = 528 });             
            context.Applications.Add(new ApplicationNode() { ApplicationId = 2, DatabaseRevision = 1, CreatedByUser = "wildbillcat", CreatedAt = DateTime.Now, ApplicationName = "ZiflandiaApp", GlobalApplicationID = 522 });
            context.Applications.Add(new ApplicationNode() { ApplicationId = 3, DatabaseRevision = 1, CreatedByUser = "wildbillcat", CreatedAt = DateTime.Now, ApplicationName = "ClickOnceMadness", GlobalApplicationID = 523 });
            context.Applications.Add(new ApplicationNode() { ApplicationId = 4, DatabaseRevision = 1, CreatedByUser = "wildbillcat", CreatedAt = DateTime.Now, ApplicationName = "FlappyHappy", GlobalApplicationID = 524 });

            context.ApplicationToApplicationDependencys.Add(new ApplicationToApplicationDependency() { DownstreamApplicationId = 1, DownstreamDatabaseRevision = 2, UpstreamApplicationID = 3 });
            context.ApplicationToApplicationDependencys.Add(new ApplicationToApplicationDependency() { DownstreamApplicationId = 2, DownstreamDatabaseRevision = 1, UpstreamApplicationID = 1});
            context.ApplicationToApplicationDependencys.Add(new ApplicationToApplicationDependency() { DownstreamApplicationId = 4, DownstreamDatabaseRevision = 1, UpstreamApplicationID = 1 });
            context.ApplicationToApplicationDependencys.Add(new ApplicationToApplicationDependency() { DownstreamApplicationId = 4, DownstreamDatabaseRevision = 1, UpstreamApplicationID = 3 });
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

