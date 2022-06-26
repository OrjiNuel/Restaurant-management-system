﻿namespace OjayFood.Domain.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Product_ProductURL : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Products", "ProductURL", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Products", "ProductURL");
        }
    }
}
