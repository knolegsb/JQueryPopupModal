namespace JQueryPopupModal.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Some : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Contacts", "CountryName", c => c.String());
            AddColumn("dbo.Contacts", "StateName", c => c.String());
            AlterColumn("dbo.Contacts", "ContactPerson", c => c.String(nullable: false));
            AlterColumn("dbo.Contacts", "ContactNo", c => c.String(nullable: false));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Contacts", "ContactNo", c => c.String());
            AlterColumn("dbo.Contacts", "ContactPerson", c => c.String());
            DropColumn("dbo.Contacts", "StateName");
            DropColumn("dbo.Contacts", "CountryName");
        }
    }
}
