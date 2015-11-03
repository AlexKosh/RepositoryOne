namespace Josephine.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class fixedRecipeAndItems1 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Recipes", "ResultItemId", c => c.Int(nullable: false));
            AddColumn("dbo.Recipes", "ResultName", c => c.String());
            AddColumn("dbo.Recipes", "ResultQuantity", c => c.Int(nullable: false));
            AddColumn("dbo.Recipes", "UnitsOfMeasurement", c => c.String());
            AddColumn("dbo.RecipeItems", "Name", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.RecipeItems", "Name");
            DropColumn("dbo.Recipes", "UnitsOfMeasurement");
            DropColumn("dbo.Recipes", "ResultQuantity");
            DropColumn("dbo.Recipes", "ResultName");
            DropColumn("dbo.Recipes", "ResultItemId");
        }
    }
}
