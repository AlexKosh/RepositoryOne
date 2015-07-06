namespace Josephine.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class InitialCreate : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Customers",
                c => new
                    {
                        CustomerId = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        Surname = c.String(),
                        Nickname = c.String(),
                        Gender = c.Boolean(nullable: false),
                        Birthday = c.DateTime(nullable: false),
                        Age = c.Int(nullable: false),
                        FirstMet = c.DateTime(nullable: false),
                        Notations = c.String(),
                        locationOfTrade = c.String(),
                        PhoneNumber = c.String(),
                        AlternatePhone = c.String(),
                        Email = c.String(),
                        Skype = c.String(),
                        OtherContact = c.String(),
                        lastPurchase = c.DateTime(nullable: false),
                        isInformed = c.Boolean(nullable: false),
                        Balance = c.Int(nullable: false),
                        MoneySpent = c.Int(nullable: false),
                        Level = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.CustomerId);
            
            CreateTable(
                "dbo.Employees",
                c => new
                    {
                        EmployeeId = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        Surname = c.String(),
                        HireDate = c.DateTime(nullable: false),
                        City = c.String(),
                        Address = c.String(),
                        PhoneNumber = c.String(),
                        AlternatePhone = c.String(),
                    })
                .PrimaryKey(t => t.EmployeeId);
            
            CreateTable(
                "dbo.OrderInfoes",
                c => new
                    {
                        OrderId = c.Int(nullable: false, identity: true),
                        EmployeeId = c.Int(nullable: false),
                        CustomerId = c.Int(nullable: false),
                        ShippingMethod = c.String(),
                        ShipFrom = c.String(),
                        ShippingToCity = c.String(),
                        ShipAddress = c.String(),
                        OrderDate = c.DateTime(nullable: false),
                        ShipmentDateMin = c.DateTime(nullable: false),
                        ShipmentDateMax = c.DateTime(nullable: false),
                        OrderNotation = c.String(),
                        Priority = c.Int(nullable: false),
                        PaymentMethod = c.String(),
                        OrderRecievingCode = c.String(),
                        Paid = c.Int(nullable: false),
                        OrderDiscount = c.Int(nullable: false),
                        OrderCost = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.OrderId);
            
            CreateTable(
                "dbo.OrderProducts",
                c => new
                    {
                        OrderId = c.Int(nullable: false),
                        ProductId = c.Int(nullable: false),
                        Quantity = c.Int(nullable: false),
                        ProductPrice = c.Int(nullable: false),
                        ProductDiscount = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.OrderId, t.ProductId });
            
            CreateTable(
                "dbo.Prices",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        ModelNumber = c.Int(nullable: false),
                        Price = c.Int(nullable: false),
                        Date = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Sales",
                c => new
                    {
                        SaleId = c.Int(nullable: false, identity: true),
                        OrderId = c.Int(nullable: false),
                        ProductId = c.Int(nullable: false),
                        CustomerId = c.Int(nullable: false),
                        SaleDate = c.DateTime(nullable: false),
                        Quantity = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.SaleId);
            
            CreateTable(
                "dbo.Stores",
                c => new
                    {
                        ProductId = c.Int(nullable: false, identity: true),
                        Price = c.Int(nullable: false),
                        Name = c.String(),
                        ModelNumber = c.Int(nullable: false),
                        Color = c.String(),
                        Size = c.Int(nullable: false),
                        Quantity = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.ProductId);
            
            CreateTable(
                "dbo.Warehouses",
                c => new
                    {
                        ProductId = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        ModelNumber = c.Int(nullable: false),
                        Color = c.String(),
                        Size = c.Int(nullable: false),
                        Quantity = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.ProductId);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.Warehouses");
            DropTable("dbo.Stores");
            DropTable("dbo.Sales");
            DropTable("dbo.Prices");
            DropTable("dbo.OrderProducts");
            DropTable("dbo.OrderInfoes");
            DropTable("dbo.Employees");
            DropTable("dbo.Customers");
        }
    }
}
