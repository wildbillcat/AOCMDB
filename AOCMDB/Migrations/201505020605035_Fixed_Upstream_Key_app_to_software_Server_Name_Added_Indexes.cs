namespace AOCMDB.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Fixed_Upstream_Key_app_to_software_Server_Name_Added_Indexes : DbMigration
    {
        public override void Up()
        {
            DropPrimaryKey("dbo.ApplicationToSoftwareOrFrameworkDependencies");
            AddColumn("dbo.ApplicationToSoftwareOrFrameworkDependencies", "UpstreamApplicationToSoftwareOrFrameworkDependencyID", c => c.Long(nullable: false));
            AddColumn("dbo.ServerOrApplianceNodes", "ServerOrApplianceName", c => c.String(nullable: false, maxLength: 450));
            AlterColumn("dbo.DatabaseNodes", "DatabaseName", c => c.String(nullable: false, maxLength: 450));
            AlterColumn("dbo.ExternalLogicalStorageNodes", "ExternalLogicalStorageName", c => c.String(nullable: false, maxLength: 450));
            AlterColumn("dbo.SoftwareOrFrameworkNodes", "SoftwareOrFrameworkName", c => c.String(nullable: false, maxLength: 450));
            AddPrimaryKey("dbo.ApplicationToSoftwareOrFrameworkDependencies", new[] { "DownstreamApplicationId", "DownstreamDatabaseRevision", "UpstreamApplicationToSoftwareOrFrameworkDependencyID" });
            CreateIndex("dbo.DatabaseNodes", "DatabaseName", unique: true);
            CreateIndex("dbo.ExternalLogicalStorageNodes", "ExternalLogicalStorageName", unique: true);
            CreateIndex("dbo.ServerOrApplianceNodes", "ServerOrApplianceName", unique: true);
            CreateIndex("dbo.SoftwareOrFrameworkNodes", "SoftwareOrFrameworkName", unique: true);
            DropColumn("dbo.ApplicationToSoftwareOrFrameworkDependencies", "UpstreamExternalLogicalStorageNodeNodeID");
            DropColumn("dbo.ServerOrApplianceNodes", "DatabaseName");
        }
        
        public override void Down()
        {
            AddColumn("dbo.ServerOrApplianceNodes", "DatabaseName", c => c.String(nullable: false));
            AddColumn("dbo.ApplicationToSoftwareOrFrameworkDependencies", "UpstreamExternalLogicalStorageNodeNodeID", c => c.Long(nullable: false));
            DropIndex("dbo.SoftwareOrFrameworkNodes", new[] { "SoftwareOrFrameworkName" });
            DropIndex("dbo.ServerOrApplianceNodes", new[] { "ServerOrApplianceName" });
            DropIndex("dbo.ExternalLogicalStorageNodes", new[] { "ExternalLogicalStorageName" });
            DropIndex("dbo.DatabaseNodes", new[] { "DatabaseName" });
            DropPrimaryKey("dbo.ApplicationToSoftwareOrFrameworkDependencies");
            AlterColumn("dbo.SoftwareOrFrameworkNodes", "SoftwareOrFrameworkName", c => c.String(nullable: false));
            AlterColumn("dbo.ExternalLogicalStorageNodes", "ExternalLogicalStorageName", c => c.String(nullable: false));
            AlterColumn("dbo.DatabaseNodes", "DatabaseName", c => c.String(nullable: false));
            DropColumn("dbo.ServerOrApplianceNodes", "ServerOrApplianceName");
            DropColumn("dbo.ApplicationToSoftwareOrFrameworkDependencies", "UpstreamApplicationToSoftwareOrFrameworkDependencyID");
            AddPrimaryKey("dbo.ApplicationToSoftwareOrFrameworkDependencies", new[] { "DownstreamApplicationId", "DownstreamDatabaseRevision", "UpstreamExternalLogicalStorageNodeNodeID" });
        }
    }
}
