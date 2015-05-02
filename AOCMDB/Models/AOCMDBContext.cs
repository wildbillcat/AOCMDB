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
            //Initial Applications
            context.Applications.Add(new ApplicationNode() { ApplicationId = 1, DatabaseRevision = 1, CreatedByUser = "wildbillcat", CreatedAt = DateTime.Now, ApplicationName = "ContosoCrossing", GlobalApplicationID = 526 });
            context.Applications.Add(new ApplicationNode() { ApplicationId = 1, DatabaseRevision = 2, CreatedByUser = "testuser1", CreatedAt = DateTime.Now, ApplicationName = "SuperContosoCrossing", GlobalApplicationID = 526 });
            context.Applications.Add(new ApplicationNode() { ApplicationId = 1, DatabaseRevision = 3, CreatedByUser = "wildbillcat", CreatedAt = DateTime.Now, ApplicationName = "ContosoCrossing", GlobalApplicationID = 528 });             
            context.Applications.Add(new ApplicationNode() { ApplicationId = 2, DatabaseRevision = 1, CreatedByUser = "wildbillcat", CreatedAt = DateTime.Now, ApplicationName = "ZiflandiaApp", GlobalApplicationID = 522 });
            context.Applications.Add(new ApplicationNode() { ApplicationId = 3, DatabaseRevision = 1, CreatedByUser = "wildbillcat", CreatedAt = DateTime.Now, ApplicationName = "ClickOnceMadness", GlobalApplicationID = 523 });
            context.Applications.Add(new ApplicationNode() { ApplicationId = 4, DatabaseRevision = 1, CreatedByUser = "wildbillcat", CreatedAt = DateTime.Now, ApplicationName = "FlappyHappy", GlobalApplicationID = 524 });
            //Inter Application Dependencies
            context.ApplicationToApplicationDependencys.Add(new ApplicationToApplicationDependency() { DownstreamApplicationId = 1, DownstreamDatabaseRevision = 2, UpstreamApplicationID = 3 });
            context.ApplicationToApplicationDependencys.Add(new ApplicationToApplicationDependency() { DownstreamApplicationId = 2, DownstreamDatabaseRevision = 1, UpstreamApplicationID = 1});
            context.ApplicationToApplicationDependencys.Add(new ApplicationToApplicationDependency() { DownstreamApplicationId = 4, DownstreamDatabaseRevision = 1, UpstreamApplicationID = 1 });
            context.ApplicationToApplicationDependencys.Add(new ApplicationToApplicationDependency() { DownstreamApplicationId = 4, DownstreamDatabaseRevision = 1, UpstreamApplicationID = 3 });
            //Initial Databases
            context.Databases.Add(new DatabaseNode() { DatabaseId = 1, DatabaseName = "CPC", DatabaseType = "HP Enterprise DB", FriendlyName = "Continuous Product Catalog", Details = "Fancy Stuff" });
            context.Databases.Add(new DatabaseNode() { DatabaseId = 2, DatabaseName = "CRC", DatabaseType = "Oracle", FriendlyName = "Failed Product Catalog", Details = "Fancy Stuff" });
            context.Databases.Add(new DatabaseNode() { DatabaseId = 3, DatabaseName = "IKE", DatabaseType = "RavenDB", FriendlyName = "Overseas Holdings", Details = "Fancy Stuff" });
            context.Databases.Add(new DatabaseNode() { DatabaseId = 4, DatabaseName = "KIKWE", DatabaseType = "HateMeal", FriendlyName = "Those who crossed the developer", Details = "Fancy Stuff" });
            //Application to Database Dependencies
            context.ApplicationToDatabaseDependencys.Add(new ApplicationToDatabaseDependency() { DownstreamApplicationId = 1, DownstreamDatabaseRevision = 2, UpstreamDatabaseNodeID = 4});
            context.ApplicationToDatabaseDependencys.Add(new ApplicationToDatabaseDependency() { DownstreamApplicationId = 1, DownstreamDatabaseRevision = 3, UpstreamDatabaseNodeID = 2 });
            context.ApplicationToDatabaseDependencys.Add(new ApplicationToDatabaseDependency() { DownstreamApplicationId = 4, DownstreamDatabaseRevision = 1, UpstreamDatabaseNodeID = 1 });
            //Initial Logical Storage Nodes
            context.ExternalLogicalStorages.Add(new ExternalLogicalStorageNode() { ExternalLogicalStorageId = 1, ExternalLogicalStorageName = @"\\FileServer\path\of\share$", FriendlyName = "General Storage Server", Details = "This share is used to hold all our important stuff."});
            context.ExternalLogicalStorages.Add(new ExternalLogicalStorageNode() { ExternalLogicalStorageId = 2, ExternalLogicalStorageName = @"ISCSI: Lun 543", FriendlyName = "File Server ISCSI Lun", Details = "This share is used by the file server to hold all our important stuff." });
            context.ExternalLogicalStorages.Add(new ExternalLogicalStorageNode() { ExternalLogicalStorageId = 3, ExternalLogicalStorageName = @"\\NAS353\OfficeDocuments", FriendlyName = "General Storage Server", Details = "This share is used to hold all stuff we forget about." });
            context.ExternalLogicalStorages.Add(new ExternalLogicalStorageNode() { ExternalLogicalStorageId = 4, ExternalLogicalStorageName = @"afp://Orange/DocuMat/Legal", FriendlyName = "The Legal Department's appletalk case share", Details = "This share is used to hold Legal's important stuff." });
            context.ExternalLogicalStorages.Add(new ExternalLogicalStorageNode() { ExternalLogicalStorageId = 5, ExternalLogicalStorageName = @"\\FileServer\path\of\share2$", FriendlyName = "General Storage Server" });
            //Application to External Logical Sources
            context.ApplicationToExternalLogicalStorageDependencys.Add(new ApplicationToExternalLogicalStorageDependency() { DownstreamApplicationId = 1, DownstreamDatabaseRevision = 3, UpstreamExternalLogicalStorageNodeNodeID = 3 });
            context.ApplicationToExternalLogicalStorageDependencys.Add(new ApplicationToExternalLogicalStorageDependency() { DownstreamApplicationId = 2, DownstreamDatabaseRevision = 1, UpstreamExternalLogicalStorageNodeNodeID = 5 });
            context.ApplicationToExternalLogicalStorageDependencys.Add(new ApplicationToExternalLogicalStorageDependency() { DownstreamApplicationId = 3, DownstreamDatabaseRevision = 1, UpstreamExternalLogicalStorageNodeNodeID = 2 });
            context.ApplicationToExternalLogicalStorageDependencys.Add(new ApplicationToExternalLogicalStorageDependency() { DownstreamApplicationId = 2, DownstreamDatabaseRevision = 1, UpstreamExternalLogicalStorageNodeNodeID = 3 });
            context.ApplicationToExternalLogicalStorageDependencys.Add(new ApplicationToExternalLogicalStorageDependency() { DownstreamApplicationId = 4, DownstreamDatabaseRevision = 1, UpstreamExternalLogicalStorageNodeNodeID = 1 });
            //Initial Servers or Network Appliances
            context.ServerOrAppliances.Add(new ServerOrApplianceNode() { ServerOrApplianceId = 1, ServerOrApplianceName = "FileServer", FriendlyName = "Office File Server", Details = "Server 2012R2 VM"});
            context.ServerOrAppliances.Add(new ServerOrApplianceNode() { ServerOrApplianceId = 2, ServerOrApplianceName = "SanFranNas01", FriendlyName = "Sanfrancisco Network Attached Storage" });
            context.ServerOrAppliances.Add(new ServerOrApplianceNode() { ServerOrApplianceId = 3, ServerOrApplianceName = "NAS353", FriendlyName = "Bob's ReadyNas" });
            context.ServerOrAppliances.Add(new ServerOrApplianceNode() { ServerOrApplianceId = 4, ServerOrApplianceName = "Ora3", FriendlyName = "Primary Oracle 12 Server" });
            context.ServerOrAppliances.Add(new ServerOrApplianceNode() { ServerOrApplianceId = 5, ServerOrApplianceName = "RVN3", FriendlyName = "RavenDB" });
            context.ServerOrAppliances.Add(new ServerOrApplianceNode() { ServerOrApplianceId = 6, ServerOrApplianceName = "HPEnt", FriendlyName = "HP Enterprise DB" });
            context.ServerOrAppliances.Add(new ServerOrApplianceNode() { ServerOrApplianceId = 7, ServerOrApplianceName = "H8", FriendlyName = "HateMeal Server" });
            //Application to Servers or Appliance Dependencies
            context.ApplicationToServerDependencys.Add(new ApplicationToServerOrApplianceDependency() { DownstreamApplicationId = 1, DownstreamDatabaseRevision = 3, UpstreamServerOrApplianceNodeID = 4 });
            context.ApplicationToServerDependencys.Add(new ApplicationToServerOrApplianceDependency() { DownstreamApplicationId = 4, DownstreamDatabaseRevision = 1, UpstreamServerOrApplianceNodeID = 3 });
            context.ApplicationToServerDependencys.Add(new ApplicationToServerOrApplianceDependency() { DownstreamApplicationId = 2, DownstreamDatabaseRevision = 1, UpstreamServerOrApplianceNodeID = 2 });
            context.ApplicationToServerDependencys.Add(new ApplicationToServerOrApplianceDependency() { DownstreamApplicationId = 2, DownstreamDatabaseRevision = 1, UpstreamServerOrApplianceNodeID = 4 });
            //Server to Logical Storage Dependency
            context.ServerOrApplianceToExternalLogicalStorageDependencys.Add(new ServerOrApplianceToExternalLogicalStorageDependency() { DownstreamServerOrApplianceId = 1, UpstreamExternalLogicalStorageId = 2 });
            //Logical Storage to Server/Applicance
            context.ExternalLogicalStorageDependencyToServerOrAppliances.Add(new ExternalLogicalStorageDependencyToServerOrAppliance() { DownstreamExternalLogicalStorageId = 1, UpstreamServerOrApplianceId = 1 });
            context.ExternalLogicalStorageDependencyToServerOrAppliances.Add(new ExternalLogicalStorageDependencyToServerOrAppliance() { DownstreamExternalLogicalStorageId = 2, UpstreamServerOrApplianceId = 2 });
            context.ExternalLogicalStorageDependencyToServerOrAppliances.Add(new ExternalLogicalStorageDependencyToServerOrAppliance() { DownstreamExternalLogicalStorageId = 5, UpstreamServerOrApplianceId = 1 });
            context.ExternalLogicalStorageDependencyToServerOrAppliances.Add(new ExternalLogicalStorageDependencyToServerOrAppliance() { DownstreamExternalLogicalStorageId = 4, UpstreamServerOrApplianceId = 4 });
            context.ExternalLogicalStorageDependencyToServerOrAppliances.Add(new ExternalLogicalStorageDependencyToServerOrAppliance() { DownstreamExternalLogicalStorageId = 3, UpstreamServerOrApplianceId = 3 });
            //Database to Server
            context.DatabaseToServerOrApplianceDependencys.Add(new DatabaseToServerOrApplianceDependency() { DownstreamDatabaseId = 1 , UpstreamServerOrApplianceId = 6 });
            context.DatabaseToServerOrApplianceDependencys.Add(new DatabaseToServerOrApplianceDependency() { DownstreamDatabaseId = 2, UpstreamServerOrApplianceId = 4 });
            context.DatabaseToServerOrApplianceDependencys.Add(new DatabaseToServerOrApplianceDependency() { DownstreamDatabaseId = 3, UpstreamServerOrApplianceId = 5 });
            context.DatabaseToServerOrApplianceDependencys.Add(new DatabaseToServerOrApplianceDependency() { DownstreamDatabaseId = 4, UpstreamServerOrApplianceId = 7 });
            //Initial Software or Frameworks
            context.SoftwareOrFrameworks.Add(new SoftwareOrFrameworkNode() { SoftwareOrFrameworkStorageId = 1, SoftwareOrFrameworkName = ".Net 2.0", FriendlyName = "Microsoft .Net Framework 2.0" });
            context.SoftwareOrFrameworks.Add(new SoftwareOrFrameworkNode() { SoftwareOrFrameworkStorageId = 2, SoftwareOrFrameworkName = ".Net 4.5.2", FriendlyName = "Microsoft .Net Framework 4.5.2" });
            context.SoftwareOrFrameworks.Add(new SoftwareOrFrameworkNode() { SoftwareOrFrameworkStorageId = 3, SoftwareOrFrameworkName = "IIS 7.5", FriendlyName = "Microsoft Internet Information Server 7.5" });
            //Applications to Software
            context.ApplicationToSoftwareOrFrameworkDependencys.Add(new ApplicationToSoftwareOrFrameworkDependency() { DownstreamApplicationId = 1, DownstreamDatabaseRevision = 3, UpstreamApplicationToSoftwareOrFrameworkDependencyID = 2});
            context.ApplicationToSoftwareOrFrameworkDependencys.Add(new ApplicationToSoftwareOrFrameworkDependency() { DownstreamApplicationId = 1, DownstreamDatabaseRevision = 3, UpstreamApplicationToSoftwareOrFrameworkDependencyID = 3 });
            context.ApplicationToSoftwareOrFrameworkDependencys.Add(new ApplicationToSoftwareOrFrameworkDependency() { DownstreamApplicationId = 1, DownstreamDatabaseRevision = 2, UpstreamApplicationToSoftwareOrFrameworkDependencyID = 3 });
            context.ApplicationToSoftwareOrFrameworkDependencys.Add(new ApplicationToSoftwareOrFrameworkDependency() { DownstreamApplicationId = 4, DownstreamDatabaseRevision = 1, UpstreamApplicationToSoftwareOrFrameworkDependencyID = 1 });
            context.ApplicationToSoftwareOrFrameworkDependencys.Add(new ApplicationToSoftwareOrFrameworkDependency() { DownstreamApplicationId = 2, DownstreamDatabaseRevision = 1, UpstreamApplicationToSoftwareOrFrameworkDependencyID = 2 });

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

