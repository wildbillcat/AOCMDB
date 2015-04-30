namespace AOCMDB.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Added_relaionships_and_nodes : DbMigration
    {
        public override void Up()
        {
            RenameTable(name: "dbo.Applications", newName: "ApplicationNodes");
            RenameTable(name: "dbo.UpstreamApplicationDependencies", newName: "ApplicationToApplicationDependencies");
            CreateTable(
                "dbo.ApplicationToDatabaseDependencies",
                c => new
                    {
                        DownstreamApplicationId = c.Long(nullable: false),
                        DownstreamDatabaseRevision = c.Long(nullable: false),
                        UpstreamDatabaseNodeID = c.Long(nullable: false),
                    })
                .PrimaryKey(t => new { t.DownstreamApplicationId, t.DownstreamDatabaseRevision, t.UpstreamDatabaseNodeID });
            
            CreateTable(
                "dbo.ApplicationToExternalLogicalStorageDependencies",
                c => new
                    {
                        DownstreamApplicationId = c.Long(nullable: false),
                        DownstreamDatabaseRevision = c.Long(nullable: false),
                        UpstreamExternalLogicalStorageNodeNodeID = c.Long(nullable: false),
                    })
                .PrimaryKey(t => new { t.DownstreamApplicationId, t.DownstreamDatabaseRevision, t.UpstreamExternalLogicalStorageNodeNodeID });
            
            CreateTable(
                "dbo.ApplicationToServerOrApplianceDependencies",
                c => new
                    {
                        DownstreamApplicationId = c.Long(nullable: false),
                        DownstreamDatabaseRevision = c.Long(nullable: false),
                        UpstreamServerOrApplianceNodeID = c.Long(nullable: false),
                    })
                .PrimaryKey(t => new { t.DownstreamApplicationId, t.DownstreamDatabaseRevision, t.UpstreamServerOrApplianceNodeID });
            
            CreateTable(
                "dbo.ApplicationToSoftwareOrFrameworkDependencies",
                c => new
                    {
                        DownstreamApplicationId = c.Long(nullable: false),
                        DownstreamDatabaseRevision = c.Long(nullable: false),
                        UpstreamExternalLogicalStorageNodeNodeID = c.Long(nullable: false),
                    })
                .PrimaryKey(t => new { t.DownstreamApplicationId, t.DownstreamDatabaseRevision, t.UpstreamExternalLogicalStorageNodeNodeID });
            
            CreateTable(
                "dbo.DatabaseNodes",
                c => new
                    {
                        DatabaseId = c.Long(nullable: false, identity: true),
                        DatabaseName = c.String(nullable: false),
                        FriendlyName = c.String(),
                        DatabaseType = c.String(),
                        Details = c.String(),
                    })
                .PrimaryKey(t => t.DatabaseId);
            
            CreateTable(
                "dbo.DatabaseToServerOrApplianceDependencies",
                c => new
                    {
                        DownstreamDatabaseId = c.Long(nullable: false),
                        UpstreamServerOrApplianceId = c.Long(nullable: false),
                    })
                .PrimaryKey(t => new { t.DownstreamDatabaseId, t.UpstreamServerOrApplianceId });
            
            CreateTable(
                "dbo.ExternalLogicalStorageDependencyToServerOrAppliances",
                c => new
                    {
                        DownstreamExternalLogicalStorageId = c.Long(nullable: false),
                        UpstreamServerOrApplianceId = c.Long(nullable: false),
                    })
                .PrimaryKey(t => new { t.DownstreamExternalLogicalStorageId, t.UpstreamServerOrApplianceId });
            
            CreateTable(
                "dbo.ExternalLogicalStorageNodes",
                c => new
                    {
                        ExternalLogicalStorageId = c.Long(nullable: false, identity: true),
                        ExternalLogicalStorageName = c.String(nullable: false),
                        FriendlyName = c.String(),
                        Details = c.String(),
                    })
                .PrimaryKey(t => t.ExternalLogicalStorageId);
            
            CreateTable(
                "dbo.ServerOrApplianceNodes",
                c => new
                    {
                        ServerOrApplianceId = c.Long(nullable: false, identity: true),
                        DatabaseName = c.String(nullable: false),
                        FriendlyName = c.String(),
                        Details = c.String(),
                    })
                .PrimaryKey(t => t.ServerOrApplianceId);
            
            CreateTable(
                "dbo.ServerOrApplianceToExternalLogicalStorageDependencies",
                c => new
                    {
                        DownstreamServerOrApplianceId = c.Long(nullable: false),
                        UpstreamExternalLogicalStorageId = c.Long(nullable: false),
                    })
                .PrimaryKey(t => new { t.DownstreamServerOrApplianceId, t.UpstreamExternalLogicalStorageId });
            
            CreateTable(
                "dbo.SoftwareOrFrameworkNodes",
                c => new
                    {
                        SoftwareOrFrameworkStorageId = c.Long(nullable: false, identity: true),
                        SoftwareOrFrameworkName = c.String(nullable: false),
                        FriendlyName = c.String(),
                        Details = c.String(),
                    })
                .PrimaryKey(t => t.SoftwareOrFrameworkStorageId);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.SoftwareOrFrameworkNodes");
            DropTable("dbo.ServerOrApplianceToExternalLogicalStorageDependencies");
            DropTable("dbo.ServerOrApplianceNodes");
            DropTable("dbo.ExternalLogicalStorageNodes");
            DropTable("dbo.ExternalLogicalStorageDependencyToServerOrAppliances");
            DropTable("dbo.DatabaseToServerOrApplianceDependencies");
            DropTable("dbo.DatabaseNodes");
            DropTable("dbo.ApplicationToSoftwareOrFrameworkDependencies");
            DropTable("dbo.ApplicationToServerOrApplianceDependencies");
            DropTable("dbo.ApplicationToExternalLogicalStorageDependencies");
            DropTable("dbo.ApplicationToDatabaseDependencies");
            RenameTable(name: "dbo.ApplicationToApplicationDependencies", newName: "UpstreamApplicationDependencies");
            RenameTable(name: "dbo.ApplicationNodes", newName: "Applications");
        }
    }
}
