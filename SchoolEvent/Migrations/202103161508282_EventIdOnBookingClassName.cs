namespace SchoolEvent.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class EventIdOnBookingClassName : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.UserBookings", "Eventid", c => c.String());
            DropColumn("dbo.UserBookings", "Eid");
        }
        
        public override void Down()
        {
            AddColumn("dbo.UserBookings", "Eid", c => c.String());
            DropColumn("dbo.UserBookings", "Eventid");
        }
    }
}
