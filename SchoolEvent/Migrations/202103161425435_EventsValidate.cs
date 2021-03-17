namespace SchoolEvent.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class EventsValidate : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Events", "EventName", c => c.String(nullable: false));
            AlterColumn("dbo.Events", "EventDate", c => c.String(nullable: false));
            AlterColumn("dbo.Events", "EventAmount", c => c.String(nullable: false));
            AlterColumn("dbo.Events", "EventId", c => c.String(nullable: false));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Events", "EventId", c => c.String());
            AlterColumn("dbo.Events", "EventAmount", c => c.String());
            AlterColumn("dbo.Events", "EventDate", c => c.String());
            AlterColumn("dbo.Events", "EventName", c => c.String());
        }
    }
}
