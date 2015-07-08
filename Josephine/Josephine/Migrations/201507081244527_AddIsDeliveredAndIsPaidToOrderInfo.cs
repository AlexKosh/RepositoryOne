namespace Josephine.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddIsDeliveredAndIsPaidToOrderInfo : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.OrderInfoes", "isDelivered", c => c.String());
            AddColumn("dbo.OrderInfoes", "isPaid", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.OrderInfoes", "isPaid");
            DropColumn("dbo.OrderInfoes", "isDelivered");
        }
    }
}
