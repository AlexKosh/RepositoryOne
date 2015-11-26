namespace Josephine.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddCut : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Cuts",
                c => new
                    {
                        CutId = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        Size = c.Int(nullable: false),
                        Color = c.String(),
                        Quantity = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.CutId);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.Cuts");
        }
    }
}
