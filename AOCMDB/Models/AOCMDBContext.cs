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

        public IEnumerable<Application> GetLatestApplicationVersions()
        {
            return this.Applications.GroupBy(p => p.ApplicationId)
                        .Select(group => group.Where(x => x.DatabaseRevision == group.Max(y => y.DatabaseRevision)).FirstOrDefault());
        }

    }

    public class DefaultTestDataInitializer : DropCreateDatabaseAlways<AOCMDBContext>
    {
        protected override void Seed(AOCMDBContext context)
        {
            Application ContosoApp = new Application() { ApplicationId = 1, DatabaseRevision = 1, CreatedByUser = "wildbillcat", CreatedAt = DateTime.Now, ApplicationName = "ContosoCrossing", GlobalApplicationID = 526 };
            context.Applications.Add(ContosoApp);
            context.Applications.Add(new Application() { ApplicationId = 1, DatabaseRevision = 2, CreatedByUser = "testuser1", CreatedAt = DateTime.Now, ApplicationName = "SuperContosoCrossing", GlobalApplicationID = 526 });
            context.Applications.Add(new Application() { ApplicationId = 1, DatabaseRevision = 3, CreatedByUser = "wildbillcat", CreatedAt = DateTime.Now, ApplicationName = "ContosoCrossing", GlobalApplicationID = 528 });
            List<Application> ContosoDependency = new List<Application>();
            ContosoDependency.Add(ContosoApp);

            context.Applications.Add(new Application() { ApplicationId = 2, DatabaseRevision = 1, CreatedByUser = "wildbillcat", CreatedAt = DateTime.Now, ApplicationName = "ZiflandiaApp", GlobalApplicationID = 522, UpstreamApplicationDependency = ContosoDependency });
            context.Applications.Add(new Application() { ApplicationId = 3, DatabaseRevision = 1, CreatedByUser = "wildbillcat", CreatedAt = DateTime.Now, ApplicationName = "ClickOnceMadness", GlobalApplicationID = 523 });
            context.Applications.Add(new Application() { ApplicationId = 4, DatabaseRevision = 1, CreatedByUser = "wildbillcat", CreatedAt = DateTime.Now, ApplicationName = "FlappyHappy", GlobalApplicationID = 524, UpstreamApplicationDependency = ContosoDependency });
            
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

