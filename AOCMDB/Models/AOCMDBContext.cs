using System;
using System.Data.Entity;


namespace AOCMDB.Models
{
    public class AOCMDBContext : DbContext
    {
        
        public DbSet<Application> Applications { get; set; }

    }
    
}

