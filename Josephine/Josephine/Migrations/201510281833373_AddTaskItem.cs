namespace Josephine.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddTaskItem : DbMigration
    {
        public override void Up()
        {
            DropPrimaryKey("dbo.ProductionTasks");
            DropPrimaryKey("dbo.Recipes");
            CreateTable(
                "dbo.TaskItems",
                c => new
                    {
                        TaskItemId = c.Int(nullable: false, identity: true),
                        Quantity = c.Int(nullable: false),
                        TaskId = c.Int(nullable: false),
                        ItemId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.TaskItemId)
                .ForeignKey("dbo.ProductionTasks", t => t.TaskId, cascadeDelete: true)
                .Index(t => t.TaskId);
            
            AddColumn("dbo.ProductionTasks", "TaskId", c => c.Int(nullable: false, identity: true));
            AddColumn("dbo.Recipes", "RecipeId", c => c.Int(nullable: false));
            AddPrimaryKey("dbo.ProductionTasks", "TaskId");
            AddPrimaryKey("dbo.Recipes", new[] { "RecipeId", "ItemId" });
            DropColumn("dbo.ProductionTasks", "ItemId");
            DropColumn("dbo.ProductionTasks", "ItemCategory");
            DropColumn("dbo.ProductionTasks", "Id");
            DropColumn("dbo.ProductionTasks", "RecipeCategory");
            DropColumn("dbo.ProductionTasks", "Quantity");
            DropColumn("dbo.Recipes", "Id");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Recipes", "Id", c => c.Int(nullable: false));
            AddColumn("dbo.ProductionTasks", "Quantity", c => c.Int(nullable: false));
            AddColumn("dbo.ProductionTasks", "RecipeCategory", c => c.Int(nullable: false));
            AddColumn("dbo.ProductionTasks", "Id", c => c.Int(nullable: false));
            AddColumn("dbo.ProductionTasks", "ItemCategory", c => c.Int(nullable: false));
            AddColumn("dbo.ProductionTasks", "ItemId", c => c.Int(nullable: false));
            DropForeignKey("dbo.TaskItems", "TaskId", "dbo.ProductionTasks");
            DropIndex("dbo.TaskItems", new[] { "TaskId" });
            DropPrimaryKey("dbo.Recipes");
            DropPrimaryKey("dbo.ProductionTasks");
            DropColumn("dbo.Recipes", "RecipeId");
            DropColumn("dbo.ProductionTasks", "TaskId");
            DropTable("dbo.TaskItems");
            AddPrimaryKey("dbo.Recipes", new[] { "Id", "ItemId" });
            AddPrimaryKey("dbo.ProductionTasks", new[] { "ItemId", "ItemCategory" });
        }
    }
}
