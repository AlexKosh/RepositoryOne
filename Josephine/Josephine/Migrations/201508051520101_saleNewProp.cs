namespace Josephine.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class saleNewProp : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Sales", "ModelNumber", c => c.Int(nullable: false));
            AddColumn("dbo.Sales", "Color", c => c.String());
            AddColumn("dbo.Sales", "Size", c => c.Int(nullable: false));
            AddColumn("dbo.Sales", "ProductPrice", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Sales", "ProductPrice");
            DropColumn("dbo.Sales", "Size");
            DropColumn("dbo.Sales", "Color");
            DropColumn("dbo.Sales", "ModelNumber");
        }
    }
}
