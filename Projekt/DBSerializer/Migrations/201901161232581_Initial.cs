namespace DBSerializer.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Initial : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.DbReflectionModels",
                c => new
                    {
                        DbReflectionModelId = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false, maxLength: 100, unicode: false),
                    })
                .PrimaryKey(t => t.DbReflectionModelId);
            
            CreateTable(
                "dbo.DbNamespaceModels",
                c => new
                    {
                        DbNamespaceModelId = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false, maxLength: 150, unicode: false),
                        DbReflectionModel_DbReflectionModelId = c.Int(),
                    })
                .PrimaryKey(t => t.DbNamespaceModelId)
                .ForeignKey("dbo.DbReflectionModels", t => t.DbReflectionModel_DbReflectionModelId)
                .Index(t => t.DbReflectionModel_DbReflectionModelId);
            
            CreateTable(
                "dbo.DbReflectedTypes",
                c => new
                    {
                        DbReflectedTypeId = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false, maxLength: 150, unicode: false),
                        Namespace = c.String(),
                        IsStatic = c.Boolean(nullable: false),
                        IsAbstract = c.Boolean(nullable: false),
                        TypeKind = c.Int(nullable: false),
                        Access = c.Int(nullable: false),
                        DbReflectedType_DbReflectedTypeId = c.Int(),
                        BaseType_DbReflectedTypeId = c.Int(),
                        DbReflectedType_DbReflectedTypeId1 = c.Int(),
                        DbNamespaceModel_DbNamespaceModelId = c.Int(),
                        DbNamespaceModel_DbNamespaceModelId1 = c.Int(),
                        DbNamespaceModel_DbNamespaceModelId2 = c.Int(),
                    })
                .PrimaryKey(t => t.DbReflectedTypeId)
                .ForeignKey("dbo.DbReflectedTypes", t => t.DbReflectedType_DbReflectedTypeId)
                .ForeignKey("dbo.DbReflectedTypes", t => t.BaseType_DbReflectedTypeId)
                .ForeignKey("dbo.DbReflectedTypes", t => t.DbReflectedType_DbReflectedTypeId1)
                .ForeignKey("dbo.DbNamespaceModels", t => t.DbNamespaceModel_DbNamespaceModelId)
                .ForeignKey("dbo.DbNamespaceModels", t => t.DbNamespaceModel_DbNamespaceModelId1)
                .ForeignKey("dbo.DbNamespaceModels", t => t.DbNamespaceModel_DbNamespaceModelId2)
                .Index(t => t.DbReflectedType_DbReflectedTypeId)
                .Index(t => t.BaseType_DbReflectedTypeId)
                .Index(t => t.DbReflectedType_DbReflectedTypeId1)
                .Index(t => t.DbNamespaceModel_DbNamespaceModelId)
                .Index(t => t.DbNamespaceModel_DbNamespaceModelId1)
                .Index(t => t.DbNamespaceModel_DbNamespaceModelId2);
            
            CreateTable(
                "dbo.DbMethodModels",
                c => new
                    {
                        DbMethodModelId = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false, maxLength: 150, unicode: false),
                        Access = c.Int(nullable: false),
                        ReturnType_DbReflectedTypeId = c.Int(),
                        DbReflectedType_DbReflectedTypeId = c.Int(),
                        DbReflectedType_DbReflectedTypeId1 = c.Int(),
                    })
                .PrimaryKey(t => t.DbMethodModelId)
                .ForeignKey("dbo.DbReflectedTypes", t => t.ReturnType_DbReflectedTypeId)
                .ForeignKey("dbo.DbReflectedTypes", t => t.DbReflectedType_DbReflectedTypeId)
                .ForeignKey("dbo.DbReflectedTypes", t => t.DbReflectedType_DbReflectedTypeId1)
                .Index(t => t.ReturnType_DbReflectedTypeId)
                .Index(t => t.DbReflectedType_DbReflectedTypeId)
                .Index(t => t.DbReflectedType_DbReflectedTypeId1);
            
            CreateTable(
                "dbo.DbParameterModels",
                c => new
                    {
                        DbParameterModelId = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false, maxLength: 150, unicode: false),
                        ParamType_DbReflectedTypeId = c.Int(),
                        DbMethodModel_DbMethodModelId = c.Int(),
                    })
                .PrimaryKey(t => t.DbParameterModelId)
                .ForeignKey("dbo.DbReflectedTypes", t => t.ParamType_DbReflectedTypeId)
                .ForeignKey("dbo.DbMethodModels", t => t.DbMethodModel_DbMethodModelId)
                .Index(t => t.ParamType_DbReflectedTypeId)
                .Index(t => t.DbMethodModel_DbMethodModelId);
            
            CreateTable(
                "dbo.DbFieldModels",
                c => new
                    {
                        DbFieldModelId = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false, maxLength: 150, unicode: false),
                        Access = c.Int(nullable: false),
                        Type_DbReflectedTypeId = c.Int(),
                    })
                .PrimaryKey(t => t.DbFieldModelId)
                .ForeignKey("dbo.DbReflectedTypes", t => t.Type_DbReflectedTypeId)
                .Index(t => t.Type_DbReflectedTypeId);
            
            CreateTable(
                "dbo.DbPropertyModels",
                c => new
                    {
                        DbPropertyModelId = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false, maxLength: 150, unicode: false),
                        PropertyAccess = c.Int(nullable: false),
                        GetMethod_DbMethodModelId = c.Int(),
                        SetMethod_DbMethodModelId = c.Int(),
                        Type_DbReflectedTypeId = c.Int(),
                    })
                .PrimaryKey(t => t.DbPropertyModelId)
                .ForeignKey("dbo.DbMethodModels", t => t.GetMethod_DbMethodModelId)
                .ForeignKey("dbo.DbMethodModels", t => t.SetMethod_DbMethodModelId)
                .ForeignKey("dbo.DbReflectedTypes", t => t.Type_DbReflectedTypeId)
                .Index(t => t.GetMethod_DbMethodModelId)
                .Index(t => t.SetMethod_DbMethodModelId)
                .Index(t => t.Type_DbReflectedTypeId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.DbNamespaceModels", "DbReflectionModel_DbReflectionModelId", "dbo.DbReflectionModels");
            DropForeignKey("dbo.DbReflectedTypes", "DbNamespaceModel_DbNamespaceModelId2", "dbo.DbNamespaceModels");
            DropForeignKey("dbo.DbReflectedTypes", "DbNamespaceModel_DbNamespaceModelId1", "dbo.DbNamespaceModels");
            DropForeignKey("dbo.DbReflectedTypes", "DbNamespaceModel_DbNamespaceModelId", "dbo.DbNamespaceModels");
            DropForeignKey("dbo.DbPropertyModels", "Type_DbReflectedTypeId", "dbo.DbReflectedTypes");
            DropForeignKey("dbo.DbPropertyModels", "SetMethod_DbMethodModelId", "dbo.DbMethodModels");
            DropForeignKey("dbo.DbPropertyModels", "GetMethod_DbMethodModelId", "dbo.DbMethodModels");
            DropForeignKey("dbo.DbMethodModels", "DbReflectedType_DbReflectedTypeId1", "dbo.DbReflectedTypes");
            DropForeignKey("dbo.DbReflectedTypes", "DbReflectedType_DbReflectedTypeId1", "dbo.DbReflectedTypes");
            DropForeignKey("dbo.DbFieldModels", "Type_DbReflectedTypeId", "dbo.DbReflectedTypes");
            DropForeignKey("dbo.DbMethodModels", "DbReflectedType_DbReflectedTypeId", "dbo.DbReflectedTypes");
            DropForeignKey("dbo.DbMethodModels", "ReturnType_DbReflectedTypeId", "dbo.DbReflectedTypes");
            DropForeignKey("dbo.DbParameterModels", "DbMethodModel_DbMethodModelId", "dbo.DbMethodModels");
            DropForeignKey("dbo.DbParameterModels", "ParamType_DbReflectedTypeId", "dbo.DbReflectedTypes");
            DropForeignKey("dbo.DbReflectedTypes", "BaseType_DbReflectedTypeId", "dbo.DbReflectedTypes");
            DropForeignKey("dbo.DbReflectedTypes", "DbReflectedType_DbReflectedTypeId", "dbo.DbReflectedTypes");
            DropIndex("dbo.DbPropertyModels", new[] { "Type_DbReflectedTypeId" });
            DropIndex("dbo.DbPropertyModels", new[] { "SetMethod_DbMethodModelId" });
            DropIndex("dbo.DbPropertyModels", new[] { "GetMethod_DbMethodModelId" });
            DropIndex("dbo.DbFieldModels", new[] { "Type_DbReflectedTypeId" });
            DropIndex("dbo.DbParameterModels", new[] { "DbMethodModel_DbMethodModelId" });
            DropIndex("dbo.DbParameterModels", new[] { "ParamType_DbReflectedTypeId" });
            DropIndex("dbo.DbMethodModels", new[] { "DbReflectedType_DbReflectedTypeId1" });
            DropIndex("dbo.DbMethodModels", new[] { "DbReflectedType_DbReflectedTypeId" });
            DropIndex("dbo.DbMethodModels", new[] { "ReturnType_DbReflectedTypeId" });
            DropIndex("dbo.DbReflectedTypes", new[] { "DbNamespaceModel_DbNamespaceModelId2" });
            DropIndex("dbo.DbReflectedTypes", new[] { "DbNamespaceModel_DbNamespaceModelId1" });
            DropIndex("dbo.DbReflectedTypes", new[] { "DbNamespaceModel_DbNamespaceModelId" });
            DropIndex("dbo.DbReflectedTypes", new[] { "DbReflectedType_DbReflectedTypeId1" });
            DropIndex("dbo.DbReflectedTypes", new[] { "BaseType_DbReflectedTypeId" });
            DropIndex("dbo.DbReflectedTypes", new[] { "DbReflectedType_DbReflectedTypeId" });
            DropIndex("dbo.DbNamespaceModels", new[] { "DbReflectionModel_DbReflectionModelId" });
            DropTable("dbo.DbPropertyModels");
            DropTable("dbo.DbFieldModels");
            DropTable("dbo.DbParameterModels");
            DropTable("dbo.DbMethodModels");
            DropTable("dbo.DbReflectedTypes");
            DropTable("dbo.DbNamespaceModels");
            DropTable("dbo.DbReflectionModels");
        }
    }
}
