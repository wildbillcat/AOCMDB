using System.Data.Entity;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using AOCMDB.Models.Data;
using System.Linq;
using System.Collections.Generic;
using System.Data.Common;

namespace AOCMDB.Models
{
    // You can add profile data for the user by adding more properties to your ApplicationUser class, please visit http://go.microsoft.com/fwlink/?LinkID=317594 to learn more.
    public class ApplicationUser : IdentityUser
    {
        public async Task<ClaimsIdentity> GenerateUserIdentityAsync(UserManager<ApplicationUser> manager)
        {
            // Note the authenticationType must match the one defined in CookieAuthenticationOptions.AuthenticationType
            var userIdentity = await manager.CreateIdentityAsync(this, DefaultAuthenticationTypes.ApplicationCookie);
            // Add custom user claims here
            return userIdentity;
        }
    }

    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public ApplicationDbContext() : base("DefaultConnection", throwIfV1Schema: false)
        {
        }

        public ApplicationDbContext(DbConnection connection) : base(connection, true)
        {
        }

        DbSet<Dependency> Dependencies { get; set; }
        DbSet<Application> Applications { get; set; }
        DbSet<DatabaseOrWarehouse> DatabaseOrWarehouses { get; set; }
        DbSet<ExternalLogicalStorage> ExternalLogicalStorages { get; set; }
        DbSet<ServerOrAppliance> ServerOrAppliances { get; set; }
        DbSet<SoftwareOrFramework> SoftwareOrFrameworks { get; set; }
        DbSet<SourceCodeRepository> SourceCodeRepositories { get; set; }

        public class DefaultStartDataInitializer : CreateDatabaseIfNotExists<ApplicationDbContext>
        {
            protected override void Seed(ApplicationDbContext context)
            {
                base.Seed(context);
            }
        }

        public static ApplicationDbContext Create()
        {
            return new ApplicationDbContext();
        }
    }
}