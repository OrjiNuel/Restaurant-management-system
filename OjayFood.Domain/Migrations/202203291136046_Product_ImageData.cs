namespace OjayFood.Domain.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Product_ImageData : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Products", "ImageData", c => c.Binary());
            AddColumn("dbo.Products", "ImageMimeType", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Products", "ImageMimeType");
            DropColumn("dbo.Products", "ImageData");
        }
    }
}
