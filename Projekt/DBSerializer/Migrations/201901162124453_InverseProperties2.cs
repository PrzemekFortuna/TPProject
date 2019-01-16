namespace DBSerializer.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class InverseProperties2 : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.DbReflectedTypes", "DbReflectedType_DbReflectedTypeId", "dbo.DbReflectedTypes");
            DropIndex("dbo.DbReflectedTypes", new[] { "DbReflectedType_DbReflectedTypeId" });
            CreateTable(
                "dbo.DbReflectedTypeDbReflectedTypes",
                c => new
                    {
                        DbReflectedType_DbReflectedTypeId = c.Int(nullable: false),
                        DbReflectedType_DbReflectedTypeId1 = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.DbReflectedType_DbReflectedTypeId, t.DbReflectedType_DbReflectedTypeId1 })
                .ForeignKey("dbo.DbReflectedTypes", t => t.DbReflectedType_DbReflectedTypeId)
                .ForeignKey("dbo.DbReflectedTypes", t => t.DbReflectedType_DbReflectedTypeId1)
                .Index(t => t.DbReflectedType_DbReflectedTypeId)
                .Index(t => t.DbReflectedType_DbReflectedTypeId1);
            
            DropColumn("dbo.DbReflectedTypes", "DbReflectedType_DbReflectedTypeId");
        }
        
        public override void Down()
        {
            AddColumn("dbo.DbReflectedTypes", "DbReflectedType_DbReflectedTypeId", c => c.Int());
            DropForeignKey("dbo.DbReflectedTypeDbReflectedTypes", "DbReflectedType_DbReflectedTypeId1", "dbo.DbReflectedTypes");
            DropForeignKey("dbo.DbReflectedTypeDbReflectedTypes", "DbReflectedType_DbReflectedTypeId", "dbo.DbReflectedTypes");
            DropIndex("dbo.DbReflectedTypeDbReflectedTypes", new[] { "DbReflectedType_DbReflectedTypeId1" });
            DropIndex("dbo.DbReflectedTypeDbReflectedTypes", new[] { "DbReflectedType_DbReflectedTypeId" });
            DropTable("dbo.DbReflectedTypeDbReflectedTypes");
            CreateIndex("dbo.DbReflectedTypes", "DbReflectedType_DbReflectedTypeId");
            AddForeignKey("dbo.DbReflectedTypes", "DbReflectedType_DbReflectedTypeId", "dbo.DbReflectedTypes", "DbReflectedTypeId");
        }
    }
}
