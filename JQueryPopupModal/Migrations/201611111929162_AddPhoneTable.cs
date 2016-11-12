namespace JQueryPopupModal.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddPhoneTable : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Phones",
                c => new
                    {
                        PhoneId = c.Int(nullable: false, identity: true),
                        Model = c.String(nullable: false),
                        Company = c.String(nullable: false),
                        Price = c.Decimal(nullable: false, precision: 18, scale: 2),
                    })
                .PrimaryKey(t => t.PhoneId);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.Phones");
        }
    }
}
