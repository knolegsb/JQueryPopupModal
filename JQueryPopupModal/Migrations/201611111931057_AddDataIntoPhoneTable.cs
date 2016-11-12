namespace JQueryPopupModal.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddDataIntoPhoneTable : DbMigration
    {
        public override void Up()
        {
            Sql("INSERT INTO Phones (Model, Company, Price) VALUES ('Samsung Galaxy Note 1', 'Samsung', 339)");
            Sql("INSERT INTO Phones (Model, Company, Price) VALUES ('Samsung Galaxy Note 2', 'Samsung', 339)");
            Sql("INSERT INTO Phones (Model, Company, Price) VALUES ('Samsung Galaxy S III', 'Samsung', 217)");
            Sql("INSERT INTO Phones (Model, Company, Price) VALUES ('Samsung Galaxy S IV', 'Samsung', 234)");
            Sql("INSERT INTO Phones (Model, Company, Price) VALUES ('iPhone 5', 'Apple', 456)");
        }
        
        public override void Down()
        {
        }
    }
}
