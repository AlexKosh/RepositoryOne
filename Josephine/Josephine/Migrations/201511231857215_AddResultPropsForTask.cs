namespace Josephine.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddResultPropsForTask : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.ProductionTasks", "ResultItemId", c => c.Int(nullable: false));
            AddColumn("dbo.ProductionTasks", "ResultQuantity", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.ProductionTasks", "ResultQuantity");
            DropColumn("dbo.ProductionTasks", "ResultItemId");
        }
    }
}
