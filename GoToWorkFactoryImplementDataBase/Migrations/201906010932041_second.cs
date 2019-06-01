namespace GoToWorkFactoryImplementDataBase.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class second : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Requests",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Count = c.Int(nullable: false),
                        MaterialId = c.Int(nullable: false),
                        ImplementDate = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Materials", t => t.MaterialId, cascadeDelete: true)
                .Index(t => t.MaterialId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Requests", "MaterialId", "dbo.Materials");
            DropIndex("dbo.Requests", new[] { "MaterialId" });
            DropTable("dbo.Requests");
        }
    }
}
