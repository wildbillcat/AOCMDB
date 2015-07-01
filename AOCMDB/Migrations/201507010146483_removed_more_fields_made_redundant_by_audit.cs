namespace AOCMDB.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class removed_more_fields_made_redundant_by_audit : DbMigration
    {
        public override void Up()
        {
            DropColumn("dbo.Dependencies", "CreatedByUser");
            DropColumn("dbo.Dependencies", "CreatedAt");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Dependencies", "CreatedAt", c => c.DateTime(nullable: false));
            AddColumn("dbo.Dependencies", "CreatedByUser", c => c.String(nullable: false));
        }
    }
}
