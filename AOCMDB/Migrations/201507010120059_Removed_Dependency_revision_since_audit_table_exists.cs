namespace AOCMDB.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Removed_Dependency_revision_since_audit_table_exists : DbMigration
    {
        public override void Up()
        {
            DropColumn("dbo.Dependencies", "DatabaseRevision");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Dependencies", "DatabaseRevision", c => c.Long(nullable: false));
        }
    }
}
