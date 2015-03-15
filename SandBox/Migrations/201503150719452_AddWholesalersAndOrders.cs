namespace SandBox.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddWholesalersAndOrders : DbMigration
    {
        public override void Up()
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
                    })
                .PrimaryKey(t => t.ItemID);
            
            CreateTable(
                "dbo.Wholesalers",
                c => new
                    {
                        WholesalerID = c.Int(nullable: false, identity: true),
                        FirstName = c.String(),
                        LastName = c.String(),
                        City = c.String(),
                        PhoneNumber = c.String(),
                        AlternatePhoneNumber = c.String(),
                        Address = c.String(),
                        AlternateAddress = c.String(),
                        Points = c.Int(nullable: false),
                        Level = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.WholesalerID);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.Wholesalers");
            DropTable("dbo.Orders");
        }
    }
}
