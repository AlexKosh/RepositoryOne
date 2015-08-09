namespace Josephine.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addEmployeeIdToSale : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Sales", "EmployeeId", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Sales", "EmployeeId");
        }
    }
}
