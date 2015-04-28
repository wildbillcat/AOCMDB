namespace AOCMDB.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class GaveUpOnVirtualAdaptersToMakeExplicitConnections : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.UpstreamApplicationDependencies", new[] { "Application_ApplicationId", "Application_DatabaseRevision" }, "dbo.Applications");
            DropIndex("dbo.UpstreamApplicationDependencies", new[] { "Application_ApplicationId", "Application_DatabaseRevision" });
            DropPrimaryKey("dbo.UpstreamApplicationDependencies");
            AddColumn("dbo.UpstreamApplicationDependencies", "DownstreamApplicationId", c => c.Long(nullable: false));
            AddColumn("dbo.UpstreamApplicationDependencies", "DownstreamDatabaseRevision", c => c.Long(nullable: false));
            AddPrimaryKey("dbo.UpstreamApplicationDependencies", new[] { "DownstreamApplicationId", "DownstreamDatabaseRevision", "UpstreamApplicationID" });
            DropColumn("dbo.UpstreamApplicationDependencies", "UpstreamApplicationDependencyId");
            DropColumn("dbo.UpstreamApplicationDependencies", "DatabaseRevision");
            DropColumn("dbo.UpstreamApplicationDependencies", "CreatedByUser");
            DropColumn("dbo.UpstreamApplicationDependencies", "CreatedAt");
            DropColumn("dbo.UpstreamApplicationDependencies", "Application_ApplicationId");
            DropColumn("dbo.UpstreamApplicationDependencies", "Application_DatabaseRevision");
        }
        
        public override void Down()
        {
            AddColumn("dbo.UpstreamApplicationDependencies", "Application_DatabaseRevision", c => c.Long());
            AddColumn("dbo.UpstreamApplicationDependencies", "Application_ApplicationId", c => c.Long());
            AddColumn("dbo.UpstreamApplicationDependencies", "CreatedAt", c => c.DateTime(nullable: false));
            AddColumn("dbo.UpstreamApplicationDependencies", "CreatedByUser", c => c.String(nullable: false));
            AddColumn("dbo.UpstreamApplicationDependencies", "DatabaseRevision", c => c.Long(nullable: false));
            AddColumn("dbo.UpstreamApplicationDependencies", "UpstreamApplicationDependencyId", c => c.Long(nullable: false));
            DropPrimaryKey("dbo.UpstreamApplicationDependencies");
            DropColumn("dbo.UpstreamApplicationDependencies", "DownstreamDatabaseRevision");
            DropColumn("dbo.UpstreamApplicationDependencies", "DownstreamApplicationId");
            AddPrimaryKey("dbo.UpstreamApplicationDependencies", new[] { "UpstreamApplicationDependencyId", "DatabaseRevision" });
            CreateIndex("dbo.UpstreamApplicationDependencies", new[] { "Application_ApplicationId", "Application_DatabaseRevision" });
            AddForeignKey("dbo.UpstreamApplicationDependencies", new[] { "Application_ApplicationId", "Application_DatabaseRevision" }, "dbo.Applications", new[] { "ApplicationId", "DatabaseRevision" });
        }
    }
}
