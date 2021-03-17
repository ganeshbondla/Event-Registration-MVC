namespace SchoolEvent.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AccountTransections : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Transections",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        UserId = c.String(),
                        EventId = c.String(),
                        TransectionId = c.String(),
                        TransectionStatus = c.String(),
                        TransectionDate = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.Transections");
        }
    }
}
