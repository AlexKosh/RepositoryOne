namespace Josephine.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddRecipeTable : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Recipes",
                c => new
                    {
                        Id = c.Int(nullable: false),
                        ItemId = c.Int(nullable: false),
                        CategoryId = c.Int(nullable: false),
                        Name = c.String(),
                        Quantity = c.Int(nullable: false),
                        UnitsOfMeasurement = c.String(),
                    })
                .PrimaryKey(t => new { t.Id, t.ItemId });
            
        }
        
        public override void Down()
        {
            DropTable("dbo.Recipes");
        }
    }
}
