using System;
using Microsoft.Data.Entity;


namespace AOCMDB.Models
{
    public class AOCMDBContext : DbContext
    {
        public DbSet<Application> Applications { get; set; }



    }
}

