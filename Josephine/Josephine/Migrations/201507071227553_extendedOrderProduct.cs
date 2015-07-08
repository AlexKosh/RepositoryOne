namespace Josephine.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class extendedOrderProduct : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.OrderProducts", "ModelNumber", c => c.Int(nullable: false));
            AddColumn("dbo.OrderProducts", "Color", c => c.String());
            AddColumn("dbo.OrderProducts", "Size", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.OrderProducts", "Size");
            DropColumn("dbo.OrderProducts", "Color");
            DropColumn("dbo.OrderProducts", "ModelNumber");
        }
    }
}
