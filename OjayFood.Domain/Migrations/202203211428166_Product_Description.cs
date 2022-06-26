namespace OjayFood.Domain.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Product_Description : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Products", "Description", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Products", "Description");
        }
    }
}
