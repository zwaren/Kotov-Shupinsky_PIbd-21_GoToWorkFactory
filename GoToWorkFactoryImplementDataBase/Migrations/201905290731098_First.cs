namespace GoToWorkFactoryImplementDataBase.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class First : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.ClientBindingModels",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        Email = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Clients",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false),
                        Email = c.String(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.ClientViewModels",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        Email = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.MaterialBindingModels",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        Count = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Materials",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false),
                        Count = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.ProductMaterials",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        ProductId = c.Int(nullable: false),
                        MaterialId = c.Int(nullable: false),
                        Count = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Materials", t => t.MaterialId, cascadeDelete: true)
                .ForeignKey("dbo.Products", t => t.ProductId, cascadeDelete: true)
                .Index(t => t.ProductId)
                .Index(t => t.MaterialId);
            
            CreateTable(
                "dbo.Products",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false),
                        Price = c.Decimal(nullable: false, precision: 18, scale: 2),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.OrderProducts",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        OrderId = c.Int(nullable: false),
                        ProductId = c.Int(nullable: false),
                        Count = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Orders", t => t.OrderId, cascadeDelete: true)
                .ForeignKey("dbo.Products", t => t.ProductId, cascadeDelete: true)
                .Index(t => t.OrderId)
                .Index(t => t.ProductId);
            
            CreateTable(
                "dbo.Orders",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        ClientId = c.Int(nullable: false),
                        Sum = c.Decimal(nullable: false, precision: 18, scale: 2),
                        Reserved = c.Boolean(nullable: false),
                        Status = c.Int(nullable: false),
                        DateCreate = c.DateTime(nullable: false),
                        DateImplement = c.DateTime(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Clients", t => t.ClientId, cascadeDelete: true)
                .Index(t => t.ClientId);
            
            CreateTable(
                "dbo.MaterialViewModels",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        Count = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.OrderViewModels",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        ClientId = c.Int(nullable: false),
                        ClientName = c.String(),
                        Sum = c.Decimal(nullable: false, precision: 18, scale: 2),
                        Reserved = c.Boolean(nullable: false),
                        Status = c.String(),
                        DateCreate = c.String(),
                        DateImplement = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.OrderProductViewModels",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        ProductName = c.String(),
                        OrderId = c.Int(nullable: false),
                        ProductId = c.Int(nullable: false),
                        Count = c.Int(nullable: false),
                        OrderViewModel_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.OrderViewModels", t => t.OrderViewModel_Id)
                .Index(t => t.OrderViewModel_Id);
            
            CreateTable(
                "dbo.ProductBindingModels",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        Price = c.Decimal(nullable: false, precision: 18, scale: 2),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.ProductMaterialBindingModels",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        ProductId = c.Int(nullable: false),
                        MaterialId = c.Int(nullable: false),
                        MaterialName = c.String(),
                        Count = c.Int(nullable: false),
                        ProductBindingModel_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.ProductBindingModels", t => t.ProductBindingModel_Id)
                .Index(t => t.ProductBindingModel_Id);
            
            CreateTable(
                "dbo.ProductViewModels",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        Price = c.Decimal(nullable: false, precision: 18, scale: 2),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.ProductMaterialViewModels",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        ProductId = c.Int(nullable: false),
                        MaterialId = c.Int(nullable: false),
                        MaterialName = c.String(),
                        Count = c.Int(nullable: false),
                        ProductViewModel_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.ProductViewModels", t => t.ProductViewModel_Id)
                .Index(t => t.ProductViewModel_Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.ProductMaterialViewModels", "ProductViewModel_Id", "dbo.ProductViewModels");
            DropForeignKey("dbo.ProductMaterialBindingModels", "ProductBindingModel_Id", "dbo.ProductBindingModels");
            DropForeignKey("dbo.OrderProductViewModels", "OrderViewModel_Id", "dbo.OrderViewModels");
            DropForeignKey("dbo.ProductMaterials", "ProductId", "dbo.Products");
            DropForeignKey("dbo.OrderProducts", "ProductId", "dbo.Products");
            DropForeignKey("dbo.OrderProducts", "OrderId", "dbo.Orders");
            DropForeignKey("dbo.Orders", "ClientId", "dbo.Clients");
            DropForeignKey("dbo.ProductMaterials", "MaterialId", "dbo.Materials");
            DropIndex("dbo.ProductMaterialViewModels", new[] { "ProductViewModel_Id" });
            DropIndex("dbo.ProductMaterialBindingModels", new[] { "ProductBindingModel_Id" });
            DropIndex("dbo.OrderProductViewModels", new[] { "OrderViewModel_Id" });
            DropIndex("dbo.Orders", new[] { "ClientId" });
            DropIndex("dbo.OrderProducts", new[] { "ProductId" });
            DropIndex("dbo.OrderProducts", new[] { "OrderId" });
            DropIndex("dbo.ProductMaterials", new[] { "MaterialId" });
            DropIndex("dbo.ProductMaterials", new[] { "ProductId" });
            DropTable("dbo.ProductMaterialViewModels");
            DropTable("dbo.ProductViewModels");
            DropTable("dbo.ProductMaterialBindingModels");
            DropTable("dbo.ProductBindingModels");
            DropTable("dbo.OrderProductViewModels");
            DropTable("dbo.OrderViewModels");
            DropTable("dbo.MaterialViewModels");
            DropTable("dbo.Orders");
            DropTable("dbo.OrderProducts");
            DropTable("dbo.Products");
            DropTable("dbo.ProductMaterials");
            DropTable("dbo.Materials");
            DropTable("dbo.MaterialBindingModels");
            DropTable("dbo.ClientViewModels");
            DropTable("dbo.Clients");
            DropTable("dbo.ClientBindingModels");
        }
    }
}
