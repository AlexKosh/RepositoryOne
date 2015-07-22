namespace Josephine.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddOrderInfoIsPacked : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.OrderInfoes", "isPacked", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.OrderInfoes", "isPacked");
        }
    }
}
