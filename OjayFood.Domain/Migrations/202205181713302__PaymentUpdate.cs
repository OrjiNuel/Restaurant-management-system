namespace OjayFood.Domain.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class _PaymentUpdate : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.PaymentDetails", "PaymentTypeId_Id", "dbo.PaymentTypes");
            DropForeignKey("dbo.PaymentDetails", "Username_Id", "dbo.Users");
            DropIndex("dbo.PaymentDetails", new[] { "PaymentTypeId_Id" });
            DropIndex("dbo.PaymentDetails", new[] { "Username_Id" });
            CreateTable(
                "dbo.Payments",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Amount = c.Decimal(nullable: false, precision: 16, scale: 8),
                        Currency = c.String(),
                        Phone = c.String(),
                        CardNumber = c.String(),
                        Description = c.String(),
                        PaymentTypeId_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.PaymentTypes", t => t.PaymentTypeId_Id)
                .Index(t => t.PaymentTypeId_Id);
            
            DropTable("dbo.PaymentDetails");
        }
        
        public override void Down()
        {
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
                .PrimaryKey(t => t.Id);
            
            DropForeignKey("dbo.Payments", "PaymentTypeId_Id", "dbo.PaymentTypes");
            DropIndex("dbo.Payments", new[] { "PaymentTypeId_Id" });
            DropTable("dbo.Payments");
            CreateIndex("dbo.PaymentDetails", "Username_Id");
            CreateIndex("dbo.PaymentDetails", "PaymentTypeId_Id");
            AddForeignKey("dbo.PaymentDetails", "Username_Id", "dbo.Users", "Id");
            AddForeignKey("dbo.PaymentDetails", "PaymentTypeId_Id", "dbo.PaymentTypes", "Id");
        }
    }
}
