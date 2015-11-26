namespace Josephine.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddTaskCategory : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.ProductionTasks", "TaskCategory", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.ProductionTasks", "TaskCategory");
        }
    }
}
