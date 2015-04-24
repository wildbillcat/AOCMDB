namespace AOCMDB.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Add_Upstream_Application_Dependency_Model : DbMigration
    {
        public override void Up()
        {
            DropPrimaryKey("dbo.Applications");
            CreateTable(
                "dbo.UpstreamApplicationDependencies",
                c => new
                    {
                        UpstreamApplicationDependencyId = c.Long(nullable: false),
                        DatabaseRevision = c.Long(nullable: false),
                        CreatedByUser = c.String(nullable: false),
                        CreatedAt = c.DateTime(nullable: false),
                        UpstreamApplicationID = c.Long(nullable: false),
                        Application_ApplicationId = c.Long(),
                        Application_DatabaseRevision = c.Long(),
                    })
                .PrimaryKey(t => new { t.UpstreamApplicationDependencyId, t.DatabaseRevision })
                .ForeignKey("dbo.Applications", t => new { t.Application_ApplicationId, t.Application_DatabaseRevision })
                .Index(t => new { t.Application_ApplicationId, t.Application_DatabaseRevision });
            
            AlterColumn("dbo.Applications", "ApplicationId", c => c.Long(nullable: false));
            AlterColumn("dbo.Applications", "DatabaseRevision", c => c.Long(nullable: false));
            AddPrimaryKey("dbo.Applications", new[] { "ApplicationId", "DatabaseRevision" });
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.UpstreamApplicationDependencies", new[] { "Application_ApplicationId", "Application_DatabaseRevision" }, "dbo.Applications");
            DropIndex("dbo.UpstreamApplicationDependencies", new[] { "Application_ApplicationId", "Application_DatabaseRevision" });
            DropPrimaryKey("dbo.Applications");
            AlterColumn("dbo.Applications", "DatabaseRevision", c => c.Int(nullable: false));
            AlterColumn("dbo.Applications", "ApplicationId", c => c.Int(nullable: false));
            DropTable("dbo.UpstreamApplicationDependencies");
            AddPrimaryKey("dbo.Applications", new[] { "ApplicationId", "DatabaseRevision" });
        }
    }
}
