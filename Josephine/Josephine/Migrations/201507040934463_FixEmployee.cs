namespace Josephine.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class FixEmployee : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Employees", "Birthday", c => c.DateTime(nullable: false));
            AddColumn("dbo.Employees", "Skype", c => c.String());
            AddColumn("dbo.Employees", "SocialNetwork", c => c.String());
            AddColumn("dbo.Employees", "Email", c => c.String());
            AddColumn("dbo.Employees", "Speciality", c => c.String());
            AddColumn("dbo.Employees", "Description", c => c.String());
            DropColumn("dbo.Employees", "City");
            DropColumn("dbo.OrderInfoes", "ShippingToCity");
        }
        
        public override void Down()
        {
            AddColumn("dbo.OrderInfoes", "ShippingToCity", c => c.String());
            AddColumn("dbo.Employees", "City", c => c.String());
            DropColumn("dbo.Employees", "Description");
            DropColumn("dbo.Employees", "Speciality");
            DropColumn("dbo.Employees", "Email");
            DropColumn("dbo.Employees", "SocialNetwork");
            DropColumn("dbo.Employees", "Skype");
            DropColumn("dbo.Employees", "Birthday");
        }
    }
}
