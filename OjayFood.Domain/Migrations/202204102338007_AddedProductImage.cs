namespace OjayFood.Domain.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddedProductImage : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.ProductImages",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        ProductID = c.Int(nullable: false),
                        ImageID = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.Images", t => t.ImageID, cascadeDelete: true)
                .ForeignKey("dbo.Products", t => t.ProductID, cascadeDelete: true)
                .Index(t => t.ProductID)
                .Index(t => t.ImageID);
            
            CreateTable(
                "dbo.Images",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        URL = c.String(),
                    })
                .PrimaryKey(t => t.ID);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.ProductImages", "ProductID", "dbo.Products");
            DropForeignKey("dbo.ProductImages", "ImageID", "dbo.Images");
            DropIndex("dbo.ProductImages", new[] { "ImageID" });
            DropIndex("dbo.ProductImages", new[] { "ProductID" });
            DropTable("dbo.Images");
            DropTable("dbo.ProductImages");
        }
    }
}
