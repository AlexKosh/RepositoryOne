namespace SandBox.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddTailoringItemAndSeparateOrdersAndFixedTailoringItem : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.TailoringItems", "ItemName", c => c.String());
        }
        
        public override void Down()
        {
            AlterColumn("dbo.TailoringItems", "ItemName", c => c.Int(nullable: false));
        }
    }
}
