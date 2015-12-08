namespace Josephine.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddTaskCutsForProductionTask : DbMigration
    {
        public override void Up()
        {
            CreateIndex("dbo.Cuts", "TaskId");
            AddForeignKey("dbo.Cuts", "TaskId", "dbo.ProductionTasks", "TaskId", cascadeDelete: true);
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Cuts", "TaskId", "dbo.ProductionTasks");
            DropIndex("dbo.Cuts", new[] { "TaskId" });
        }
    }
}
