namespace Josephine.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class fixedRecipeItemsKeys : DbMigration
    {
        public override void Up()
        {
            DropPrimaryKey("dbo.RecipeItems");
            AddColumn("dbo.RecipeItems", "Id", c => c.Int(nullable: false, identity: true));
            AddPrimaryKey("dbo.RecipeItems", "Id");
        }
        
        public override void Down()
        {
            DropPrimaryKey("dbo.RecipeItems");
            DropColumn("dbo.RecipeItems", "Id");
            AddPrimaryKey("dbo.RecipeItems", new[] { "RecipeId", "ItemId" });
        }
    }
}
