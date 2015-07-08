namespace Josephine.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addIsResolved : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.OrderInfoes", "isResolved", c => c.Boolean(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.OrderInfoes", "isResolved");
        }
    }
}
