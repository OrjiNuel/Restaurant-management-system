namespace OjayFood.Domain.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ShippingDetail_FinalCreate : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.ShippingDetails", "Username_Id", "dbo.Users");
            DropIndex("dbo.ShippingDetails", new[] { "Username_Id" });
            AddColumn("dbo.ShippingDetails", "Name", c => c.String(nullable: false));
            AddColumn("dbo.ShippingDetails", "Line1", c => c.String(nullable: false));
            AddColumn("dbo.ShippingDetails", "Line2", c => c.String());
            AddColumn("dbo.ShippingDetails", "Line3", c => c.String());
            AddColumn("dbo.ShippingDetails", "City", c => c.String(nullable: false));
            AddColumn("dbo.ShippingDetails", "Zip", c => c.String());
            AddColumn("dbo.ShippingDetails", "GiftWrap", c => c.Boolean(nullable: false));
            AlterColumn("dbo.ShippingDetails", "Country", c => c.String(nullable: false));
            AlterColumn("dbo.ShippingDetails", "State", c => c.String(nullable: false));
            DropColumn("dbo.ShippingDetails", "Address");
            DropColumn("dbo.ShippingDetails", "Province");
            DropColumn("dbo.ShippingDetails", "Username_Id");
        }
        
        public override void Down()
        {
            AddColumn("dbo.ShippingDetails", "Username_Id", c => c.Int());
            AddColumn("dbo.ShippingDetails", "Province", c => c.String());
            AddColumn("dbo.ShippingDetails", "Address", c => c.String());
            AlterColumn("dbo.ShippingDetails", "State", c => c.String());
            AlterColumn("dbo.ShippingDetails", "Country", c => c.String());
            DropColumn("dbo.ShippingDetails", "GiftWrap");
            DropColumn("dbo.ShippingDetails", "Zip");
            DropColumn("dbo.ShippingDetails", "City");
            DropColumn("dbo.ShippingDetails", "Line3");
            DropColumn("dbo.ShippingDetails", "Line2");
            DropColumn("dbo.ShippingDetails", "Line1");
            DropColumn("dbo.ShippingDetails", "Name");
            CreateIndex("dbo.ShippingDetails", "Username_Id");
            AddForeignKey("dbo.ShippingDetails", "Username_Id", "dbo.Users", "Id");
        }
    }
}
