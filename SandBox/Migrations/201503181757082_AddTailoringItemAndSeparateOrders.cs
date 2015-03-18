namespace SandBox.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddTailoringItemAndSeparateOrders : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.OrderDatas",
                c => new
                    {
                        OrderID = c.Int(nullable: false, identity: true),
                        WholesalerID = c.Int(nullable: false),
                        Delivery = c.Boolean(nullable: false),
                        DeliveryAddress = c.String(),
                        DeliveryDateTime = c.DateTime(nullable: false),
                        Description = c.String(),
                    })
                .PrimaryKey(t => t.OrderID);
            
            CreateTable(
                "dbo.OrderItems",
                c => new
                    {
                        OrderItemID = c.Int(nullable: false, identity: true),
                        OrderID = c.Int(nullable: false),
                        ItemName = c.String(),
                        ItemNumber = c.Int(nullable: false),
                        Color = c.String(),
                        Size = c.Int(nullable: false),
                        Price = c.Int(nullable: false),
                        Quantity = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.OrderItemID);
            
            CreateTable(
                "dbo.TailoringItems",
                c => new
                    {
                        ItemID = c.Int(nullable: false, identity: true),
                        ItemNumber = c.Int(nullable: false),
                        ItemName = c.Int(nullable: false),
                        Color = c.String(),
                        Size = c.Int(nullable: false),
                        Quantity = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.ItemID);
            
            DropTable("dbo.Orders");
        }
        
        public override void Down()
        {
            CreateTable(
                "dbo.Orders",
                c => new
                    {
                        ItemID = c.Int(nullable: false, identity: true),
                        OrderID = c.Int(nullable: false),
                        WholesalerID = c.Int(nullable: false),
                        Name = c.String(),
                        ItemNumber = c.Int(nullable: false),
                        Color = c.String(),
                        Size = c.Int(nullable: false),
                        Price = c.Int(nullable: false),
                        Quantity = c.Int(nullable: false),
                        Delivery = c.Boolean(nullable: false),
                        DeliveryAddress = c.String(),
                        DeliveryDateTime = c.DateTime(nullable: false),
                        Description = c.String(),
                    })
                .PrimaryKey(t => t.ItemID);
            
            DropTable("dbo.TailoringItems");
            DropTable("dbo.OrderItems");
            DropTable("dbo.OrderDatas");
        }
    }
}
