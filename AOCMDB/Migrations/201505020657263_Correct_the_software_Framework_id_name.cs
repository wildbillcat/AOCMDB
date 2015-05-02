namespace AOCMDB.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Correct_the_software_Framework_id_name : DbMigration
    {
        public override void Up()
        {
            DropPrimaryKey("dbo.SoftwareOrFrameworkNodes");
            DropColumn("dbo.SoftwareOrFrameworkNodes", "SoftwareOrFrameworkStorageId");
            AddColumn("dbo.SoftwareOrFrameworkNodes", "SoftwareOrFrameworkId", c => c.Long(nullable: false, identity: true));
            AddPrimaryKey("dbo.SoftwareOrFrameworkNodes", "SoftwareOrFrameworkId");
        }
        
        public override void Down()
        {
            DropPrimaryKey("dbo.SoftwareOrFrameworkNodes");
            DropColumn("dbo.SoftwareOrFrameworkNodes", "SoftwareOrFrameworkId");
            AddColumn("dbo.SoftwareOrFrameworkNodes", "SoftwareOrFrameworkStorageId", c => c.Long(nullable: false, identity: true));            
            AddPrimaryKey("dbo.SoftwareOrFrameworkNodes", "SoftwareOrFrameworkStorageId");
        }
    }
}
