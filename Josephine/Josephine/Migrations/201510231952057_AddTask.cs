namespace Josephine.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddTask : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.ProductionTasks",
                c => new
                    {
                        ItemId = c.Int(nullable: false),
                        ItemCategory = c.Int(nullable: false),
                        Id = c.Int(nullable: false),
                        RecipeCategory = c.Int(nullable: false),
                        Quantity = c.Int(nullable: false),
                        isCompleted = c.Int(nullable: false),
                        StartTime = c.DateTime(nullable: false),
                        FinishTime = c.DateTime(nullable: false),
                        Priority = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.ItemId, t.ItemCategory });
            
        }
        
        public override void Down()
        {
            DropTable("dbo.ProductionTasks");
        }
    }
}
