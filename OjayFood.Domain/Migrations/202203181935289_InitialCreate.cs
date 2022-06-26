namespace OjayFood.Domain.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class InitialCreate : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.OjayFoodUsers",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 128),
                        Email = c.String(),
                        EmailConfirmed = c.Boolean(nullable: false),
                        PasswordHash = c.String(),
                        SecurityStamp = c.String(),
                        PhoneNumber = c.String(),
                        PhoneNumberConfirmed = c.Boolean(nullable: false),
                        TwoFactorEnabled = c.Boolean(nullable: false),
                        LockoutEndDateUtc = c.DateTime(),
                        LockoutEnabled = c.Boolean(nullable: false),
                        AccessFailedCount = c.Int(nullable: false),
                        UserName = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.IdentityUserClaims",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        UserId = c.String(),
                        ClaimType = c.String(),
                        ClaimValue = c.String(),
                        OjayFoodUser_Id = c.String(maxLength: 128),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.OjayFoodUsers", t => t.OjayFoodUser_Id)
                .Index(t => t.OjayFoodUser_Id);
            
            CreateTable(
                "dbo.IdentityUserLogins",
                c => new
                    {
                        UserId = c.String(nullable: false, maxLength: 128),
                        LoginProvider = c.String(),
                        ProviderKey = c.String(),
                        OjayFoodUser_Id = c.String(maxLength: 128),
                    })
                .PrimaryKey(t => t.UserId)
                .ForeignKey("dbo.OjayFoodUsers", t => t.OjayFoodUser_Id)
                .Index(t => t.OjayFoodUser_Id);
            
            CreateTable(
                "dbo.IdentityUserRoles",
                c => new
                    {
                        RoleId = c.String(nullable: false, maxLength: 128),
                        UserId = c.String(nullable: false, maxLength: 128),
                        OjayFoodUser_Id = c.String(maxLength: 128),
                        IdentityRole_Id = c.String(maxLength: 128),
                    })
                .PrimaryKey(t => new { t.RoleId, t.UserId })
                .ForeignKey("dbo.OjayFoodUsers", t => t.OjayFoodUser_Id)
                .ForeignKey("dbo.IdentityRoles", t => t.IdentityRole_Id)
                .Index(t => t.OjayFoodUser_Id)
                .Index(t => t.IdentityRole_Id);
            
            CreateTable(
                "dbo.PaymentDetails",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Amount = c.Decimal(nullable: false, precision: 16, scale: 8),
                        BankName = c.String(),
                        CardNumber = c.Int(nullable: false),
                        CVV = c.Int(nullable: false),
                        PaymentTypeId_Id = c.Int(),
                        Username_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.PaymentTypes", t => t.PaymentTypeId_Id)
                .ForeignKey("dbo.Users", t => t.Username_Id)
                .Index(t => t.PaymentTypeId_Id)
                .Index(t => t.Username_Id);
            
            CreateTable(
                "dbo.PaymentTypes",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Users",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        LastName = c.String(),
                        FirstName = c.String(),
                        Phone = c.Int(nullable: false),
                        Address = c.String(),
                        Status = c.String(),
                        Date_Of_Birth = c.DateTime(nullable: false),
                        State = c.String(),
                        Province = c.String(),
                        Username = c.String(),
                        Password = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Products",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        Price = c.Decimal(nullable: false, precision: 18, scale: 2),
                        Mfg_Date = c.DateTime(nullable: false),
                        Exp_Date = c.DateTime(nullable: false),
                        Category = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.ShippingDetails",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Address = c.String(),
                        Country = c.String(),
                        State = c.String(),
                        Province = c.String(),
                        Username_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Users", t => t.Username_Id)
                .Index(t => t.Username_Id);
            
            CreateTable(
                "dbo.Stores",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Quantity = c.Int(nullable: false),
                        Stock_Detail = c.String(),
                        ProductId_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Products", t => t.ProductId_Id)
                .Index(t => t.ProductId_Id);
            
            CreateTable(
                "dbo.IdentityRoles",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 128),
                        Name = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.IdentityUserRoles", "IdentityRole_Id", "dbo.IdentityRoles");
            DropForeignKey("dbo.Stores", "ProductId_Id", "dbo.Products");
            DropForeignKey("dbo.ShippingDetails", "Username_Id", "dbo.Users");
            DropForeignKey("dbo.PaymentDetails", "Username_Id", "dbo.Users");
            DropForeignKey("dbo.PaymentDetails", "PaymentTypeId_Id", "dbo.PaymentTypes");
            DropForeignKey("dbo.IdentityUserRoles", "OjayFoodUser_Id", "dbo.OjayFoodUsers");
            DropForeignKey("dbo.IdentityUserLogins", "OjayFoodUser_Id", "dbo.OjayFoodUsers");
            DropForeignKey("dbo.IdentityUserClaims", "OjayFoodUser_Id", "dbo.OjayFoodUsers");
            DropIndex("dbo.Stores", new[] { "ProductId_Id" });
            DropIndex("dbo.ShippingDetails", new[] { "Username_Id" });
            DropIndex("dbo.PaymentDetails", new[] { "Username_Id" });
            DropIndex("dbo.PaymentDetails", new[] { "PaymentTypeId_Id" });
            DropIndex("dbo.IdentityUserRoles", new[] { "IdentityRole_Id" });
            DropIndex("dbo.IdentityUserRoles", new[] { "OjayFoodUser_Id" });
            DropIndex("dbo.IdentityUserLogins", new[] { "OjayFoodUser_Id" });
            DropIndex("dbo.IdentityUserClaims", new[] { "OjayFoodUser_Id" });
            DropTable("dbo.IdentityRoles");
            DropTable("dbo.Stores");
            DropTable("dbo.ShippingDetails");
            DropTable("dbo.Products");
            DropTable("dbo.Users");
            DropTable("dbo.PaymentTypes");
            DropTable("dbo.PaymentDetails");
            DropTable("dbo.IdentityUserRoles");
            DropTable("dbo.IdentityUserLogins");
            DropTable("dbo.IdentityUserClaims");
            DropTable("dbo.OjayFoodUsers");
        }
    }
}
