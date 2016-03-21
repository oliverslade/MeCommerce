namespace MeCommerce.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class UserModel : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.AspNetUsers", "HouseName", c => c.String());
            AddColumn("dbo.AspNetUsers", "AddressLine1", c => c.String());
            AddColumn("dbo.AspNetUsers", "AddressLine2", c => c.String());
            AddColumn("dbo.AspNetUsers", "AddressLine3", c => c.String());
            AddColumn("dbo.AspNetUsers", "Town", c => c.String());
            AddColumn("dbo.AspNetUsers", "County", c => c.String());
            AddColumn("dbo.AspNetUsers", "Postcode", c => c.String());
            AddColumn("dbo.AspNetUsers", "ContactNumber", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.AspNetUsers", "ContactNumber");
            DropColumn("dbo.AspNetUsers", "Postcode");
            DropColumn("dbo.AspNetUsers", "County");
            DropColumn("dbo.AspNetUsers", "Town");
            DropColumn("dbo.AspNetUsers", "AddressLine3");
            DropColumn("dbo.AspNetUsers", "AddressLine2");
            DropColumn("dbo.AspNetUsers", "AddressLine1");
            DropColumn("dbo.AspNetUsers", "HouseName");
        }
    }
}
