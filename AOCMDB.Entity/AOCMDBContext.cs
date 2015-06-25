using System;
using System.Data.Entity;
using System.Linq;
using System.Collections.Generic;
using System.Data.Common;

namespace AOCMDB.Entity
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
        public DbSet<Dependency> Dependency { get; set; }
        

    }

   
    /// <summary>
    /// Seed Data
    /// </summary>
    public class DefaultTestDataInitializer : DropCreateDatabaseAlways<AOCMDBContext>
    {
        protected override void Seed(AOCMDBContext context)
        {

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

