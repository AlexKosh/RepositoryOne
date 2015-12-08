namespace Josephine.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Start : DbMigration
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
                        Speciality = c.String(),
                        lastPurchase = c.DateTime(nullable: false),
                        isInformed = c.Boolean(nullable: false),
                        Balance = c.Int(nullable: false),
                        MoneySpent = c.Int(nullable: false),
                        Level = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.CustomerId);
            
            CreateTable(
                "dbo.Cuts",
                c => new
                    {
                        CutId = c.Int(nullable: false, identity: true),
                        TaskId = c.Int(nullable: false),
                        ModelNumber = c.Int(nullable: false),
                        Name = c.String(),
                        Size = c.Int(nullable: false),
                        Color = c.String(),
                        Quantity = c.Int(nullable: false),
                        isComplete = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.CutId);
            
            CreateTable(
                "dbo.Employees",
                c => new
                    {
                        EmployeeId = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        Surname = c.String(),
                        HireDate = c.DateTime(nullable: false),
                        Birthday = c.DateTime(nullable: false),
                        Address = c.String(),
                        PhoneNumber = c.String(),
                        AlternatePhone = c.String(),
                        Skype = c.String(),
                        SocialNetwork = c.String(),
                        Email = c.String(),
                        Speciality = c.String(),
                        Description = c.String(),
                        Gender = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.EmployeeId);
            
            CreateTable(
                "dbo.MainWarehouses",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        CategoryId = c.Int(nullable: false),
                        Name = c.String(),
                        Color = c.String(),
                        Quantity = c.Int(nullable: false),
                        UnitOfMeasurement = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.OrderInfoes",
                c => new
                    {
                        OrderId = c.Int(nullable: false, identity: true),
                        EmployeeId = c.Int(nullable: false),
                        CustomerId = c.Int(nullable: false),
                        ShippingMethod = c.String(),
                        ShipFrom = c.String(),
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
                        isDelivered = c.String(),
                        isPaid = c.String(),
                        isPacked = c.String(),
                        isResolved = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.OrderId);
            
            CreateTable(
                "dbo.OrderProducts",
                c => new
                    {
                        OrderId = c.Int(nullable: false),
                        ProductId = c.Int(nullable: false),
                        ModelNumber = c.Int(nullable: false),
                        Color = c.String(),
                        Size = c.Int(nullable: false),
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
                "dbo.ProductionTasks",
                c => new
                    {
                        TaskId = c.Int(nullable: false, identity: true),
                        TaskCategory = c.Int(nullable: false),
                        ResultItemId = c.Int(nullable: false),
                        ResultQuantity = c.Int(nullable: false),
                        isCompleted = c.Int(nullable: false),
                        StartTime = c.DateTime(nullable: false),
                        FinishTime = c.DateTime(nullable: false),
                        Priority = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.TaskId);
            
            CreateTable(
                "dbo.TaskItems",
                c => new
                    {
                        TaskItemId = c.Int(nullable: false, identity: true),
                        Quantity = c.Int(nullable: false),
                        TaskId = c.Int(nullable: false),
                        ItemId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.TaskItemId)
                .ForeignKey("dbo.ProductionTasks", t => t.TaskId, cascadeDelete: true)
                .Index(t => t.TaskId);
            
            CreateTable(
                "dbo.Recipes",
                c => new
                    {
                        RecipeId = c.Int(nullable: false, identity: true),
                        RecipeCategory = c.Int(nullable: false),
                        Name = c.String(),
                        ResultItemId = c.Int(nullable: false),
                        ResultName = c.String(),
                        ResultQuantity = c.Int(nullable: false),
                        UnitsOfMeasurement = c.String(),
                    })
                .PrimaryKey(t => t.RecipeId);
            
            CreateTable(
                "dbo.RecipeItems",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        RecipeId = c.Int(nullable: false),
                        ItemId = c.Int(nullable: false),
                        ItemCategory = c.Int(nullable: false),
                        Name = c.String(),
                        Quantity = c.Int(nullable: false),
                        UnitsOfMeasurement = c.String(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Recipes", t => t.RecipeId, cascadeDelete: true)
                .Index(t => t.RecipeId);
            
            CreateTable(
                "dbo.Sales",
                c => new
                    {
                        SaleId = c.Int(nullable: false, identity: true),
                        OrderId = c.Int(nullable: false),
                        ProductId = c.Int(nullable: false),
                        CustomerId = c.Int(nullable: false),
                        EmployeeId = c.Int(nullable: false),
                        SaleDate = c.DateTime(nullable: false),
                        ModelNumber = c.Int(nullable: false),
                        Color = c.String(),
                        Size = c.Int(nullable: false),
                        Quantity = c.Int(nullable: false),
                        ProductPrice = c.Int(nullable: false),
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
            DropForeignKey("dbo.RecipeItems", "RecipeId", "dbo.Recipes");
            DropForeignKey("dbo.TaskItems", "TaskId", "dbo.ProductionTasks");
            DropIndex("dbo.RecipeItems", new[] { "RecipeId" });
            DropIndex("dbo.TaskItems", new[] { "TaskId" });
            DropTable("dbo.Warehouses");
            DropTable("dbo.Stores");
            DropTable("dbo.Sales");
            DropTable("dbo.RecipeItems");
            DropTable("dbo.Recipes");
            DropTable("dbo.TaskItems");
            DropTable("dbo.ProductionTasks");
            DropTable("dbo.Prices");
            DropTable("dbo.OrderProducts");
            DropTable("dbo.OrderInfoes");
            DropTable("dbo.MainWarehouses");
            DropTable("dbo.Employees");
            DropTable("dbo.Cuts");
            DropTable("dbo.Customers");
        }
    }
}
