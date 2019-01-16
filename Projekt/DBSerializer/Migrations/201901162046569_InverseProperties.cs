namespace DBSerializer.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class InverseProperties : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.DbParameterModels", "DbMethodModel_DbMethodModelId", "dbo.DbMethodModels");
            DropForeignKey("dbo.DbFieldModels", "Type_DbReflectedTypeId", "dbo.DbReflectedTypes");
            DropForeignKey("dbo.DbReflectedTypes", "BaseType_DbReflectedTypeId", "dbo.DbReflectedTypes");
            DropForeignKey("dbo.DbPropertyModels", "Type_DbReflectedTypeId", "dbo.DbReflectedTypes");
            DropIndex("dbo.DbParameterModels", new[] { "DbMethodModel_DbMethodModelId" });
            CreateTable(
                "dbo.DbParameterModelDbMethodModels",
                c => new
                    {
                        DbParameterModel_DbParameterModelId = c.Int(nullable: false),
                        DbMethodModel_DbMethodModelId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.DbParameterModel_DbParameterModelId, t.DbMethodModel_DbMethodModelId })
                .ForeignKey("dbo.DbParameterModels", t => t.DbParameterModel_DbParameterModelId, cascadeDelete: true)
                .ForeignKey("dbo.DbMethodModels", t => t.DbMethodModel_DbMethodModelId, cascadeDelete: true)
                .Index(t => t.DbParameterModel_DbParameterModelId)
                .Index(t => t.DbMethodModel_DbMethodModelId);
            
            AddColumn("dbo.DbReflectedTypes", "DbParameterModel_DbParameterModelId", c => c.Int());
            AddColumn("dbo.DbReflectedTypes", "DbFieldModel_DbFieldModelId", c => c.Int());
            AddColumn("dbo.DbReflectedTypes", "DbReflectedType_DbReflectedTypeId", c => c.Int());
            AddColumn("dbo.DbReflectedTypes", "DbPropertyModel_DbPropertyModelId", c => c.Int());
            AddColumn("dbo.DbFieldModels", "DbReflectedType_DbReflectedTypeId", c => c.Int());
            AddColumn("dbo.DbPropertyModels", "DbReflectedType_DbReflectedTypeId", c => c.Int());
            CreateIndex("dbo.DbReflectedTypes", "DbParameterModel_DbParameterModelId");
            CreateIndex("dbo.DbReflectedTypes", "DbFieldModel_DbFieldModelId");
            CreateIndex("dbo.DbReflectedTypes", "DbReflectedType_DbReflectedTypeId");
            CreateIndex("dbo.DbReflectedTypes", "DbPropertyModel_DbPropertyModelId");
            CreateIndex("dbo.DbFieldModels", "DbReflectedType_DbReflectedTypeId");
            CreateIndex("dbo.DbPropertyModels", "DbReflectedType_DbReflectedTypeId");
            AddForeignKey("dbo.DbReflectedTypes", "DbParameterModel_DbParameterModelId", "dbo.DbParameterModels", "DbParameterModelId");
            AddForeignKey("dbo.DbReflectedTypes", "DbFieldModel_DbFieldModelId", "dbo.DbFieldModels", "DbFieldModelId");
            AddForeignKey("dbo.DbReflectedTypes", "DbPropertyModel_DbPropertyModelId", "dbo.DbPropertyModels", "DbPropertyModelId");
            AddForeignKey("dbo.DbFieldModels", "DbReflectedType_DbReflectedTypeId", "dbo.DbReflectedTypes", "DbReflectedTypeId");
            AddForeignKey("dbo.DbReflectedTypes", "DbReflectedType_DbReflectedTypeId", "dbo.DbReflectedTypes", "DbReflectedTypeId");
            AddForeignKey("dbo.DbPropertyModels", "DbReflectedType_DbReflectedTypeId", "dbo.DbReflectedTypes", "DbReflectedTypeId");
            DropColumn("dbo.DbParameterModels", "DbMethodModel_DbMethodModelId");
        }
        
        public override void Down()
        {
            AddColumn("dbo.DbParameterModels", "DbMethodModel_DbMethodModelId", c => c.Int());
            DropForeignKey("dbo.DbPropertyModels", "DbReflectedType_DbReflectedTypeId", "dbo.DbReflectedTypes");
            DropForeignKey("dbo.DbReflectedTypes", "DbReflectedType_DbReflectedTypeId", "dbo.DbReflectedTypes");
            DropForeignKey("dbo.DbFieldModels", "DbReflectedType_DbReflectedTypeId", "dbo.DbReflectedTypes");
            DropForeignKey("dbo.DbReflectedTypes", "DbPropertyModel_DbPropertyModelId", "dbo.DbPropertyModels");
            DropForeignKey("dbo.DbReflectedTypes", "DbFieldModel_DbFieldModelId", "dbo.DbFieldModels");
            DropForeignKey("dbo.DbReflectedTypes", "DbParameterModel_DbParameterModelId", "dbo.DbParameterModels");
            DropForeignKey("dbo.DbParameterModelDbMethodModels", "DbMethodModel_DbMethodModelId", "dbo.DbMethodModels");
            DropForeignKey("dbo.DbParameterModelDbMethodModels", "DbParameterModel_DbParameterModelId", "dbo.DbParameterModels");
            DropIndex("dbo.DbParameterModelDbMethodModels", new[] { "DbMethodModel_DbMethodModelId" });
            DropIndex("dbo.DbParameterModelDbMethodModels", new[] { "DbParameterModel_DbParameterModelId" });
            DropIndex("dbo.DbPropertyModels", new[] { "DbReflectedType_DbReflectedTypeId" });
            DropIndex("dbo.DbFieldModels", new[] { "DbReflectedType_DbReflectedTypeId" });
            DropIndex("dbo.DbReflectedTypes", new[] { "DbPropertyModel_DbPropertyModelId" });
            DropIndex("dbo.DbReflectedTypes", new[] { "DbReflectedType_DbReflectedTypeId" });
            DropIndex("dbo.DbReflectedTypes", new[] { "DbFieldModel_DbFieldModelId" });
            DropIndex("dbo.DbReflectedTypes", new[] { "DbParameterModel_DbParameterModelId" });
            DropColumn("dbo.DbPropertyModels", "DbReflectedType_DbReflectedTypeId");
            DropColumn("dbo.DbFieldModels", "DbReflectedType_DbReflectedTypeId");
            DropColumn("dbo.DbReflectedTypes", "DbPropertyModel_DbPropertyModelId");
            DropColumn("dbo.DbReflectedTypes", "DbReflectedType_DbReflectedTypeId");
            DropColumn("dbo.DbReflectedTypes", "DbFieldModel_DbFieldModelId");
            DropColumn("dbo.DbReflectedTypes", "DbParameterModel_DbParameterModelId");
            DropTable("dbo.DbParameterModelDbMethodModels");
            CreateIndex("dbo.DbParameterModels", "DbMethodModel_DbMethodModelId");
            AddForeignKey("dbo.DbPropertyModels", "Type_DbReflectedTypeId", "dbo.DbReflectedTypes", "DbReflectedTypeId");
            AddForeignKey("dbo.DbReflectedTypes", "BaseType_DbReflectedTypeId", "dbo.DbReflectedTypes", "DbReflectedTypeId");
            AddForeignKey("dbo.DbFieldModels", "Type_DbReflectedTypeId", "dbo.DbReflectedTypes", "DbReflectedTypeId");
            AddForeignKey("dbo.DbParameterModels", "DbMethodModel_DbMethodModelId", "dbo.DbMethodModels", "DbMethodModelId");
        }
    }
}
