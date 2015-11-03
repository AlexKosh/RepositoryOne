namespace Josephine.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addRecipeItemsAndFixedRecipe : DbMigration
    {
        public override void Up()
        {
            DropPrimaryKey("dbo.Recipes");
            CreateTable(
                "dbo.RecipeItems",
                c => new
                    {
                        RecipeId = c.Int(nullable: false),
                        ItemId = c.Int(nullable: false),
                        ItemCategory = c.Int(nullable: false),
                        Quantity = c.Int(nullable: false),
                        UnitsOfMeasurement = c.String(),
                    })
                .PrimaryKey(t => new { t.RecipeId, t.ItemId })
                .ForeignKey("dbo.Recipes", t => t.RecipeId, cascadeDelete: true)
                .Index(t => t.RecipeId);
            
            AlterColumn("dbo.Recipes", "RecipeId", c => c.Int(nullable: false, identity: true));
            AddPrimaryKey("dbo.Recipes", "RecipeId");
            DropColumn("dbo.Recipes", "ItemId");
            DropColumn("dbo.Recipes", "ItemCategory");
            DropColumn("dbo.Recipes", "Quantity");
            DropColumn("dbo.Recipes", "UnitsOfMeasurement");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Recipes", "UnitsOfMeasurement", c => c.String());
            AddColumn("dbo.Recipes", "Quantity", c => c.Int(nullable: false));
            AddColumn("dbo.Recipes", "ItemCategory", c => c.Int(nullable: false));
            AddColumn("dbo.Recipes", "ItemId", c => c.Int(nullable: false));
            DropForeignKey("dbo.RecipeItems", "RecipeId", "dbo.Recipes");
            DropIndex("dbo.RecipeItems", new[] { "RecipeId" });
            DropPrimaryKey("dbo.Recipes");
            AlterColumn("dbo.Recipes", "RecipeId", c => c.Int(nullable: false));
            DropTable("dbo.RecipeItems");
            AddPrimaryKey("dbo.Recipes", new[] { "RecipeId", "ItemId" });
        }
    }
}
