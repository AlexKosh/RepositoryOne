namespace Josephine.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddRecipeCategoryToRecipe : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Recipes", "ItemCategory", c => c.Int(nullable: false));
            AddColumn("dbo.Recipes", "RecipeCategory", c => c.Int(nullable: false));
            DropColumn("dbo.Recipes", "CategoryId");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Recipes", "CategoryId", c => c.Int(nullable: false));
            DropColumn("dbo.Recipes", "RecipeCategory");
            DropColumn("dbo.Recipes", "ItemCategory");
        }
    }
}
