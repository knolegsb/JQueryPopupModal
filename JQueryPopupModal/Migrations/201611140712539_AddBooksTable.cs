namespace JQueryPopupModal.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddBooksTable : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Books",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        Author = c.String(),
                        Price = c.Double(),
                    })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.Books");
        }
    }
}
