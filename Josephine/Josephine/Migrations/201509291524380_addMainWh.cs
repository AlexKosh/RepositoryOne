namespace Josephine.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addMainWh : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.MainWarehouses",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        CategoryId = c.Int(nullable: false),
                        Name = c.String(),
                        Color = c.String(),
                        Quantity = c.Int(nullable: false),
                        UnitOfMeasurement = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.MainWarehouses");
        }
    }
}
