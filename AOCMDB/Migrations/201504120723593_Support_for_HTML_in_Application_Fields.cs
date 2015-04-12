namespace AOCMDB.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Support_for_HTML_in_Application_Fields : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Applications",
                c => new
                    {
                        ApplicationId = c.Int(nullable: false),
                        DatabaseRevision = c.Int(nullable: false),
                        CreatedByUser = c.String(nullable: false),
                        CreatedAt = c.DateTime(nullable: false),
                        ApplicationName = c.String(nullable: false),
                        GlobalApplicationID = c.Int(nullable: false),
                        SiteURL = c.String(),
                        NetworkDiagramOrInventory = c.String(),
                        AdministrativeProcedures = c.String(),
                        ContactInformation = c.String(),
                        ClientConfigurationAndValidation = c.String(),
                        ServerConfigurationandValidation = c.String(),
                        RecoveryProcedures = c.String(),
                    })
                .PrimaryKey(t => new { t.ApplicationId, t.DatabaseRevision });
            
        }
        
        public override void Down()
        {
            DropTable("dbo.Applications");
        }
    }
}
