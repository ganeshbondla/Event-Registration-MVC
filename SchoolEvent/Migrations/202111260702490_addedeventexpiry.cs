namespace SchoolEvent.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addedeventexpiry : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Events", "EventExpired", c => c.Boolean(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Events", "EventExpired");
        }
    }
}
