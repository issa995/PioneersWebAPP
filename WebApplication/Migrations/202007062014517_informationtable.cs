namespace WebApplication.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class informationtable : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Information",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        CustomerName = c.String(nullable: false),
                        Country = c.String(nullable: false),
                        City = c.String(nullable: false),
                        Address = c.String(nullable: false),
                        TaxNumber = c.String(),
                        Active = c.Boolean(nullable: false),
                        Logo = c.String(),
                    })
                .PrimaryKey(t => t.ID);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.Information");
        }
    }
}
