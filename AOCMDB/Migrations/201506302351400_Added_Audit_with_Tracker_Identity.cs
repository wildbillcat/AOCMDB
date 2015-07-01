namespace AOCMDB.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Added_Audit_with_Tracker_Identity : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.AuditLogs",
                c => new
                    {
                        AuditLogId = c.Int(nullable: false, identity: true),
                        UserName = c.String(),
                        EventDateUTC = c.DateTimeOffset(nullable: false, precision: 7),
                        EventType = c.Int(nullable: false),
                        TableName = c.String(nullable: false, maxLength: 256),
                        RecordId = c.String(nullable: false, maxLength: 256),
                    })
                .PrimaryKey(t => t.AuditLogId);
            
            CreateTable(
                "dbo.AuditLogDetails",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        ColumnName = c.String(nullable: false, maxLength: 256),
                        OriginalValue = c.String(),
                        NewValue = c.String(),
                        AuditLogId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.AuditLogs", t => t.AuditLogId, cascadeDelete: true)
                .Index(t => t.AuditLogId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.AuditLogDetails", "AuditLogId", "dbo.AuditLogs");
            DropIndex("dbo.AuditLogDetails", new[] { "AuditLogId" });
            DropTable("dbo.AuditLogDetails");
            DropTable("dbo.AuditLogs");
        }
    }
}
