namespace AOCMDB.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Fixed_Server_Name_Added_Indexes_to_Names : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.ServerOrApplianceNodes", "ServerOrApplianceName", c => c.String(nullable: false));
            CreateIndex("dbo.ApplicationNodes", "ApplicationName", unique: true);
            CreateIndex("dbo.DatabaseNodes", "DatabaseName", unique: true);
            CreateIndex("dbo.ExternalLogicalStorageNodes", "ExternalLogicalStorageName", unique: true);
            CreateIndex("dbo.ServerOrApplianceNodes", "ServerOrApplianceName", unique: true);
            CreateIndex("dbo.SoftwareOrFrameworkNodes", "SoftwareOrFrameworkName", unique: true);
            DropColumn("dbo.ServerOrApplianceNodes", "DatabaseName");
        }
        
        public override void Down()
        {
            AddColumn("dbo.ServerOrApplianceNodes", "DatabaseName", c => c.String(nullable: false));
            DropIndex("dbo.SoftwareOrFrameworkNodes", new[] { "SoftwareOrFrameworkName" });
            DropIndex("dbo.ServerOrApplianceNodes", new[] { "ServerOrApplianceName" });
            DropIndex("dbo.ExternalLogicalStorageNodes", new[] { "ExternalLogicalStorageName" });
            DropIndex("dbo.DatabaseNodes", new[] { "DatabaseName" });
            DropIndex("dbo.ApplicationNodes", new[] { "ApplicationName" });
            DropColumn("dbo.ServerOrApplianceNodes", "ServerOrApplianceName");
        }
    }
}
