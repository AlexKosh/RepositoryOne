namespace Josephine.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddCutProp : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Cuts", "ProductId", c => c.Int(nullable: false));
            AddColumn("dbo.Cuts", "ModelNumber", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Cuts", "ModelNumber");
            DropColumn("dbo.Cuts", "ProductId");
        }
    }
}
