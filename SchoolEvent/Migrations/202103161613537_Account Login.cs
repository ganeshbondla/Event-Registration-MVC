namespace SchoolEvent.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AccountLogin : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.AccountLogins",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Username = c.String(nullable: false),
                        Password = c.String(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.AccountLogins");
        }
    }
}
